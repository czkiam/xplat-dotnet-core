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
        public static IConfigurationRoot Build()
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

            return configBuilder.Build();
        }
    }
}
