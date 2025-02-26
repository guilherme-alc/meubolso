using System.ComponentModel.DataAnnotations;

namespace MeuBolso.Core.Requests.Categories
{
    public class GetCategoryByIdRequest : Request
    {
        [Required(ErrorMessage = "O ID da categoria é necessário.")]
        public long Id { get; set; }
    }
}
