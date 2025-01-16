using DevFreela.Application.Commands.ProjectCommands.InsertProject;
using Microsoft.Extensions.DependencyInjection;
using DevFreela.Application.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;

namespace DevFreela.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication (this IServiceCollection services)
        {
            services
                .AddHandlers()
                .AddValidation();
            return services;
        }

        private static IServiceCollection AddHandlers(this IServiceCollection services) 
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblyContaining<InsertProjectCommand>();
            });

            services
                .AddTransient<IPipelineBehavior<InsertProjectCommand, ResultViewModel<int>>,
                    ValidateInsertCommandBehavior>();

            return services;
        }

        private static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services
                .AddFluentValidationAutoValidation()
                .AddValidatorsFromAssemblyContaining<InsertProjectCommand>();
            return services;
        }
    }
}
