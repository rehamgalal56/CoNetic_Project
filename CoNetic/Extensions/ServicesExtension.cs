﻿using CoNetic.Core.Models;
using CoNetic.Core.ReposInterfaces;
using CoNetic.Core.ServicesInterfaces;
using CoNetic.Mapping;
using CoNetic.Repository.Repos;
using CoNetic.Services.Helper;
using CoNetic.Services.Services;
using Mapster;
using MapsterMapper;
using System.Reflection;

namespace CoNetic.Extensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddServicesExtension(this IServiceCollection services, IConfiguration Configuration)
        {
            //Mapping
            var mappingconfig = TypeAdapterConfig.GlobalSettings;
            mappingconfig.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton<IMapper>(new Mapper(mappingconfig));
            MapingConfigrations.RegisterMappings();

            services.AddScoped<IGenericRepo<User>, ProfileRepo>();
            services.AddScoped<IEmailService, EmailService>();
            services.Configure<SMTP>(Configuration.GetSection("EmailSettings"));
            services.AddScoped<IFileService, FileService>();


            return services;
        }

    }
}
