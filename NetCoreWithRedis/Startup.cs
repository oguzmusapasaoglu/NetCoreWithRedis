using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreWithRedis.Core.CacheManager.Redis;
using NetCoreWithRedis.Core.DbCore;
using NetCoreWithRedis.Core.Log.Services;
using NetCoreWithRedis.Domain.Authentication.Interface;
using NetCoreWithRedis.Domain.Authentication.Manager;
using NetCoreWithRedis.Domain.Users.Interface;
using NetCoreWithRedis.Domain.Users.Manager;

namespace NetCoreWithRedis
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMvc();
            #region Core
            services.AddSingleton<ILogManager, LogManager>();
            services.AddSingleton<IDbFactory, DbFactory>();
            #endregion

            #region Domains
            services.AddSingleton<IAuthenticationManager, AuthenticationManager>();
            services.AddSingleton<IUserManager, UserManager>();
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
