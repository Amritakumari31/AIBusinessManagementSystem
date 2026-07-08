using Backend.Application.Interfaces.Repositories;
using Backend.Application.Interfaces.Services;
using Backend.Application.Services;
using Backend.Infrastructure.Persistence;
using Backend.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
namespace Backend.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")));
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            
            builder.Services.AddScoped<IEmployeeRepository,EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
