using GodotAssetLibrary.Common.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotAssetLibrary.Contracts
{
    public interface ITokenUtility
    {
        string GenerateToken(TokenData tokenData);

        byte[] SignToken(string tokenPayload);

        TokenData? Validate(string token);
    }
}
