using Application.Dtos.Requests;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoanSimulatorWebAPI.Controllers
{
    [ApiController]
    [Route("emprestimos/v1/simulacoes")]
    // [Authorize]
    public class LoanSimulatorController(IMediator mediator, ILogger<LoanSimulatorController> logger) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<LoanSimulatorController> _logger = logger;

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationRequest paginationRequest)
        {
            var request = new GetAllSimulationsRequest(paginationRequest);

            var result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpGet("volume")]
        public async Task<IActionResult> GetVolume([FromQuery(Name = "dataReferencia")] DateOnly referenceDate)
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
