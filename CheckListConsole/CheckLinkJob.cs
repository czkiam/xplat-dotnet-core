using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace CheckListConsole
{
    public class CheckLinkJob
    {
        private ILogger _logger;
        private OutputSettings _output;
        private SiteSettings _site;
        private LinkChecker _linkChecker;

        public CheckLinkJob(ILogger<CheckLinkJob> logger
            ,IOptions<OutputSettings> outputOptions
            ,IOptions<SiteSettings> siteOptions
            ,LinkChecker linkChecker)
        {
            _logger = logger;
            _logger.LogInformation($"{Guid.NewGuid()}");
            _output = outputOptions.Value;
            _site = siteOptions.Value;
            _linkChecker = linkChecker;
        }

        public void Execute()
        {
            //var logger = Logs.Factory.CreateLogger<Program>();

            //will create directory if not exists. 
            Directory.CreateDirectory(_output.GetReportDirectory());
            _logger.LogInformation(200, $"Saving report to {_output.GetResportFilePath()}");

            var client = new HttpClient();
            var body = client.GetStringAsync(_site.Site);
            _logger.LogDebug(body.Result);

            //Console.WriteLine("Links");
            var links = _linkChecker.Getlinks(_site.Site, body.Result);
            //links.ToList().ForEach(Console.WriteLine);

            //will create/ overwrite content of file            
            var checkedLinks = _linkChecker.CheckLinks(links);

            using (var file = File.CreateText(_output.GetResportFilePath()))
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
