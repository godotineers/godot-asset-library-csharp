using GodotAssetLibrary.Application.Extensions;
using GodotAssetLibrary.Application.Results.Auth;
using GodotAssetLibrary.Common.Tokens;
using GodotAssetLibrary.Contracts;
using GodotAssetLibrary.DataLayer.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Web;

namespace GodotAssetLibrary.Application.Commands.Auth
{
    public class ForgotPassword : IRequest<ForgotPasswordResult>
    {
        public string Email { get; set; }

        public class ForgotPasswordHandler : IRequestHandler<ForgotPassword, ForgotPasswordResult>
        {
            public ForgotPasswordHandler(
                    ILogger<ForgotPasswordHandler> logger,
                    IUserService userService,
                    IMailUtility mailUtility,
                    IPasswordUtility passwordUtility,
                    IUrlProvider urlProvider,
                    ITokenUtility<ResetTokenData> tokenUtility)
            {
                Logger = logger;
                UserService = userService;
                MailUtility = mailUtility;
                PasswordUtility = passwordUtility;
                UrlProvider = urlProvider;
                TokenUtility = tokenUtility;
            }

            public ILogger<ForgotPasswordHandler> Logger { get; }
            public IUserService UserService { get; }
            public IMailUtility MailUtility { get; }
            public IPasswordUtility PasswordUtility { get; }
            public IUrlProvider UrlProvider { get; }
            public ITokenUtility<ResetTokenData> TokenUtility { get; }

            public async Task<ForgotPasswordResult> Handle(ForgotPassword request, CancellationToken cancellationToken)
            {
                var user = await UserService.GetUserByEmail(request.Email, cancellationToken);

                if (user != null)
                {
                    var resetTokenBytes = PasswordUtility.GenerateResetToken();

                    var resetToken = TokenUtility.GenerateToken(new ResetTokenData
                    {
                        Reset = resetTokenBytes.AsBase64Encoded(),
                    });

                    await this.UserService.SetResetToken(user.UserId, resetTokenBytes, cancellationToken);
                    var resetLink = UrlProvider.GenerateUrl("GetResetPassword", "Auth", new { token = resetToken });
                    //var resetLink = $"{resetToken}";

                    if (!await MailUtility.Send(
                            user.Username,
                            user.Email,
                            $"Password reset requested for ${user.Username}",
                            BuildResetLinkEmailBody(user, resetLink),
                            $"Reset your (${user.Username}'s) password: resetLink",
                            cancellationToken
                        ))
                    {
                        this.Logger.LogError("Failed to send email");
                    }
                }

                return new ForgotPasswordResult
                {
                    Email = request.Email,
                };
            }

            private string BuildResetLinkEmailBody(Domain.User user, string resetLink)
            {
                return @$"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
<html xmlns=""http://www.w3.org/1999/xhtml"">
<head>
    <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />
    <title>Reset password for {HttpUtility.HtmlEncode(user.Username)} on Godot asset library</title>
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
</head>
<body>
    <h1>Password reset requested for Godot asset library</h1>
    <p>
        If you haven't requested the password reset you can disregard this email.
    </p>
    <p>
        If you aren't {HttpUtility.HtmlEncode(user.Username)}, you should disregard this email completely (or try to get in touch with the real {HttpUtility.HtmlEncode(user.Username)}, and forward it to him).
    </p>
    <p>
        <a href=""{HttpUtility.HtmlEncode(resetLink)}"">Click this link to reset your password.</a>
        (In case it isn't working, you can type/copy this url manually: <a href=""{HttpUtility.HtmlEncode(resetLink)}"">{HttpUtility.HtmlEncode(resetLink)}</a>)
    </p>
</body>
</html>";
            }
        }
    }
}
