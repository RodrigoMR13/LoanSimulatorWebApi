namespace Domain.Common
{
    public class PaginationRequest
    {
        public int PageNumber { get; set; } = 1;
        public int RecordsQtyPerPage { get; set; } = 20;
    }
}
