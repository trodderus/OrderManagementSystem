using MediatR;
using OrderManagementSystem.Application.DTOs;
using OrderManagementSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Application.Entities.Products.Queries
{
    public class GetAllProductsHandler(IProductRepository repo) : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
    {
        private readonly IProductRepository _repo = repo;

        public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken ct)
        {
            var products = await _repo.GetAllAsync();
            return products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.StockQuantity)).ToList();
        }
    }
}
