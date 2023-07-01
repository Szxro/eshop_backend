using Eshop_Application.Common.Exceptions;
using Eshop_Application.Common.Interfaces;
using Eshop_Domain.Entities.ProductEntities;
using Eshop_Infrastructure.Common;
using Eshop_Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Infrastructure.Repositories
{
    public class ProductFileRepository : IProductFileRepository
    {
        private readonly IList<string> _validProductExtension = new List<string>() { "image/jpeg", "image/png", "image/jpg" };
        private readonly AppDbContext _context;

        public ProductFileRepository(AppDbContext context) 
        {
            _context = context;
        }

        public async Task UploadProductFileAsync(Product product, List<IFormFile> files)
        {
            List<ProductFile> newProductFile = new();

            foreach (var file in files)
            {
                //Making some validations
                if (!IsFileValid(file))
                {
                    throw new ProductException("The file is not valid");
                }

                //Creating a temporaryFileName
                string tempFileName = $"{Guid.NewGuid()}${Path.GetExtension(file.FileName)}";

                //Creating a filePath 
                string filePath = Path.Combine("Upload\\Files", tempFileName);

                //Creating a file instance 
                await using FileStream fs = new FileStream(filePath, FileMode.Create);


                ProductFile productFile = new ProductFile()
                {
                    FileRealName = file.FileName,
                    FileTempName = tempFileName,
                    FileLength = file.Length,
                    FileContentType = file.ContentType,
                    FilePath = $"{Directory.GetCurrentDirectory()}\\{filePath}"
                };

                newProductFile.Add(productFile);

                //copies the file that is upload
                await file.CopyToAsync(fs);
            }

            newProductFile.Select(x => x.Product = product).ToList();

            _context.ProductFile.AddRange(newProductFile);  
        }

        private bool IsFileValid(IFormFile? file)
        {
            return file is not null && _validProductExtension.Contains(file!.ContentType);
        }
    }
}
