namespace GodotAssetLibrary.Contracts
{
    public interface IMailUtility
    {
        Task<bool> Send(string username, string email, string subject, string body, string altBody, CancellationToken cancellationToken = default);
    }
}
