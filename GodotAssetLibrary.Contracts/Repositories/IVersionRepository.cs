using GodotAssetLibrary.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotAssetLibrary.Contracts.Repositories
{
    public interface IVersionRepository
    {
        Task<IEnumerable<GodotVersion>> GetVersions(CancellationToken cancellationToken = default);
    }
}
