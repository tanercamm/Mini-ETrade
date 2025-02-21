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
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ETrade.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ETrade.Persistence.Configurations;
using ETrade.Domain.Repositories.User;
using ETrade.Persistence.Repositories.User;

namespace ETrade.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            // Database connection
            builder.Services.AddDbContext<ETradeDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL") + ";TrustServerCertificate=True"));

            // Identity Configuration (Guid ID'li)
            builder.Services.AddIdentity<User, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<ETradeDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
            });

            // Authentication (JWT)
            var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Secret"]);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false; // Lokal geliþtirmede HTTPS zorunluluðunu kaldýrýr.
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    ClockSkew = TimeSpan.Zero // Token süresi biter bitmez geçersiz olur.
                };
            });

            // Dependency Injection
            builder.Services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
            builder.Services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));

            builder.Services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            builder.Services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();

            builder.Services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            builder.Services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();

            builder.Services.AddScoped<IProductReadRepository, ProductReadRepository>();
            builder.Services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

            builder.Services.AddScoped<IUserReadRepository, UserReadRepository>();
            builder.Services.AddScoped<IUserWriteRepository, UserWriteRepository>();

            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IAuthService, AuthService>();

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

                // JWT Authentication Desteði
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Token girin. Örn: Bearer {token}"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            var app = builder.Build();

            // Roller otomatik eklensin
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                await IdentityConfiguration.SeedRolesAsync(services);
            }

            app.UseDeveloperExceptionPage(); // Hatalarýn ayrýntýlý görünmesi için

            // Geliþtirme ortamýnda Swagger'ý aç
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();   
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "ETrade API v1");
                    options.RoutePrefix = string.Empty; // Ana sayfada Swagger'ý aç
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
