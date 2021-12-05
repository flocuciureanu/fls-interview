using System;
using System.Linq;
using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.Builder.MongoEntityBuilder;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
#pragma warning disable 1591

namespace FirstLineSoftwareInterviewTask.Hosting.WebApi.Installers
{
    public class ServicesAssemblyDependentServicesInstaller : IInstaller
    {
        private const string ServicesAssemblyName = "FirstLineSoftwareInterviewTask.Business.Services";

        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var servicesAssembly = AppDomain.CurrentDomain
                .GetAssemblies()
                .FirstOrDefault(a => a.FullName != null && a.FullName.StartsWith(ServicesAssemblyName));
            
            if (servicesAssembly is null)
                return;
            
            //MediatR
            services.AddMediatR(servicesAssembly);
            
            //FluentValidation
            services.AddFluentValidation(new[] { servicesAssembly });

            // services.AddValidatorsFromAssembly(servicesAssembly);
            
            //AutoMapper
            services.AddAutoMapper(servicesAssembly);
            
            //This registers all implementations of type T interface 
            services.Scan(scan => scan.FromAssemblies(servicesAssembly)
                .AddClasses(classes => classes.AssignableTo(typeof(IMongoEntityBuilder<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());
        }
    }
}