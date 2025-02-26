using System.ComponentModel.DataAnnotations;

namespace MeuBolso.Core.Requests.Categories
{
    public class UpdateCategoryRequest : Request
    {
        [Required(ErrorMessage = "O título é necessário.")]
        [StringLength(80, MinimumLength = 4, ErrorMessage = "O título deve conter de 4 até até 80 caracteres.")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "A descrição é necessária.")]
        public string Description { get; set; } = string.Empty;
    }
}
