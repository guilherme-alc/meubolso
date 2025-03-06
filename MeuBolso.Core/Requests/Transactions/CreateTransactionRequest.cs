using MeuBolso.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace MeuBolso.Core.Requests.Transactions
{
    public class CreateTransactionRequest : Request
    {
        [Required(ErrorMessage = "O título é necessário")]
        [StringLength(180, MinimumLength = 3, ErrorMessage = "O título deve conter entre 3 À 180 caracteres")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "A quantia é necessária")]
        public decimal Amount { get; set; }
        [Required (ErrorMessage = "O Id da categoria é necessário")]
        public long CategoryId { get; set; }
        [Required(ErrorMessage = "Data inválida")]
        public DateTime? PaidOrReceivedAt { get; set; }
        [Required(ErrorMessage = "O título é necessário")]
        public ETransactionType Type { get; set; }
    }
}
