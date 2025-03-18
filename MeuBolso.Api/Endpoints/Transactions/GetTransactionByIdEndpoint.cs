using MeuBolso.Api.Common.Api;
using MeuBolso.Core.Handlers;
using MeuBolso.Core.Models;
using MeuBolso.Core.Requests.Transactions;
using MeuBolso.Core.Responses;

namespace MeuBolso.Api.Endpoints.Transactions
{
    public class GetTransactionByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/{id:long}", HandleAsync)
                .WithName("Transactions: Get By Id")
                .WithSummary("Recupera uma transação")
                .WithDescription("Recupera uma transação")
                .WithOrder(4)
                .Produces<Response<Transaction?>>();
        }

        private static async Task<IResult> HandleAsync(
            ITransactionHandler handler,
            long id)
        {
            var request = new GetTransactionByIdRequest()
            {
                UserId = "teste@teste.com",
                Id = id
            };

            var result = await handler.GetByIdAsync(request);

            if (result.IsSuccess)
                return Results.Ok(result);

            return Results.BadRequest(result);
        }
    }
}
