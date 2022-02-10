using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TimeBank.API.Extensions;
using TimeBank.API.Maps;
using TimeBank.API.Services;
using TimeBank.Services;
using TimeBank.Services.Contracts;

namespace TimeBank.API
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration _config)
        {
            this._config = _config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCors();
            services.ConfigureIISIntegration();
            services.ConfigureDatabase(_config);
            services.ConfigureIdentity(_config);

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TimeBank.API", Version = "v1" });
            });

            services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            services.AddScoped<IJobService, JobService>();
            services.AddScoped<IJobCategoryService, JobCategoryService>();
            services.AddScoped<IJobApplicationService, JobApplicationService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ITokenBalanceService, TokenBalanceService>();
            services.AddScoped<ITokenTransactionService, TokenTransactionService>();
            services.AddScoped<IUserRatingService, UserRatingService>();
            services.AddScoped<IUserSkillService, UserSkillService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TimeBank.API v1"));
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
