using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Application.Features.Product.Commands.CreateProductCommand
{
    public record CreateProductCommandDTO
    {
        public string ProductName { get; init; } = string.Empty;

        public string Description { get; init; } = string.Empty;

        public string ProductImageUrl { get; init; } = string.Empty;

        public double ProductPrice { get; init; }

        public int ProductQuantity { get; init; }

    }
}
