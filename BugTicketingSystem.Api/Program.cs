
using BugTicketingSystem.BL;
using BugTicketingSystem.DAL;
using BugTicketingSystem.DAL.Context;
using BugTicketingSystem.DAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.FileProviders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace BugTicketingSystem.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
           
            builder.Services.AddOpenApi();

            builder.Services.AddDataAccessServices(builder.Configuration);
            builder.Services.AddBusinessServices(builder.Configuration);

            builder.Services.AddIdentity<Users, IdentityRole<Guid>>(options =>
            {
                // Password validation settings
                options.Password.RequiredUniqueChars = 2;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;

                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<BugTicketingSystemContext>()
            .AddDefaultTokenProviders();


            //var jwtSettings = builder.Configuration.GetSection("Jwt");
            //var key = jwtSettings.GetValue<string>("Key");
            //var issuer = jwtSettings.GetValue<string>("Issuer");
            //var audience = jwtSettings.GetValue<string>("Audience");

            ////for authorize
            //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            // .AddJwtBearer(options =>
            // {
            //     options.TokenValidationParameters = new TokenValidationParameters
            //     {
            //         ValidateIssuer = true,
            //         ValidateAudience = true,
            //         ValidateLifetime = true,
            //         ValidateIssuerSigningKey = true,
            //         ValidIssuer = issuer,   //  appsettings
            //         ValidAudience = audience, //  appsettings
            //         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
            //     };
            // });
            //builder.Services.AddAuthorization();


            //authorize
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });
            //







            var app = builder.Build();

            

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();
            app.Use(async (context, next) =>
            {
                if (!context.User.Identity.IsAuthenticated)
                {
                    // Log the authentication failure or inspect further
                    Console.WriteLine("Authentication failed");
                }
                await next();
            });

            // Allow serving files from the uploads folder
            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

            if (!Directory.Exists(uploadsPath))
            {
                Directory.CreateDirectory(uploadsPath);
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(uploadsPath),
                RequestPath = "/uploads"
            });
            app.UseStaticFiles();
            app.MapControllers();

            app.Run();
        }
    }
}
