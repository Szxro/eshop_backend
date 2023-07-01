using Eshop_Application.Common.Exceptions;
using Eshop_Application.Common.Interfaces;
using Eshop_Domain.Entities.ProductEntities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Application.Features.Products.Commands.UploadProductFileCommand
{
    public record UploadProductFileCommand(string productName, List<IFormFile>? files) : IRequest<Unit> { }

    public class UploadProductFileCommandHandler : IRequestHandler<UploadProductFileCommand, Unit>
    {
        private readonly IProductRepository _product;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductFileRepository _productFile;

        public UploadProductFileCommandHandler(
            IProductRepository product,
            IUnitOfWork unitOfWork,
            IProductFileRepository productFile)
        {
            _product = product;
            _unitOfWork = unitOfWork;
            _productFile = productFile;
        }
        public async Task<Unit> Handle(UploadProductFileCommand request, CancellationToken cancellationToken)
        {
            Product? currentProduct = await _product.GetProductByProductName(request.productName);

            if (currentProduct is null)
            {
                throw new NotFoundException($"The product ${request.productName} was not found");
            }


            if (!AreFilesValid(request.files))
            {
                throw new ProductException("The file is required");
            }

            _product.ChangeProductStateToUnchanged(currentProduct);

            await _productFile.UploadProductFileAsync(currentProduct, request.files!);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private bool AreFilesValid(List<IFormFile>? files)
        {
            return files is not null && files.Any();
        }
    }
}
