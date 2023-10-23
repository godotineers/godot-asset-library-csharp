using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotAssetLibrary.Application.Extensions
{
    public static class EncodingExtensions
    {
        public static string AsBase64Encoded(this byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
