using System;
using System.Threading;
using System.Threading.Tasks;
using Ctron.API.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Ctron.API.Services
{
    public class CustomStartService : IHostedService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        //private AdminConfiguration _admin;
        public CustomStartService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            //_admin = admin;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var adminConf = scope.ServiceProvider.GetRequiredService<AdminConfiguration>();
                await adminConf.SeedAdminAndRoles();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        public void Dispose() { }
    }
}