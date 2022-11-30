using Core;
using Core.EventStoreContext;
using Infrastructure.Data.Context;
using Infrastructure.Data.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using OutgoingContext.Food.Repositories;
using OutgoingContext.Product.Domain;
using OutgoingContext.Product.Handlers;
using System.Configuration;
using System.Reflection;
using Google.Cloud.Firestore;
using Core.Events;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "D:\\IEat\\IEat-Backend\\IEatBackend\\WebApi\\firebase_api_key.json");

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddEntityFrameworkNpgsql().AddDbContext<ApplicationContext>(opt =>
                opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnectionString")));
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddScoped<IEventStore, EventStore>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(ProductQueryHandler).Assembly);
            
            var app = builder.Build();

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