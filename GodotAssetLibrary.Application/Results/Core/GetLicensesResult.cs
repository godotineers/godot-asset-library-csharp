using GodotAssetLibrary.Common.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotAssetLibrary.Application.Results.Core
{
    public class GetLicensesResult
    {
        public IEnumerable<SoftwareLicense> Licenses { get; set; }
    }
}
