using MeuBolso.Api.Common.Api;
using MeuBolso.Core.Handlers;
using MeuBolso.Core.Models;
using MeuBolso.Core.Requests.Categories;
using MeuBolso.Core.Responses;

namespace MeuBolso.Api.Endpoints.Categories
{
    public class UpdateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPut("/{id:long}", HandleAsync)
                .WithName("Categories: Update")
                .WithSummary("Atualiza uma categoria")
                .WithDescription("Atualiza uma categoria")
                .WithOrder(2)
                .Produces<Response<Category?>>();
        }

        private static async Task<IResult> HandleAsync(
            ICategoryHandler handler, 
            UpdateCategoryRequest request,
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
