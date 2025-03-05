using MeuBolso.Api.Common.Api;
using MeuBolso.Core.Handlers;
using MeuBolso.Core.Models;
using MeuBolso.Core.Requests.Categories;
using MeuBolso.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace MeuBolso.Api.Endpoints.Categories
{
    public class CreateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPost("/", HandleAsync)
                .WithName("Categories: Create")
                .WithSummary("Cria uma nova categoria")
                .WithDescription("Cria uma nova categoria")
                .WithOrder(1)
                .Produces<Response<Category?>>(201);
        }

        private static async Task<IResult> HandleAsync(
            ICategoryHandler handler, 
            CreateCategoryRequest request)
        {
            request.UserId = "teste@teste.com";

            var result = await handler.CreateAsync(request);

            if(result.IsSuccess)
                return Results.Created($"/{result.Data?.Id}", result);

            return Results.BadRequest(result);
        }
    }
}
