namespace LoanSimulatorWebAPI.Dtos
{
    public class ErrorResponse
    {
        public string RequestId { get; set; } = default!;
        public int StatusCode { get; set; }
        public string Message { get; set; } = default!;
        public List<ErrorDetail> Errors { get; set; } = [];
    }
}
