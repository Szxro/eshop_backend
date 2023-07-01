using Eshop_Domain.Entities.ProductEntities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Application.Common.Interfaces
{
    public interface IProductFileRepository
    { 
        Task UploadProductFileAsync(Product product, List<IFormFile> files);
    }
}
