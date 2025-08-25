using Application.Dtos.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoanSimulatorWebAPI.Controllers
{
    [ApiController]
    [Route("emprestimos/v1/produtos")]
    [Authorize]
    public class ProductsController(IMediator mediator, ILogger<ProductsController> logger) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<ProductsController> _logger = logger;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var request = new GetAllProductsRequest();

            var response = await _mediator.Send(request);

            return Ok(response.Data);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var request = new GetProductByIdRequest(id);

            var response = await _mediator.Send(request);

            return Ok(response.Data);
        }
    }
}
