using System.Collections.Concurrent;

namespace Application.OpenTelemetry
{
    public class TelemetryStorage
    {
        private readonly ConcurrentDictionary<string, List<long>> _durations = new();
        private readonly ConcurrentDictionary<string, int> _totalRequests = new();
        private readonly ConcurrentDictionary<string, int> _failedRequests = new();
        public void Record(string endpoint, long durationMs, bool success)
        {
            _durations.AddOrUpdate(endpoint,
                _ => new List<long> { durationMs },
                (_, list) =>
                {
                    list.Add(durationMs);
                    return list;
                });

            _totalRequests.AddOrUpdate(endpoint, 1, (_, old) => old + 1);
            if (!success)
                _failedRequests.AddOrUpdate(endpoint, 1, (_, old) => old + 1);
        }

        public IEnumerable<EndpointTelemetryDto> GetAllTelemetry()
        {
            foreach (var endpoint in _totalRequests.Keys)
            {
                var durations = _durations[endpoint];
                var total = _totalRequests[endpoint];
                var failures = _failedRequests.GetValueOrDefault(endpoint, 0);
                yield return new EndpointTelemetryDto
                {
                    NomeApi = endpoint,
                    QtdRequisicoes = total,
                    TempoMedioResposta = durations.Average(),
                    TempoMinimoResposta = durations.Min(),
                    TempoMaximoResposta = durations.Max(),
                    PercentualSucesso = total == 0 ? 1 : (double)(total - failures) / total
                };
            }
        }
    }
}
