using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CheckListConsole
{
    public class Config
    {
        public string Site { get; set; }
        public OutputSettings Output { get; set; }
        public IConfigurationRoot ConfigurationRoot { get; set; }

        public Config()
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
                .AddCommandLine(Environment.GetCommandLineArgs().Skip(1).ToArray());

            var configuration = configBuilder.Build();
            ConfigurationRoot = configuration;

            Site  = configuration["site"];

            //var outputSettings = new OutputSettings();
            //configuration.GetSection("output").Bind(outputSettings);

            Output = configuration.GetSection("output").Get<OutputSettings>();
        }
    }
}
