namespace GodotAssetLibrary.Contracts
{
    public interface IPasswordUtility
    {
        string Generate(string initialPassword);
        bool Verify(string initialPassword, string passwordHash);
    }
}
