using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Domain.Entities.UserEntities
{
    public class RefreshTokenUser : AuditableEntity
    {
        public RefreshTokenUser()
        {
            User = new User();
            UserRefreshToken = new UserRefreshToken();
        }

        public int UserId { get; set; }

        public User User { get; set; }

        public int UserRefreshTokenId { get; set; }

        public UserRefreshToken UserRefreshToken { get; set; }
    }
}
