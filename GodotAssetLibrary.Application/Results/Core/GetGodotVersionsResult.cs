using GodotAssetLibrary.Common.Domain;

namespace GodotAssetLibrary.Application.Results.Core
{
    public class GetGodotVersionsResult
    {
        public IEnumerable<GodotVersion> Versions { get; set; }
    }
}
