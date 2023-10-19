using GodotAssetLibrary.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotAssetLibrary.Application.Results.Assets
{
    public class GetAssetsResult
    {
        public IEnumerable<Asset> Result { get; set; }
        public int Page { get; set; }
        public int Pages { get; set; }
        public int PageLength { get; set; }
        public int TotalItems { get; set; }
    }
}
