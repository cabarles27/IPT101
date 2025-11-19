using Cabarles_IPT.Domain.Commands;
using Cabarles_IPT.Domain.Queries;
using Cabarles_IPT.Framework.Commands;
using Cabarles_IPT.Framework.Queries;
using CabarlesWPF.Stores;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CabarlesWPF.HostBuilders
{
    public static class AddServicesHostBuilder
    {
        public static IHostBuilder AddServices(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<OrderStore>();
                
                services.AddSingleton<ICreateOrderItemCommand, CreateOrderItemCommand>();
                services.AddSingleton<IUpdateOrderItemCommand, UpdateOrderItemCommand>();
                services.AddSingleton<IDeleteOrderItemCommand, DeleteOrderItemCommand>();
                services.AddSingleton<IGetAllOrderItemsQuery, GetAllOrderItemsQuery>();
            });

            return hostBuilder;
        }
    }
}
