using SCITest.Application.Handlers.Commands.Products;
using SCITest.Application.Handlers.Queries.Products;
using SCITest.Application.Interfaces.Repositories;
using SCITest.Domain.Services;
using SCITest.Infrastructure.Data.Interfaces;
using SCITest.Infrastructure.Data;
using SCITest.Infrastructure.Repositories.Products;
using SCITest.Application.Validators.Products;
using SCITest.Api.Middleware;
using SCITest.Application.Handlers.Queries.Weather;
using SCITest.Infrastructure.ExternalServices.Weather;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration
    .GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "DefaultConnection is not configured.");

builder.Services.AddSingleton<IDbConnectionFactory>(
    _ => new DbConnectionFactory(connectionString));

builder.Services.Configure<WeatherServiceOption>(
    builder.Configuration.GetSection("ExternalApis:Weather"));

builder.Services.AddHttpClient<IWeatherService, WeatherService>();

builder.Services.AddScoped<GetWeatherHandler>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<ProductService>();

builder.Services.AddScoped<CreateProductHandler>();
builder.Services.AddScoped<UpdateProductHandler>();
builder.Services.AddScoped<DeleteProductHandler>();
builder.Services.AddScoped<GetProductsHandler>();
builder.Services.AddScoped<GetProductByIdHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();