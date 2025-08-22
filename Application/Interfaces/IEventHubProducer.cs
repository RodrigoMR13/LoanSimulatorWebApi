namespace Application.Interfaces
{
    public interface IEventHubProducer
    {
        Task SendMessageAsync(string message);
    }
}
