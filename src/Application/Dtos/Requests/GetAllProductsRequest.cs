using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Application.Dtos.Requests
{
    public class GetAllProductsRequest : IRequest<ApiResponse<IEnumerable<Product>>> { }
}
