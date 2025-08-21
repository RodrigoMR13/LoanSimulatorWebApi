using Application.Dtos.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LoanSimulatorWebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/emprestimos/produtos")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var request = new GetAllProductsRequest();

            var response = await _mediator.Send(request);

            return Ok(response.Data);
        }
    }
}
