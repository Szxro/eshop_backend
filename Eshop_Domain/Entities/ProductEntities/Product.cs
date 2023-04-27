using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Domain.Entities.ProductEntities
{
    public class Product : AuditableEntity
    {
        public Product()
        {
            //UserProducts = new HashSet<User>();
        }
        public string ProductName { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string ProductImageUrl { get; set; } = null!;

        public double ProductPrice { get; set; }

        public int ProductQuantity { get; set; }

        public int ProductCategoryId { get; set; }

        //1:N
        public ProductCategory ProductCategory { get; set; } = new();

        //1:N
        public int UserId { get; set; }

        public User User { get; set; } = new();
    }
}
