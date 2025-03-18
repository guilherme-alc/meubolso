using MeuBolso.Api.Common.Api;
using MeuBolso.Core.Handlers;
using MeuBolso.Core.Models;
using MeuBolso.Core.Requests.Transactions;
using MeuBolso.Core.Responses;

namespace MeuBolso.Api.Endpoints.Transactions
{
    public class UpdateTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPut("/{id:long}", HandleAsync)
                .WithName("Transaction: Update")
                .WithSummary("Atualiza uma transação")
                .WithDescription("Atualiza uma transação")
                .WithOrder(2)
                .Produces<Response<Transaction?>>();
        }

        private static async Task<IResult> HandleAsync(
            ITransactionHandler handler,
            UpdateTransactionRequest request,
            long id)
        {
            request.UserId = "teste@teste.com";
            request.Id = id;

            var result = await handler.UpdateAsync(request);

            if (result.IsSuccess)
                return Results.Ok(result);

            return Results.BadRequest(result);
        }
    }
}
