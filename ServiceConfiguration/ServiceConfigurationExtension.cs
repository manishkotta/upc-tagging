﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using IRepository;
using Repository;
using IBusiness;
using BusinessProvider;

namespace ServiceConfiguration
{
    public static class ServiceConfigurationExtension
    {

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            RegisterUPCTaggingRepositories(services);
            RegisterUPCTaggingServices(services);
            return services;
        } 

        public static void RegisterUPCTaggingRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUPCTaggingRepository,UPCTaggingRepository>();
        }

        public static void RegisterUPCTaggingServices(IServiceCollection services)
        {
            services.AddScoped<IFileService, FileService>();
        }

    }
}