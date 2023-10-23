using GodotAssetLibrary.Common.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotAssetLibrary.Contracts
{
    public interface ITokenUtility<TToken>
    {
        string GenerateToken(TToken tokenData);

        TToken? Validate(string token);
    }
}
