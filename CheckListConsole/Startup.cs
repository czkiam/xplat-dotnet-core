using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace CheckListConsole
{
    public class Startup
    {
        private IConfigurationRoot _config;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            _config = Config.Build();

            services.AddHangfire(c => c.UseMemoryStorage());
            services.AddTransient<CheckLinkJob>();
            services.AddSingleton<LinkChecker>();
            services.Configure<OutputSettings>(_config.GetSection("output"));
            services.Configure<SiteSettings>(_config);
            //Output = configuration.GetSection("output").Get<OutputSettings>();
            //Site = configuration["site"];
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            Logs.Init(loggerFactory, _config);
            
            app.UseHangfireServer();
            app.UseHangfireDashboard();
            app.Run(async context => await context.Response.WriteAsync("We are doing well!"));
        }
    }
}