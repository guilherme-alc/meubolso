using MeuBolso.Api.Data;
using MeuBolso.Api.Handlers;
using MeuBolso.Core.Handlers;
using MeuBolso.Core.Models;
using MeuBolso.Core.Requests.Categories;
using MeuBolso.Core.Responses;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder
    .Configuration
    .GetConnectionString("DefaultConnection") ?? string.Empty;

builder.Services.AddDbContext<AppDbContext>(
        opts =>
        {
            opts.UseSqlServer(connectionString);
        }
    );

builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();

// Adiciona suporte para Open Api
builder.Services.AddEndpointsApiExplorer();
// Adiciona suporte a interface do Swagger
builder.Services.AddSwaggerGen(opts =>
{
    // Full Qualified Name
    opts.CustomSchemaIds(type => type.FullName);
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/v1/categories", 
    async(CreateCategoryRequest request, ICategoryHandler handler) 
        => await handler.CreateAsync(request))
    .WithName("Categories: Create")
    .WithSummary("Cria uma nova categoria")
    .Produces<Response<Category?>>();

app.MapPut(
    "/v1/categories/{id:long}", 
    async(long id, UpdateCategoryRequest request, ICategoryHandler handler)
        =>
    {
        request.Id = id;
        await handler.UpdateAsync(request);
    })
    .WithName("Categories: Update")
    .WithSummary("Atualiza uma categoria")
    .Produces<Response<Category?>>();

app.MapDelete(
    "/v1/categories/{id:long}",
    async (long id, ICategoryHandler handler)
        =>
    {
        var request = new DeleteCategoryRequest() { Id = id};
        request.Id = id;
        await handler.DeleteAsync(request);
    })
    .WithName("Categories: Delete")
    .WithSummary("Exclui uma categoria")
    .Produces<Response<Category?>>();

app.Run();