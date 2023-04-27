using Eshop_Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Domain.Entities.UserEntities
{
    public class UserRoles : AuditableEntity
    {
        public UserRoles()
        {
            //Initializing the props
            User = new HashSet<User>();
        }
        public string Name { get; set; } = null!;

        public string NormalizedName { get; set; } = null!;

        public int UserId { get; set; }

        public ICollection<User> User { get; set; }
    }
}
