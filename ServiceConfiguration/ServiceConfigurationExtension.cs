using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using IRepository;
using Repository;
using IBusiness;
using BusinessProvider;
using System.Security.Cryptography;

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
            services.AddScoped<IUntaggedUPCRepository, UntaggedUPCRepository>();
            services.AddScoped<ICommonRepository, CommonRepository>();
            services.AddScoped<ITaggedUPCRepository, TaggedUPCRepository>();
        }

        public static void RegisterUPCTaggingServices(IServiceCollection services)
        {
            services.AddScoped<IUPCTaggingService, UPCTaggingService>();
            services.AddScoped<IUntaggedUPCService, UntagggedUPCService>();
            services.AddScoped<ICommonService, CommonService>();
            services.AddScoped<ITaggedUPCService, TaggedUPCService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IHashingService, HashingService>();
            services.AddScoped<HashAlgorithm, SHA256CryptoServiceProvider>();
            services.AddSingleton<RandomNumberGenerator, RNGCryptoServiceProvider>();
        }

    }
}
