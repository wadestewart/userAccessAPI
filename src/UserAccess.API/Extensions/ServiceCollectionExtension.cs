using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserAccess.Business;
using UserAccess.Data.Mongo;
using UserAccess.Data.Mongo.Collections;
using UserAccess.Data.Mongo.Models;
using MongoDB.Driver;

namespace UserAccess.API.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtension
    {

        public static void ConfigureDependencyInjection(this IServiceCollection services,
            Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");
            services.AddSingleton<IMongoClient>(s => new MongoClient(connectionString));
            services.AddSingleton(s => new MongoUrl(connectionString));
            services.AddScoped<IMongoContext<UserDataModel>, MongoContext<UserDataModel>>();
            services.AddScoped<IUserCollection, UserCollection>();
            services.AddScoped<IUserManager, UserManger>();
        }
        
    }
}