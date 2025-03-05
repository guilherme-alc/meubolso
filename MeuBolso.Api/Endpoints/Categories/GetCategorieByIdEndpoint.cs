using MeuBolso.Api.Common.Api;
using MeuBolso.Core.Handlers;
using MeuBolso.Core.Models;
using MeuBolso.Core.Requests.Categories;
using MeuBolso.Core.Responses;

namespace MeuBolso.Api.Endpoints.Categories
{
    public class GetCategorieByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/{id:long}", HandleAsync)
                .WithName("Categories: Get By Id")
                .WithSummary("Recupera uma categoria")
                .WithDescription("Recupera uma categoria")
                .WithOrder(4)
                .Produces<Response<Category?>>();
        }

        private static async Task<IResult> HandleAsync(
            ICategoryHandler handler,
            long id)
        {
            var request = new GetCategoryByIdRequest()
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
