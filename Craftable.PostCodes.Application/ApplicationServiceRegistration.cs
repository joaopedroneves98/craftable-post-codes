namespace Craftable.PostCodes.Application
{
    using Abstractions.Application;

    using Microsoft.Extensions.DependencyInjection;

    using Services;
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IPostCodesService, PostCodesService>();
            services.AddScoped<IGeoCalculatorService, GeoCalculatorService>();

            return services;
        }
    }
}