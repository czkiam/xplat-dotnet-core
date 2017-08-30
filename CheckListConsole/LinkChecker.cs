using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace xplat_dotnet_core
{
    public class LinkChecker
    {
        public static IEnumerable<string> Getlinks(string page)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(page);
            var links = htmlDocument.DocumentNode.SelectNodes("//a[@href]")
                .Select(n => n.GetAttributeValue("href", string.Empty))
                .Where(l => !String.IsNullOrEmpty(l))
                .Where(l => l.StartsWith("http", StringComparison.Ordinal));

            return links;
        }
    }
}