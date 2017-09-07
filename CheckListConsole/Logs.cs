using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CheckListConsole
{
    public static class Logs
    {
        public static LoggerFactory Factory = new LoggerFactory();

        public static void Init(IConfiguration configuration)
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
            
            //Factory.AddConsole(LogLevel.Trace, includeScopes: true);
            Factory.AddConsole(configuration.GetSection("Logging"));
            //Factory.AddDebug(LogLevel.Debug);
            Factory.AddFile(Path.Combine("logs", "checklist-{Date}.json"), 
                isJson: true,  
                minimumLevel: LogLevel.Trace);
        }
    }
}
