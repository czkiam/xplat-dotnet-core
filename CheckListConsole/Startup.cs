using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace CheckListConsole
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHangfire(c => c.UseMemoryStorage());
            services.AddTransient<CheckLinkJob>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            var config = new Config();
            Logs.Factory = loggerFactory;
            Logs.Init(config.ConfigurationRoot);
            
            app.UseHangfireServer();
            app.UseHangfireDashboard();
            app.Run(async context => await context.Response.WriteAsync("We are doing well!"));

            RecurringJob.AddOrUpdate<CheckLinkJob>("check-link", 
                j => j.Execute(config.Site, config.Output), 
                Cron.Minutely);
        }
    }
}