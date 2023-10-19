using System.Security.Claims;

namespace GodotAssetLibrary.Contracts
{

    public interface IClaimsProvider
    {
        public ClaimsPrincipal? ClaimsPrincipal { get; }
    }
}
