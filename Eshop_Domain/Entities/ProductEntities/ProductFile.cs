using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Domain.Entities.ProductEntities
{
    public class ProductFile:AuditableEntity
    {
        public string FileRealName { get; set; } = string.Empty;

        public string FileTempName { get; set; } = string.Empty;

        public string FilePath { get; set; } = string.Empty;

        public string FileContentType { get; set; } = string.Empty;

        public long FileLength { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; } = new();
    }
}
