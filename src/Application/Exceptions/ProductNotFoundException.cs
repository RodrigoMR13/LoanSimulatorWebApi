namespace Application.Exceptions
{
    public class ProductNotFoundException(int id) : Exception($"Produto com Id {id} não foi encontrado.")
    {
        public int Id { get; set; } = id;
    }
}
