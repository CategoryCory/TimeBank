using HashidsNet;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TimeBank.API.Options;
using TimeBank.API.Services;
using TimeBank.Repository;
using TimeBank.Services;
using TimeBank.Services.Contracts;

namespace TimeBank.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services, IConfiguration config)
        {
            //var corsOrigins = config.GetSection("CorsOrigins").Get<string>();
            var corsOrigins = config["CorsOrigins"];

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .WithOrigins(corsOrigins)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
        }

        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("ApplicationConnection"));
            });
        }

        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options => { });
        }

        public static void ConfigureLocalServices(this IServiceCollection services, IConfiguration config)
        {
            //services.AddSingleton<IUserIdProvider, EmailBasedUserIdProvider>();
            services.AddSingleton<IHashids>(_ => new Hashids(config["HashIdSettings:Salt"],
                                                             config.GetSection("HashIdSettings:MinLength").Get<int>()));
            services.AddScoped<IJobService, JobService>();
            services.AddScoped<IJobCategoryService, JobCategoryService>();
            services.AddScoped<IJobApplicationService, JobApplicationService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ITokenBalanceService, TokenBalanceService>();
            services.AddScoped<ITokenTransactionService, TokenTransactionService>();
            services.AddScoped<IUserRatingService, UserRatingService>();
            services.AddScoped<IUserService, UserService>();
            //services.AddScoped<IUserSkillService, UserSkillService>();
            services.AddScoped<IJobScheduleService, JobScheduleService>();
            services.AddScoped<IMessageThreadService, MessageThreadService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IPhotoUploadService, PhotoUploadService>();

            services.Configure<AzureStorageSettings>(config.GetSection(AzureStorageSettings.AzureStorage));
        }
    }
}
