using Eshop_Application.Common.Behaviours;
using Eshop_Application.Common.Interfaces;
using Eshop_Application.Common.Options;
using Eshop_Application.Features.User.Commands.RegisterUserCommand;
using Eshop_Application.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Eshop_Application;

public static class ApplicationServicesRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services,IConfiguration configuration)
    {
        //Registering the Mediatr => Transient
        services.AddMediatR(options => 
        {
            options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            options.AddBehavior(typeof(IPipelineBehavior<,>),typeof(ValidationBehavior<,>));
            options.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            options.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformaceBehaviour<,>));
        });

        //Registering the validations => Transient
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        //Registering the services
        services.AddTransient<IPasswordUtilities, PasswordUtilitiesServices>();

        //Registering the Config (Need the IConfiguration Extensions nugget and CofigurationBinder Extensions nugget)
        services.Configure<HashSettings>(options => configuration.GetSection("HashConfig").Bind(options));

        return services;
    }
}