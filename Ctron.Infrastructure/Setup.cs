using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;


namespace Ctron.Infrastructure
{
    public static class Setup
    {
        public static void AddDbContext(this IServiceCollection services, string connectionString) =>
            services.AddDbContext<AppDbContext>(
                options =>
                options.UseMySql(connectionString, x => x.MigrationsAssembly("Ctron.Infrastructure")));
    }
}