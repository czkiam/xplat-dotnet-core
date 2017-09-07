﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net.Http;
using Microsoft.Extensions.Logging;

namespace CheckListConsole
{
    public class LinkChecker
    {
        protected static readonly ILogger<LinkChecker> Logger = Logs.Factory.CreateLogger<LinkChecker>();

        public static IEnumerable<string> Getlinks(string link, string page)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(page);
            var originalLinks = htmlDocument.DocumentNode.SelectNodes("//a[@href]")
                .Select(n => n.GetAttributeValue("href", string.Empty))
                .ToList();

            using (Logger.BeginScope($"Getting links from {link}"))
            {
                originalLinks.ForEach(l => Logger.LogTrace(100, "Original link: {link}", l));
            };

            var links = originalLinks
                .Where(l => !String.IsNullOrEmpty(l))
                .Where(l => l.StartsWith("http", StringComparison.Ordinal));

            return links;
        }

        public static IEnumerable<LinkCheckResult> CheckLinks(IEnumerable<string> links)
        {
            var all = Task.WhenAll(links.Select(CheckLinkAsync));

            return all.Result;
        }

        public static async Task<LinkCheckResult> CheckLinkAsync(string link)
        {
            var result = new LinkCheckResult();
            result.Link = link;
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Head, link);
                try
                {
                    var response = await client.SendAsync(request);
                    result.Problem = response.IsSuccessStatusCode ? null : response.StatusCode.ToString();
                    return result;
                }
                catch (HttpRequestException exception)
                {
                    Logger.LogTrace(0, exception, "Failed to retrieve {link}", link);
                    result.Problem = exception.Message;
                    return result;
                }
            }
        }
    }
}