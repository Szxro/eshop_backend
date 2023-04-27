using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Domain.Entities.UserEntities
{
    public class UserSalt : AuditableEntity
    {
        public UserSalt()
        {
            User = new User();
        }

        public string SaltValue { get; set; } = null!;

        public int UserId { get; set; }

        //1:N
        public User User { get; set; }
    }
}
