namespace Application.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public int Id { get; set; }

        public ProductNotFoundException(int id) : base($"Produto com Id {id} não foi encontrado.")
        {
            Id = id;
        }
    }
}
