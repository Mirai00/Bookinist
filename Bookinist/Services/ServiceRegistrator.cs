using Bookinist.Services.Interfaces;
using Bookinist.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Bookinist.Services
{
    static class ServiceRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
            .AddTransient<ISalesService, SalesService>()
        ;
    }
}
