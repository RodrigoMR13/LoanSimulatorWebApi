using Application.Dtos.Requests;
using Application.Exceptions;
using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers
{
    public class GetProductByIdHandler(IProductRepository repository, ILogger<GetProductByIdHandler> logger) : IRequestHandler<GetProductByIdRequest,
        ApiResponse<Product>>
    {
        private readonly IProductRepository _repository = repository;
        private readonly ILogger<GetProductByIdHandler> _logger = logger;

        public async Task<ApiResponse<Product>> Handle(
            GetProductByIdRequest request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Obtendo produto de id {request.Id} no Banco de Dados...");
            var product = await _repository.GetByIdAsync(request.Id);
            return product == null
                ? throw new ProductNotFoundException(request.Id)
                : ApiResponse<Product>.Ok(product, "Produtos retornados com sucesso");
        }
    }
}
