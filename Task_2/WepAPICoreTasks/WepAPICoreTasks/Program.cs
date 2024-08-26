
using Microsoft.EntityFrameworkCore;
using WepAPICoreTasks.Models;

namespace WepAPICoreTasks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            //// على شان يستقبل الفرونت 
            //builder.Services.AddCors(options =>
            //options.AddPolicy("Develpoment", builder =>
            //{
            //    builder.AllowAnyOrigin();
            //    builder.AllowAnyMethod();
            //    builder.AllowAnyHeader();
            //})
            //); // على شان يستقبل الفرونت 

            builder.Services.AddDbContext<MyDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("YourConnectionString")));

            var app = builder.Build();

            //app.UseCors("Develpoment");// على شان يستقبل الفرونت 
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
