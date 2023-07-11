using Eshop_Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Domain.Entities.UserEntities
{
    public class UserUserRoles : AuditableEntity
    {
        public int UserId { get; set; }

        public User User { get; set; } = new();

        public int UserRolesId { get; set; }

        public UserRoles UserRoles { get; set; } = new();
    }
}
