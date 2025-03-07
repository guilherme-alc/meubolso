using MeuBolso.Api.Data;
using MeuBolso.Core.Common.Extensions;
using MeuBolso.Core.Handlers;
using MeuBolso.Core.Models;
using MeuBolso.Core.Requests.Transactions;
using MeuBolso.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace MeuBolso.Api.Handlers
{
    public class TransactionHandler : ITransactionHandler
    {
        public TransactionHandler(AppDbContext context)
        {
            _context = context;
        }
        private readonly AppDbContext _context;
        public async Task<PagedResponse<List<Transaction>>> GetAllAsync(GetAllTransactionsRequest request)
        {

            var query = _context
                .Transactions
                .AsNoTracking()
                .OrderBy(t => t.CreatedAt);

            var transactions = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();


            return new PagedResponse<List<Transaction>>(transactions, count, request.PageNumber, request.PageSize);
        }

        public async Task<PagedResponse<List<Transaction>>> GetByPeriod(GetTransactionsByPeriodRequest request)
        {
            try
            {
                request.StartDate ??= DateTime.Now.GetFirstDay();
                request.EndDate ??= DateTime.Now.GetLastDay();
            }
            catch
            {
                return new PagedResponse<List<Transaction>>(null, 500, "Não foi possível determinar a data de início ou término");
            }
            var query = _context
                .Transactions
                .AsNoTracking()
                .Where(t => t.CreatedAt >= request.StartDate && t.CreatedAt <= request.EndDate && t.UserId == request.UserId)
                .OrderBy(t => t.CreatedAt);

            var transactions = await query
                
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();

            return new PagedResponse<List<Transaction>>(transactions, count, request.PageNumber, request.PageSize);
        }

        public async Task<Response<Transaction?>> GetByIdAsync (GetTransactionByIdRequest request)
        {
            try
            {
                var transaction = await _context
                    .Transactions
                    .AsNoTracking()
                    .FirstOrDefaultAsync(t => t.Id == request.Id && t.UserId == request.UserId);

                if (transaction is null)
                    return new Response<Transaction?>(null, 404, "Transação não encontrada");

                return new Response<Transaction?>(transaction, 200);
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Não foi possível buscar a transação");
            }
        }

        public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
        {
            try
            {
                var transaction = new Transaction()
                {
                    UserId = request.UserId,
                    Title = request.Title,
                    Amount = request.Amount,
                    CategoryId = request.CategoryId,
                    CreatedAt = DateTime.Now,
                    PaidOrReceivedAt = request.PaidOrReceivedAt,
                    Type = request.Type
                };

                await _context.Transactions.AddAsync(transaction);
                await _context.SaveChangesAsync();

                return new Response<Transaction?>(transaction, 201, "Transação criada com sucesso");
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Não foi possível criar a transação.");
            }
        }

        public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
        {
            try
            {
                var transaction = await _context
                    .Transactions
                    .FirstOrDefaultAsync(t => t.Id == request.Id && t.UserId == request.UserId);

                if(transaction is null)
                    return new Response<Transaction?>(null, 404, "Transação não encontrada.");

                transaction.Title = string.IsNullOrWhiteSpace(request.Title) ? transaction.Title : request.Title;
                transaction.Amount = request.Amount;
                transaction.CategoryId = request.CategoryId;
                transaction.PaidOrReceivedAt = request.PaidOrReceivedAt;
                transaction.Type = request.Type;

                _context.Transactions.Update(transaction);
                await _context.SaveChangesAsync();

                return new Response<Transaction?>(transaction, 200, "Transação atualizada com sucesso");
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Não foi possível atualizar a transação.");
            }
        }

        public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
        {
            try
            {
                var transaction = await _context
                    .Transactions
                    .FirstOrDefaultAsync(t => t.Id == request.Id && t.UserId == request.UserId);

                if (transaction is null)
                    new Response<Transaction?>(null, 404, "Transação não encontrada");

                _context.Transactions.Remove(transaction);
                await _context.SaveChangesAsync();

                return new Response<Transaction?>(null, 200, "Transação excluida com sucesso");
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Não foi possível excluir a transação");
            }
        }
    }
}
