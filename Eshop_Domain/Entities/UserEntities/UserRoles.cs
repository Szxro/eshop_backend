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
            UserUserRoles = new HashSet<UserUserRoles>();
        }

        public string Name { get; set; } = null!;

        public string NormalizedName { get; set; } = null!;

        // N:N
        public ICollection<UserUserRoles> UserUserRoles { get; set; }
    }
}
