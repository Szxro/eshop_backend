using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eshop_Domain.Entities.ProductEntities;

namespace Eshop_Domain.Entities.UserEntities
{
    public class UserCart : AuditableEntity
    {
        public UserCart()
        {
            Product = new HashSet<Product>();
        }

        public int UserId { get; set; }

        public User User { get; set; } = new();

        public int ProductId { get; set; }

        public ICollection<Product> Product { get; set; }

        public int ProductQuantity { get; set; }
    }
}
