using Application.Dtos.Requests;
using Application.Exceptions;
using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Handlers
{
    public class GetProductByIdHandler(IProductRepository repository) : IRequestHandler<GetProductByIdRequest, ApiResponse<Product>>
    {
        private readonly IProductRepository _repository = repository;

        public async Task<ApiResponse<Product>> Handle(
            GetProductByIdRequest request,
            CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id) ?? throw new ProductNotFoundException(request.Id);
            return ApiResponse<Product>.Ok(product, "Produto retornado com sucesso");
        }
    }
}
