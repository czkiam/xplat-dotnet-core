﻿using Microsoft.Extensions.Configuration;
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
            var config = new Config(args);
            Logs.Init(config.ConfigurationRoot);
            var logger = Logs.Factory.CreateLogger<Program>();
            

            //will create directory if not exists. 
            Directory.CreateDirectory(config.Output.GetReportDirectory());
            logger.LogInformation(200, $"Saving report to {config.Output.GetResportFilePath()}");

            var client = new HttpClient();
            var body = client.GetStringAsync(config.Site);
            logger.LogDebug(body.Result);

            //Console.WriteLine("Links");
            var links = LinkChecker.Getlinks(config.Site, body.Result);
            //links.ToList().ForEach(Console.WriteLine);

            //will create/ overwrite content of file            
            var checkedLinks = LinkChecker.CheckLinks(links);

            using (var file = File.CreateText(config.Output.GetResportFilePath()))
            using (var linkDb = new LinksDb())
            {
                foreach (var link in checkedLinks.OrderBy(l => l.Exists))
                {
                    var status = link.IsMissing ? "missing" : "OK";
                    file.WriteLine($"{status} - {link.Link}");
                    linkDb.Links.Add(link);
                }
                linkDb.SaveChanges();
            }
            
        }
    }
}
