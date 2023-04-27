using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Domain.Entities.ProductEntities
{
    public class ProductCategory : AuditableEntity
    {
        public ProductCategory()
        {
            Products = new HashSet<Product>();
        }
        public string CategoryName { get; set; } = null!;

        public int ProductId { get; set; }

        //1:N
        public ICollection<Product> Products { get; set; }
    }
}
