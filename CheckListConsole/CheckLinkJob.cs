using Microsoft.Extensions.Logging;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace CheckListConsole
{
    public class CheckLinkJob
    {
        public void Execute(string site, OutputSettings output)
        {
            var logger = Logs.Factory.CreateLogger<Program>();

            //will create directory if not exists. 
            Directory.CreateDirectory(output.GetReportDirectory());
            logger.LogInformation(200, $"Saving report to {output.GetResportFilePath()}");

            var client = new HttpClient();
            var body = client.GetStringAsync(site);
            logger.LogDebug(body.Result);

            //Console.WriteLine("Links");
            var links = LinkChecker.Getlinks(site, body.Result);
            //links.ToList().ForEach(Console.WriteLine);

            //will create/ overwrite content of file            
            var checkedLinks = LinkChecker.CheckLinks(links);

            using (var file = File.CreateText(output.GetResportFilePath()))
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
