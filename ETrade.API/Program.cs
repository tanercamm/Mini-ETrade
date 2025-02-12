using ETrade.Application.Services.Abstract;
using ETrade.Application.Services.Concrete;
using ETrade.Domain.Repositories;
using ETrade.Domain.Repositories.Customer;
using ETrade.Domain.Repositories.Order;
using ETrade.Domain.Repositories.Product;
using ETrade.Infrastructure.Repositories;
using ETrade.Infrastructure.Repositories.Customer;
using ETrade.Infrastructure.Repositories.Order;
using ETrade.Infrastructure.Repositories.Product;
using ETrade.Persistence.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace ETrade.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddOpenApi();

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            // Database connection
            builder.Services.AddDbContext<ETradeDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL") + ";TrustServerCertificate=True"));


            // Dependency Injection
            builder.Services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
            builder.Services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));

            builder.Services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            builder.Services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();

            builder.Services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            builder.Services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();

            builder.Services.AddScoped<IProductReadRepository, ProductReadRepository>();
            builder.Services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IProductService, ProductService>();

            // Swagger'ý ekleyelim
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ETrade API",
                    Version = "v1",
                    Description = "ETrade e-ticaret API dokümantasyonu",
                    Contact = new OpenApiContact
                    {
                        Name = "Destek",
                        Email = "destek@etrade.com",
                        Url = new Uri("https://etrade.com")
                    }
                });
            });

            var app = builder.Build();

            // Geliþtirme ortamýnda Swagger'ý aç
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "ETrade API v1");
                    options.RoutePrefix = string.Empty; // Ana sayfada Swagger'ý aç
                });
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
