﻿using MBS_QUERY.API.DependencyInjection.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace MBS_QUERY.API.DependencyInjection.Extensions;

public static class SwaggerExtensions
{
    public static void AddSwaggerAPI(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme()
            {
                Description = @"JWT Authorization header using the Bearer scheme. 

Enter 'Bearer' [space] and then your token in the text input below.

Example: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = JwtBearerDefaults.AuthenticationScheme
            });
            
            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        },
                        Scheme = "oauth2",
                        Name = JwtBearerDefaults.AuthenticationScheme,
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });
            
            c.OperationFilter<SwaggerFormDataOperationFilter>();
            
            c.EnableAnnotations();
            
        });
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
    }
    
    public class SwaggerFormDataOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var formParameters = context.MethodInfo
                .GetParameters()
                .Where(p => p.GetCustomAttributes(true)
                    .Any(attr => attr.GetType() == typeof(FromFormAttribute)))
                .ToList();

            if (formParameters.Any())
            {
                foreach (var param in formParameters)
                {
                    operation.RequestBody = new OpenApiRequestBody
                    {
                        Content = {
                            ["multipart/form-data"] = new OpenApiMediaType
                            {
                                Schema = context.SchemaGenerator.GenerateSchema(param.ParameterType, context.SchemaRepository)
                            }
                        }
                    };
                }
            }
        }
    }

    public static void UseSwaggerAPI(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            foreach (var version in app.DescribeApiVersions().Select(version => version.GroupName))
                options.SwaggerEndpoint($"/swagger/{version}/swagger.json", version);

            options.DisplayRequestDuration();
            options.EnableTryItOutByDefault();
            options.DocExpansion(DocExpansion.None);
        });

        app.MapGet("/", () => Results.Redirect("/swagger/index.html"))
            .WithTags(string.Empty);
    }
}
