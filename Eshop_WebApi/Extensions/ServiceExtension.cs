﻿using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Eshop_WebApi.Extensions
{
    public static class ServiceExtension
    {

        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                //Adding the Authentication Header
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    //Adding some descriptions 
                    Description = "Standard Authorization header using the Bearer Scheme(\"Bearer {token}\")",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                //Lastly adding the operation filter
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            return services;
        }
    }
}
