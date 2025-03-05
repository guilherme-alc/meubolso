using MeuBolso.Api.Common.Api;
using MeuBolso.Core.Handlers;
using MeuBolso.Core.Models;
using MeuBolso.Core.Requests.Categories;
using MeuBolso.Core.Responses;

namespace MeuBolso.Api.Endpoints.Categories
{
    public class DeleteCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapDelete("/{id:long}", HandleAsync)
                .WithName("Categories: Delete")
                .WithSummary("Exclui uma categoria")
                .WithDescription("Exclui uma categoria")
                .WithOrder(3)
                .Produces<Response<Category?>>();
        }

        private static async Task<IResult> HandleAsync(
            ICategoryHandler handler,
            long id)
        {
            var request = new DeleteCategoryRequest()
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
