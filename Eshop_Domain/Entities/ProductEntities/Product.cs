using Eshop_Domain.Common;
using Eshop_Domain.Entities.UserEntities;
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
            ProductCategory = new HashSet<ProductCategory>();
            UserCart = new HashSet<UserCart>();
            ProductFile = new HashSet<ProductFile>();
        }

        public string ProductName { get; set; } = null!;

        public string Description { get; set; } = null!;

        public double ProductPrice { get; set; }

        public int ProductQuantity { get; set; }

        //1:N
        public ICollection<ProductFile> ProductFile { get; set; }

        //1:N
        public ICollection<ProductCategory> ProductCategory { get; set; }

        //1:N
        public ICollection<UserCart> UserCart { get; set; }
    }
}
