using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Domain.Entities.TokenEntities
{
    public class TokenResponse
    {
        public TokenResponse()
        {
            //default value of string is null
            RefreshTokenValue = string.Empty;
            TokenValue = string.Empty;
        }

        public string RefreshTokenValue { get; set; }

        public string TokenValue { get; set; }
    }
}
