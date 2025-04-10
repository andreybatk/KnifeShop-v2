using KnifeShop.API.DiContainer;
using KnifeShop.BL.Helpers;
using KnifeShop.BL.DiContainer;
using KnifeShop.DB;
using KnifeShop.DB.DiContainer;
using KnifeShop.DB.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

namespace KnifeShop.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString(
                "DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            var admins = builder.Configuration.GetSection("Admins:admin").Get<List<string>>();

            var authenticationConfiguration = new AuthenticationConfiguration();
            builder.Configuration.Bind("Authentication", authenticationConfiguration);
            builder.Services.AddSingleton(authenticationConfiguration);

            builder.Services.AddControllers();
            //builder.Services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            //})
            //.AddCookie()
            //.AddGoogle(options =>
            //{
            //    options.ClientId = builder.Configuration.GetSection("Google:ClientId").Get<string>() ?? throw new InvalidOperationException("Google 'ClientId' not found.");
            //    options.ClientSecret = builder.Configuration.GetSection("Google:ClientSecret").Get<string>() ?? throw new InvalidOperationException("Google 'ClientSecret' not found.");

            //    options.Events.OnCreatingTicket = context =>
            //    {
            //        var claims = context.Identity?.Claims?.ToList() ?? new List<Claim>();
            //        var email = context.Identity?.Name;

            //        if (admins != null && email != null && admins.Contains(email))
            //        {
            //            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            //        }
            //        else
            //        {
            //            claims.Add(new Claim(ClaimTypes.Role, "User"));
            //        }

            //        context.Identity?.AddClaims(claims);
            //        return Task.CompletedTask;
            //    };
            //});

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Введите JWT токен, начиная с Bearer. Пример: Bearer {your token}"
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
                        Array.Empty<string>()
                    }
                });
            });
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            builder.Services.AddIdentityCore<User>(options =>
            {
                options.User.RequireUniqueEmail = true;

                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddRepositories();
            builder.Services.AddServices();

            builder.Services.AddValidators();

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin();
                    //policy.WithOrigins("http://localhost:3000");
                    policy.AllowAnyHeader();
                });
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationConfiguration.AccessTokenSecret)),
                    ValidIssuer = authenticationConfiguration.Issuer,
                    ValidAudience = authenticationConfiguration.Audience,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Uploads")),
                RequestPath = "/Uploads"
            });

            app.UseHttpsRedirection();

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
