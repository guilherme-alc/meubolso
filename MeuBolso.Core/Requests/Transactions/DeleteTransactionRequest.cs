using System.ComponentModel.DataAnnotations;

namespace MeuBolso.Core.Requests.Transactions
{
    public class DeleteTransactionRequest : Request
    {
        [Required(ErrorMessage = "O ID da transação é necessário.")]
        public long Id { get; set; }
    }
}
