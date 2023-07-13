using Eshop_Application.Common.Exceptions;
using Eshop_Application.Common.Interfaces;
using Eshop_Domain.Entities.ProductEntities;
using Eshop_Domain.Entities.UserEntities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Application.Features.Products.Commands.CreateProductCommand;

public record CreateProductCommand : IRequest<Unit>
{
    public CreateProductCommand()
    {
        ProductCategory = new HashSet<CreateProductCategory>();
    }

    public string ProductName { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public double ProductPrice { get; init; }

    public int ProductQuantity { get; init; }

    public ICollection<CreateProductCategory> ProductCategory { get; init; } 

}

public record CreateProductCategory
{
    public string CategoryName { get; set; } = null!;
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand,Unit>
{
    private readonly IProductRepository _product;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(
        IProductRepository product,
        IUnitOfWork unitOfWork)
    {
        _product = product;
        _unitOfWork = unitOfWork;
    }
    public async Task<Unit> Handle(CreateProductCommand request,CancellationToken cancellationToken)
    {
        _product.CreateProduct(request);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
