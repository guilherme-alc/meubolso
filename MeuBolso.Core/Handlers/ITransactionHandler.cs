using MeuBolso.Core.Models;
using MeuBolso.Core.Requests.Transactions;
using MeuBolso.Core.Responses;

namespace MeuBolso.Core.Handlers
{
    public interface ITransactionHandler
    {
        Task<PagedResponse<List<Transaction>>> GetAllAsync(GetAllTransactionsRequest request);
        Task<PagedResponse<List<Transaction>>> GetByPeriod(GetTransactionsByPeriodRequest request);
        Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request);
        Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request);
        Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request);
        Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request);
    }
}
