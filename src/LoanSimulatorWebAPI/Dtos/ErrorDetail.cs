namespace LoanSimulatorWebAPI.Dtos
{
    public class ErrorDetail
    {
        public string Field { get; set; } = default!;
        public string Message { get; set; } = default!;
    }
}
