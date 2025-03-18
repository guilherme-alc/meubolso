using MeuBolso.Api.Common.Api;
using MeuBolso.Core.Handlers;
using MeuBolso.Core.Models;
using MeuBolso.Core.Requests.Transactions;
using MeuBolso.Core.Responses;

namespace MeuBolso.Api.Endpoints.Transactions
{
    public class CreateTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPost("/", HandleAsync)
                .WithName("Transactions: Create")
                .WithSummary("Cria uma nova transação")
                .WithDescription("Cria uma nova transação")
                .WithOrder(1)
                .Produces<Response<Transaction?>>(201);
        }

        private static async Task<IResult> HandleAsync(
            ITransactionHandler handler,
            CreateTransactionRequest request)
        {
            request.UserId = "teste@teste.com";

            var result = await handler.CreateAsync(request);

            if (result.IsSuccess)
                return Results.Created($"/{result.Data?.Id}", result);

            return Results.BadRequest(result);
        }
    }
}
