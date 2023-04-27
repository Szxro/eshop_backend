using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Application.Common.Settings
{
    public class JwtSettings
    {
        public string ValidIssuer { get; set; } = string.Empty;

        public string ValidAudience { get; set; } = string.Empty;

        public string SecretKey { get; set; } = string.Empty;

        public int ExpiresIn { get; set; }
    }
}
