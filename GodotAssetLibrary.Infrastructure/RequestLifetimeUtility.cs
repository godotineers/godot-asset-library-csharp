using GodotAssetLibrary.Contracts;

namespace GodotAssetLibrary.Infrastructure
{
    public class RequestLifetimeUtility : IRequestLifetime
    {
        public bool IsFrontend { get; set; }
    }
}
