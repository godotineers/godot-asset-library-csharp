using GodotAssetLibrary.Common.Domain;
using GodotAssetLibrary.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotAssetLibrary.DataLayer.Repositories
{
    internal class VersionRepository : IVersionRepository
    {
        public VersionRepository(
                    IAssetLibraryContext context)
        {
            Context = context;
        }

        public IAssetLibraryContext Context { get; }

        public async Task<IEnumerable<GodotVersion>> GetVersions(CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(Context.Versions);
        }
    }
}
