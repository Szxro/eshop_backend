using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Domain.Entities.UserEntities
{
    public class UserRefreshToken : AuditableEntity
    {
        public UserRefreshToken()
        {
            RefreshTokenUser = new HashSet<RefreshTokenUser>();
        }

        public string RefreshTokenValue { get; set; } = null!;

        public bool IsActive { get; set; }

        public ICollection<RefreshTokenUser> RefreshTokenUser { get; set; }
    }
}
