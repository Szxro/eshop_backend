using Eshop_Application.Common.Interfaces;
using Eshop_Domain.Entities.ProductEntities;
using Eshop_Infrastructure.Common;
using Eshop_Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Infrastructure.Repositories
{
    public class ProductFileRepository : GenericRepository<ProductFile>,IProductFileRepository
    {
        public ProductFileRepository(AppDbContext context) : base(context) { }

        public async Task AddProductFiles(List<ProductFile> files)
        {
            _context.ProductFile.AddRange(files);

            await _context.SaveChangesAsync();
        }
    }
}
