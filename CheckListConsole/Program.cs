using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace CheckListConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            GlobalConfiguration.Configuration.UseMemoryStorage();

            var host = new WebHostBuilder()
                            .UseKestrel()
                            //.UseLoggerFactory(Logs.Factory)
                            .UseContentRoot(Directory.GetCurrentDirectory())
                            //.UseIISIntegration()
                            .UseStartup<Startup>()
                            .Build();

            //sample code of getting registered logger factory class
            //var loggerFactory = host.Services.GetService<ILoggerFactory>();
            //loggerFactory.CreateLogger<Program>().LogInformation("test");

            //var logger = host.Services.GetService<ILogger<Program>>();
            //logger.LogInformation("Direct logging test");

            //sample get another registered service
            //var jobActicator = host.Services.GetService<JobActivator>();
            //Console.WriteLine(jobActicator.GetType());

            RecurringJob.AddOrUpdate<CheckLinkJob>("check-link",
                j => j.Execute(),
                Cron.Minutely);

            RecurringJob.Trigger("check-link");

            //RecurringJob.AddOrUpdate(() => Console.WriteLine("Simple!"), Cron.Minutely);

            host.Run();
        }
    }
}
