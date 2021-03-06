using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace N8T.Infrastructure.EfCore
{
    public class DbContextMigratorHostedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public DbContextMigratorHostedService(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var dbFacadeResolver = scope.ServiceProvider.GetRequiredService<IDbFacadeResolver>();
            await dbFacadeResolver.Database.MigrateAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
