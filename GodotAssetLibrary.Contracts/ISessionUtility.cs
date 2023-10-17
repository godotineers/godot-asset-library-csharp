namespace GodotAssetLibrary.Contracts
{
    public interface ISessionUtility
    {
        byte[] GenerateSessionId();

        byte[] GenerateResetId();

        string GenerateToken(TokenData tokenData);

        byte[] SignToken(string tokenPayload);

        TokenData? Validate(string token);
    }
}
