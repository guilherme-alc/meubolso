using MeuBolso.Api.Common.Api;
using MeuBolso.Api.Endpoints.Categories;
using MeuBolso.Api.Endpoints.Transactions;

namespace MeuBolso.Api.Endpoints
{
    public static class Endpoint
    {
        // Método de extensão para mapear os endpoints da aplicação
        public static void MapEndpoints(this WebApplication app)
        {
            // Grupos de rotas
            var endpoints = app.MapGroup("");

            endpoints.MapGroup("v1/categories")
                .WithTags("Categories")
                //.RequireAuthorization()
                .MapEndpoint<CreateCategoryEndpoint>()
                .MapEndpoint<UpdateCategoryEndpoint>()
                .MapEndpoint<DeleteCategoryEndpoint>()
                .MapEndpoint<GetCategorieByIdEndpoint>()
                .MapEndpoint<GetAllCategoriesEndpoint>();

            endpoints.MapGroup("v1/categories")
                .WithTags("Categories")
                //.RequireAuthorization()
                .MapEndpoint<CreateTransactionEndpoint>()
                .MapEndpoint<UpdateTransactionEndpoint>()
                .MapEndpoint<DeleteTransactionEndpoint>()
                .MapEndpoint<GetTransactionByIdEndpoint>()
                .MapEndpoint<GetTransactionsByPeriodEndpoint>();
        }

        private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
            where TEndpoint : IEndpoint
        {
            TEndpoint.Map(app);
            return app;
        }
    }
}
