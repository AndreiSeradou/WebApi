using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Mapping;

namespace WebApi.Configuration
{
    public static class DependenciesConfiguration
    {
        public static IServiceCollection RegisterMappingConfig(this IServiceCollection serviceCollection)
        {

            serviceCollection.AddAutoMapper(
                c => c.AddProfile<MappingConfiguration>(),
                typeof(MappingConfiguration));

            return serviceCollection;
        }
    }
}
