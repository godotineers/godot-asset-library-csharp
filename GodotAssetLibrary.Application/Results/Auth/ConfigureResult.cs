using GodotAssetLibrary.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotAssetLibrary.Application.Results.Auth
{
    public class ConfigureResult
    {
        public IEnumerable<Category> Categories { get; set; }
        public string LoginUrl { get; internal set; }
        public string Token { get; internal set; }
    }
}
