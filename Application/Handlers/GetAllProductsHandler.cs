using Application.Dtos.Requests;
using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers
{
    public class GetAllProductsHandler(IProductRepository repository, ILogger<GetAllProductsHandler> logger) : IRequestHandler<GetAllProductsRequest,
        ApiResponse<IEnumerable<Product>>>
    {
        private readonly IProductRepository _repository = repository;
        private readonly ILogger<GetAllProductsHandler> _logger = logger;

        public async Task<ApiResponse<IEnumerable<Product>>> Handle(
            GetAllProductsRequest request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Obtendo todos os produtos no Banco de Dados...");
            var products = await _repository.GetAllAsync();
            return ApiResponse<IEnumerable<Product>>.Ok(products, "Produtos retornados com sucesso");
        }
    }
}
