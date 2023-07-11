using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Domain.DTOS
{
    public record ProductDTO
    {
        public ProductDTO()
        {
           ProductCategories = new HashSet<ProductCategoryDTO>();  
           ProductFiles = new HashSet<ProductFileDTO>();
        }

        public string ProductName { get; init; } = string.Empty;

        public string Description { get; init; } = string.Empty;

        public double ProductPrice { get; init; }

        public int ProductQuantity { get; init; }

        public ICollection<ProductCategoryDTO> ProductCategories { get; init; }

        public ICollection<ProductFileDTO> ProductFiles { get; set; }
    }

    public record ProductCategoryDTO
    {
        public string CategoryName { get; set; } = null!;
    }

    public record ProductFileDTO 
    {
        public string FileRealName { get; set; } = string.Empty;

        public string FileTempName { get; set; } = string.Empty;

        public string FilePath { get; set; } = string.Empty;

        public string FileContentType { get; set; } = string.Empty;

        public long FileLength { get; set; }
    }

}
