var builder = WebApplication.CreateBuilder(args);
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