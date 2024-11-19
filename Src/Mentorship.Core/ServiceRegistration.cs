using Microsoft.Extensions.DependencyInjection;

namespace Mentorship.Core
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddCoreService(this IServiceCollection services)
        {
            return services;
        }
    }
}
