using Application.OpenTelemetry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoanSimulatorWebAPI.Controllers
{
    [ApiController]
    [Route("emprestimos/v1/telemetry")]
    [Authorize]
    public class TelemetryController : ControllerBase
    {
        private readonly TelemetryStorage _storage;

        public TelemetryController(TelemetryStorage storage)
        {
            _storage = storage;
        }

        [HttpGet("summary")]
        public IActionResult GetSummary()
        {
            return Ok(new
            {
                dataReferencia = DateTime.Today,
                listaEndpoints = _storage.GetAllTelemetry()
            });
        }
    }
}
