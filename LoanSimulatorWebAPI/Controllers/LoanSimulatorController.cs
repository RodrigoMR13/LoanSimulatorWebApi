using Application.Dtos.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LoanSimulatorWebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/emprestimos")]
    public class LoanSimulatorController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("simular")]
        public async Task<IActionResult> Post([FromBody] SimulateLoanRequest request)
        {
            var result = await _mediator.Send(request);

            return Ok(result);
        }
    }
}
