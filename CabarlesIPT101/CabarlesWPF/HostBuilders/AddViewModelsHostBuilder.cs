using CabarlesWPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CabarlesWPF.HostBuilders
{
    public static class AddViewModelsHostBuilder
    {
        public static IHostBuilder AddViewModels(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<MainViewModel>();
            });

            return hostBuilder;
        }
    }
}
