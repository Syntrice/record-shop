﻿using Microsoft.EntityFrameworkCore;
using RecordShop.Model;
using RecordShop.Repositories;
using RecordShop.Services;

namespace RecordShop
{
    public static class WebApplicationBuilderExtensions
    {
        public static void SetupDbContext(this WebApplicationBuilder builder)
        {
            if (builder.Configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                builder.Services.AddDbContext<RecordShopDbContext>(options => options.UseInMemoryDatabase("RecordShopDb"));
                Console.WriteLine("Using in memory database");
                return;
            }

            string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            if (connectionString == null)
            {
                Console.WriteLine("Using sqlserver database");
                throw new InvalidOperationException("DefaultConnection string not found in configuration. Please provide a connections string, or set UseInMemoryDatabase to true.");
            }

            builder.Services.AddDbContext<RecordShopDbContext>(options => options.UseSqlServer(connectionString));
        }

        public static void SetupRepositories(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IRecordsRepository, RecordsRepository>();
            builder.Services.AddScoped<IGenresRepository, GenresRepository>();
        }

        public static void SetupServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IGenresService, GenresService>();
            builder.Services.AddScoped<IRecordsService, RecordsService>();
        }
    }
}
