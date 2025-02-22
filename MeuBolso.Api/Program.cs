using MeuBolso.Api.Data;
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

app.MapGet("/", () => "Hello World!");

app.Run();