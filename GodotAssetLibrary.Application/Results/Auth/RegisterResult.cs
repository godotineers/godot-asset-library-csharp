using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotAssetLibrary.Application.Results.Auth
{
    public class RegisterResult
    {
        public string Error { get; internal set; }
        public string Username { get; internal set; }
        public bool Registered { get; internal set; }
        public string Url { get; internal set; }
    }
}
