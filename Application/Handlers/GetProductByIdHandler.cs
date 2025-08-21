using Application.Dtos.Requests;
using Application.Exceptions;
using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Handlers
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdRequest,
        ApiResponse<Product>>
    {
        private readonly IProductRepository _repository;

        public GetProductByIdHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ApiResponse<Product>> Handle(
            GetProductByIdRequest request,
            CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id);
            return product == null
                ? throw new ProductNotFoundException(request.Id)
                : ApiResponse<Product>.Ok(product, "Produtos retornados com sucesso");
        }
    }
}
