using MeuBolso.Api.Common.Api;
using MeuBolso.Core;
using MeuBolso.Core.Handlers;
using MeuBolso.Core.Models;
using MeuBolso.Core.Requests.Transactions;
using MeuBolso.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace MeuBolso.Api.Endpoints.Transactions
{
    public class GetTransactionsByPeriodEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/", HandleAsync)
                .WithName("Transactions: Get All")
                .WithSummary("Recupera todas as transações")
                .WithDescription("Recupera todas as transações")
                .WithOrder(5)
                .Produces<PagedResponse<List<Transaction>?>>();
        }

        private static async Task<IResult> HandleAsync(
            ITransactionHandler handler,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null,
            [FromQuery] int pageNumber = Configuration.DEFAULT_PAGE_NUMBER,
            [FromQuery] int pageSize = Configuration.DEFAULT_PAGE_SIZE)
        {
            var request = new GetTransactionsByPeriodRequest()
            {
                UserId = "teste@teste.com",
                PageNumber = pageNumber,
                PageSize = pageSize,
                StartDate = startDate,
                EndDate = endDate,
            };

            var result = await handler.GetByPeriod(request);

            if (result.IsSuccess)
                return Results.Ok(result);

            return Results.BadRequest(result);
        }
    }
}
