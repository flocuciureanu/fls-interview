using System.Security.Authentication;
using FirstLineSoftwareInterviewTask.Common.Core.Settings.DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
#pragma warning disable 1591

namespace FirstLineSoftwareInterviewTask.Hosting.WebApi.Installers
{
    public class MongoInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseSettings>(configuration.GetSection(nameof(DatabaseSettings)));

            var databaseSettings = new DatabaseSettings();
            configuration.GetSection(nameof(DatabaseSettings)).Bind(databaseSettings);

            var connectionStrings = databaseSettings.ConnectionString;
            var databaseName = databaseSettings.DatabaseName;

            var settings = MongoClientSettings.FromUrl(new MongoUrl(connectionStrings));
            settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            var mongoClient = new MongoClient(settings);
            var database = mongoClient.GetDatabase(databaseName);

            services.AddScoped(_ => database);

            services.AddSingleton<IDatabaseSettings>(sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);
        }
    }
}