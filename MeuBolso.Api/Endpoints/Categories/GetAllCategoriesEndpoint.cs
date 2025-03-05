using MeuBolso.Api.Common.Api;
using MeuBolso.Core;
using MeuBolso.Core.Handlers;
using MeuBolso.Core.Models;
using MeuBolso.Core.Requests.Categories;
using MeuBolso.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace MeuBolso.Api.Endpoints.Categories
{
    public class GetAllCategoriesEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/", HandleAsync)
                .WithName("Categories: Get All")
                .WithSummary("Recupera todas as categorias")
                .WithDescription("Recupera todas as categorias")
                .WithOrder(5)
                .Produces<PagedResponse<List<Category>?>>();
        }

        private static async Task<IResult> HandleAsync(
            ICategoryHandler handler,
            [FromQuery] int pageNumber = Configuration.DEFAULT_PAGE_NUMBER,
            [FromQuery] int pageSize = Configuration.DEFAULT_PAGE_SIZE)
        {
            var request = new GetAllCategoriesRequest()
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await handler.GetAllAsync(request);

            if (result.IsSuccess)
                return Results.Ok(result);

            return Results.BadRequest(result);
        }
    }
}
