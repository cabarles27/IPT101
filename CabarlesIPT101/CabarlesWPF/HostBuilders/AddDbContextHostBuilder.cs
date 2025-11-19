using Cabarles_IPT.Framework.DbContextFactory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CabarlesWPF.HostBuilders
{
    public static class AddDbContextHostBuilder
    {
        public static IHostBuilder AddDbContext(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((context, services) =>
            {
                string connectionString = context.Configuration.GetConnectionString("DefaultConnection");
                services.AddSingleton(new PosDbContextFactory(connectionString));
            });

            return hostBuilder;
        }
    }
}
