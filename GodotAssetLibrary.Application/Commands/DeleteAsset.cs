using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotAssetLibrary.Application.Commands
{
    public class DeleteAsset : IRequest<string>
    {
        public int AssetId { get; set; }
    }
}
