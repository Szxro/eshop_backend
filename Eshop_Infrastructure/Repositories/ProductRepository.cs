using Eshop_Application.Common.Exceptions;
using Eshop_Application.Common.Interfaces;
using Eshop_Application.Features.Products.Commands.CreateProductCommand;
using Eshop_Domain.Entities.ProductEntities;
using Eshop_Domain.Entities.UserEntities;
using Eshop_Infrastructure.Common;
using Eshop_Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly IList<string> _validProductExtension = new List<string>() { "image/jpeg", "image/png", "image/jpg" };

        public readonly IProductFileRepository _fileRepository;

        public ProductRepository(AppDbContext context, IProductFileRepository fileRepository) : base(context)
        {
            _fileRepository = fileRepository;
        }

        public async Task CreateProduct(CreateProductCommand request)
        {
            Product newProduct = new Product()
            {
                ProductName = request.ProductName,
                Description = request.Description,
                ProductPrice = request.ProductPrice,
                ProductCategory = request.ProductCategory
                                         .Select(x => new ProductCategory() {CategoryName = x.CategoryName }).ToList(),
                ProductQuantity = request.ProductQuantity
            };

            await CreateEntity(newProduct);
        }

        public async Task UploadProductImageByProductNameAsync(string productName,List<IFormFile>? files)
        {
            List<ProductFile> newProductFile = new();

            //Getting the product and changing the state
            Product? product = await GetProductByProductName(productName);

            _context.Entry(product!).State = EntityState.Unchanged;

            if (files is null) 
            {
                throw new ProductException("The file is reqired");
            }

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
                    FileLength= file.Length,
                    FileContentType= file.ContentType,
                    FilePath = $"{Directory.GetCurrentDirectory()}\\{filePath}"
                };

                newProductFile.Add(productFile);

                //copies the file that is upload
                await file.CopyToAsync(fs);
            }

            newProductFile.Select(x => x.Product = product!).ToList();

            await _fileRepository.AddProductFiles(newProductFile);
        }

        public async Task<Product?> GetProductByProductName(string productName)
        {
            Product? product = await _context.Product.FirstOrDefaultAsync(x => x.ProductName == productName);

            return product ?? throw new ProductException("The Product was not found");
        }

        public async Task<bool> CheckProductNameExists(string productName)
        {
            return await _context.Product.AnyAsync(p => p.ProductName == productName);
        }

        private bool IsFileValid(IFormFile? file)
        {
            return file is not null && _validProductExtension.Contains(file!.ContentType);  
        }
    }
}
