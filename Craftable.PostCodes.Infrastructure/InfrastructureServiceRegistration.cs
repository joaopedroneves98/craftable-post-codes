namespace Craftable.PostCodes.Infrastructure
{
    using Application.Abstractions.Infrastructure;

    using MarkEmbling.PostcodesIO;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using PostCodes;
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IPostCodesGateway>(x =>
                new PostCodesGateway(x.GetRequiredService<ILogger<PostCodesGateway>>(), new PostcodesIOClient())
            );

            return services;
        }
    }
}