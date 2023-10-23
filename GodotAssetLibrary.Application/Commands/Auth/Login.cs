using GodotAssetLibrary.Application.Results.Auth;
using GodotAssetLibrary.Common.User;
using GodotAssetLibrary.Contracts;
using GodotAssetLibrary.DataLayer.Services;
using MediatR;

namespace GodotAssetLibrary.Application.Commands.Auth
{
    public class Login : IRequest<LoginResult>
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }

        public class LoginHandler : IRequestHandler<Login, LoginResult>
        {
            public LoginHandler(
                    IUserService userService,
                    IPasswordUtility passwordUtility,
                    ISessionUtility sessionUtility,
                    ITokenUtility<AuthTokenData> tokenUtility)
            {
                UserService = userService;
                PasswordUtility = passwordUtility;
                SessionUtility = sessionUtility;
                TokenUtility = tokenUtility;
            }

            public IUserService UserService { get; }
            public IPasswordUtility PasswordUtility { get; }
            public ISessionUtility SessionUtility { get; }
            public ITokenUtility<AuthTokenData> TokenUtility { get; }

            public async Task<LoginResult> Handle(Login request, CancellationToken cancellationToken)
            {
                var user = await UserService.GetUserByUsername(request.Username, cancellationToken);

                if (user == null)
                {
                    return new LoginResult
                    {
                        Error = "Password or username does not match.",
                    };
                }

                // verify password hash matches
                if (PasswordUtility.Verify(request.Password, user.PasswordHash))
                {
                    byte[] sessionId;
                    string token;

                    // if a token was passed in, validate the token.
                    if (!string.IsNullOrWhiteSpace(request.Token))
                    {
                        var tokenData = TokenUtility.Validate(request.Token);

                        if (tokenData == null || tokenData?.Session == null)
                        {
                            // return invalid token supplied, 400;
                            return new LoginResult
                            {
                                Error = "Invalid token supplied",
                            };
                        }

                        sessionId = tokenData.Session;
                        token = request.Token;
                    }
                    else
                    {
                        // no token, generate a new session
                        sessionId = SessionUtility.GenerateSessionId();
                        token = TokenUtility.GenerateToken(new AuthTokenData
                        {
                            Session = sessionId,
                            UserData = new UserData
                            {
                                Email = user.Email,
                                Type = user.Type,
                                UserId = user.UserId,
                                Username = user.Username,
                            }
                        });
                    }

                    // update session token for user
                    await UserService.SetSessionToken(user.UserId, sessionId, cancellationToken);


                    return new LoginResult
                    {
                        Username = request.Username,
                        Authenticated = true,
                        Token = token,
                        Url = "asset",
                    };
                }
                else
                {
                    return new LoginResult
                    {
                        Error = "Password or username does not match.",
                    };
                }
            }
        }
    }
}
