using Application.Dtos.Requests;
using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Handlers
{
    public class GetAllProductsHandler(IProductRepository repository) : IRequestHandler<GetAllProductsRequest,
        ApiResponse<IEnumerable<Product>>>
    {
        private readonly IProductRepository _repository = repository;

        public async Task<ApiResponse<IEnumerable<Product>>> Handle(
            GetAllProductsRequest request,
            CancellationToken cancellationToken)
        {
            var products = await _repository.GetAllAsync();
            return ApiResponse<IEnumerable<Product>>.Ok(products, "Produtos retornados com sucesso");
        }
    }
}
