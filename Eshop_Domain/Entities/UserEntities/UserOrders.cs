using Eshop_Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Domain.Entities.UserEntities
{
    public class UserOrders : AuditableEntity
    {
        public string OrderName { get; set; } = null!;

        public DateTime ArrivedDate { get; set; }

        public int UserId { get; set; }

        public User User { get; set; } = new();
    }
}
