using System.ComponentModel.DataAnnotations;

namespace MeuBolso.Core.Requests.Transactions
{
    public class GetTransactionByIdRequest : Request
    {
        [Required(ErrorMessage = "O ID da transação é necessário.")]
        public long Id { get; set; }
    }
}
