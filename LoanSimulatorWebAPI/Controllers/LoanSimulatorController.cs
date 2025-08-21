using Application.Dtos.Requests;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LoanSimulatorWebAPI.Controllers
{
    [ApiController]
    [Route("emprestimos/v1/simulacoes")]
    public class LoanSimulatorController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> Get([FromBody] PaginationRequest paginationRequest)
        {
            var request = new GetAllSimulationsRequest(paginationRequest);   

            var result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpGet("volume")]
        public async Task<IActionResult> GetVolume([FromQuery] DateOnly referenceDate)
        {
            var request = new GetSimulationsVolumeRequest { ReferenceDate = referenceDate };

            var result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpPost("simular")]
        public async Task<IActionResult> Post([FromBody] SimulateLoanRequest request)
        {
            var result = await _mediator.Send(request);

            return Ok(result);
        }
    }
}
