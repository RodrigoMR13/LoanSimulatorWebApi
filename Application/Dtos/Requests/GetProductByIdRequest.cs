using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Application.Dtos.Requests
{
    public class GetProductByIdRequest : IRequest<ApiResponse<Product>> 
    {
        public int Id { get; set; }
    }
}
