namespace Application.Exceptions
{
    public class NoSuitableProductFoundException(string? message = null) : Exception(message ?? "Nenhum produto atende às condições especificadas.")
    {
    }
}