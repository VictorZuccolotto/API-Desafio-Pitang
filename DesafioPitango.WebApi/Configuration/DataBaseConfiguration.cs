﻿using Microsoft.EntityFrameworkCore;
using DesafioPitang.Repository;

namespace DesafioPitang.WebApi.Configuration
{
    public static class DataBaseConfiguration
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Context>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
