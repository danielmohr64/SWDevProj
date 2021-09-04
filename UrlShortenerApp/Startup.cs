using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SW.Data.Http;
using SW.Data.Http.Repo;
using SW.Logic;

namespace UrlShortenerApp
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Config
            services.AddSingleton<IConfiguration>(Configuration);
            services.Configure<UrlServiceConfigs>(Configuration.GetSection("UrlServiceConfigs"));

            // Repo HTTP
            services.AddHttpContextAccessor();
            services.AddHttpClient<ICleanUrlApi, CleanUrlApi>(c =>
            {
                //c.DefaultRequestHeaders.Add("Host", "localhost");
                c.DefaultRequestHeaders.Add("Accept", "*/*");
                c.DefaultRequestHeaders.Add("User-Agent", ".NET");
            });

            // Tools 
            services.AddSingleton<IConvertJsonTools, ConvertJsonDotNetTools>();

            // Services
            services.AddScoped<IUrlConvertionService, UrlConvertionService>();
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
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();
        }
    }
}
