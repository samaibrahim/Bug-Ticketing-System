
using BugTicketingSystem.BL;
using BugTicketingSystem.DAL;

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
