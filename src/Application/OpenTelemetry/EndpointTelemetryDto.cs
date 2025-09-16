namespace Application.OpenTelemetry
{
    public class EndpointTelemetryDto
    {
        public string NomeApi { get; set; } = string.Empty;
        public int QtdRequisicoes { get; set; }
        public double TempoMedioResposta { get; set; }
        public double TempoMinimoResposta { get; set; }
        public double TempoMaximoResposta { get; set; }
        public double PercentualSucesso { get; set; }
    }
}
