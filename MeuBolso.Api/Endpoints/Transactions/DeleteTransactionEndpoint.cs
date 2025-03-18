using MeuBolso.Api.Common.Api;
using MeuBolso.Core.Handlers;
using MeuBolso.Core.Models;
using MeuBolso.Core.Requests.Transactions;
using MeuBolso.Core.Responses;

namespace MeuBolso.Api.Endpoints.Transactions
{
    public class DeleteTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapDelete("/{id:long}", HandleAsync)
                .WithName("Transactions: Delete")
                .WithSummary("Exclui uma transação")
                .WithDescription("Exclui uma transação")
                .WithOrder(3)
                .Produces<Response<Transaction?>>();
        }

        private static async Task<IResult> HandleAsync(
            ITransactionHandler handler,
            long id)
        {
            var request = new DeleteTransactionRequest()
            {
                UserId = "teste@teste.com",
                Id = id
            };

            var result = await handler.DeleteAsync(request);

            if (result.IsSuccess)
                return Results.Ok(result);

            return Results.BadRequest(result);
        }
    }
}
