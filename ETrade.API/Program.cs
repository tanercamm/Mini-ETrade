
using ETrade.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ETrade.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddOpenApi();

            // Database connection
            builder.Services.AddDbContext<ETradeDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL") + ";TrustServerCertificate=True"));

            var app = builder.Build();

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
