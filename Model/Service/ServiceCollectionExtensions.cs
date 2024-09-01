using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Model.Entity;
using Model.Repository;

namespace Model.Service
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureCustomServices(this IServiceCollection services)
        {
            var addJsonFile = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("settings.database.json")
                .Build()
                .GetRequiredSection(DbSettings.SectionName);

            services.AddOptions<DbSettings>()
                .Bind(addJsonFile)
                .ValidateOnStart();
            services.AddScoped<IConnectionProvider, ConnectionProvider>();
            services.AddScoped<IRepository<SportsTeam>, DapperRepository<SportsTeam>>();
            services.AddScoped<SportsTeamService>();
            services.AddSingleton<IDbConnection, SqlConnection>();
            services.AddTransient<IContextService, ContextService>();

            return services;
        }
    }
}
