using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace CheckListConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Log level
             * trace = 0
             * debug = 1
             * information = 2
             * warning = 3
             * error = 4
             * critical = 5
             */
            //default logging level is information

            var factory = new LoggerFactory();
            factory.AddConsole();
            factory.AddDebug(LogLevel.Debug);
            factory.AddFile(Path.Combine("logs", "checklist-{Date}.txt"), LogLevel.Debug);

            var logger = factory.CreateLogger("main");
            
            var config = new Config(args);

            //will create directory if not exists. 
            Directory.CreateDirectory(config.Output.GetReportDirectory());
            logger.LogInformation($"Saving report to {config.Output.GetResportFilePath()}");

            var client = new HttpClient();
            var body = client.GetStringAsync(config.Site);
            logger.LogDebug(body.Result);

            Console.WriteLine("Links");
            var links = LinkChecker.Getlinks(body.Result);
            links.ToList().ForEach(Console.WriteLine);
            
            //will create/ overwrite content of file            
            var checkedLinks = LinkChecker.CheckLinks(links);
            
            using (var file = File.CreateText(config.Output.GetResportFilePath()))
            {
                foreach (var link in checkedLinks.OrderBy(l => l.Exists))
                {
                    var status = link.IsMissing ? "missing" : "OK";
                    file.WriteLine($"{status} - {link.Link}");
                }
            }
            
        }
    }
}
