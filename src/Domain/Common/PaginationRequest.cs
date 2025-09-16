using System.ComponentModel.DataAnnotations;

namespace Domain.Common
{
    public class PaginationRequest
    {
        [Display(Name = "pagina")]
        public int PageNumber { get; set; } = 1;
        [Display(Name = "qtdRegistrosPorPagina")]
        public int RecordsQtyPerPage { get; set; } = 20;
    }
}
