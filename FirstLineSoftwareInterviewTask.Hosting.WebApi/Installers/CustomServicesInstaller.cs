using FirstLineSoftwareInterviewTask.Business.Services.Features.Item.Builders;
using FirstLineSoftwareInterviewTask.Business.Services.Features.Item.Services;
using FirstLineSoftwareInterviewTask.Business.Services.Features.User.Builders.User;
using FirstLineSoftwareInterviewTask.Business.Services.Features.User.Services;
using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.Builder.Response.Builder;
using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.Builder.Response.Factory;
using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.CommandBus;
using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.Persistence;
using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.Serialization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
#pragma warning disable 1591

namespace FirstLineSoftwareInterviewTask.Hosting.WebApi.Installers
{
    public class CustomServicesInstaller : IInstaller   
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserBuilder, UserBuilder>();
            
            services.AddTransient<IItemService, ItemService>();
            services.AddTransient<IItemBuilder, ItemBuilder>();
            
            services.AddTransient<IPricingService, PricingService>();

            services.AddSingleton<IJsonSerializer, JsonSerializer>();

            services.AddTransient<ICommandResultFactory, CommandResultFactory>();

            services.AddTransient(typeof(IResponseBuilder<>), typeof(ResponseBuilder<>));
            services.AddTransient(typeof(IResponseFactory<>), typeof(ResponseFactory<>));
            
            services.AddTransient(typeof(IDatabaseRepository<>), typeof(DatabaseRepository<>));
        }
    }
}