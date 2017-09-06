﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CheckListConsole
{
    public class Config
    {
        public string Site { get; set; }
        public OutputSettings Output { get; set; }

        public Config(string[] args)
        {
            var inMemory = new Dictionary<string, string>
            {
                {"site", "https://g0t4.github.io/pluralsight-dotnet-core-xplat-apps" },
                {"output:folder", "reports" },
            };

            //configuration precendent will depend on providers add sequences asc
            var configBuilder = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemory)
                .AddEnvironmentVariables()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("checksettings.json", true)
                .AddCommandLine(args);

            var configuration = configBuilder.Build();
            Site  = configuration["site"];

            //var outputSettings = new OutputSettings();
            //configuration.GetSection("output").Bind(outputSettings);

            Output = configuration.GetSection("output").Get<OutputSettings>();
        }
    }
}