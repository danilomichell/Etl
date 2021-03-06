using Etl.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Etl.Processamento
{
    public static class Configurations
    {
        public static ServiceProvider Inject()
        {
            var serviceCollection = new ServiceCollection();

            var configuration = SetGeneralConfiguration(serviceCollection);

            SetDbContexts(serviceCollection, configuration);
            SetScopedServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }

        private static IConfiguration SetGeneralConfiguration(IServiceCollection serviceCollection)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env}.json", true, true)
                .AddEnvironmentVariables()
                .Build();

            serviceCollection.AddSingleton<IConfiguration>(configuration);

            return configuration;
        }

        private static void SetDbContexts(IServiceCollection serviceCollection, IConfiguration configuration)
        {

            var connectionProbdCartao = configuration.GetConnectionString("FolhaContext");

            serviceCollection.AddDbContextPool<FolhaContext>(opts => opts.UseNpgsql(connectionProbdCartao));
        }

        private static void SetScopedServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IProcessoEtl, ProcessoEtl>();
        }
    }
}
