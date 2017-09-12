using CheckListConsole;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Xunit;

namespace CheckListTest
{
    public class UnitTest
    {
        protected static readonly ILogger<LinkChecker> Logger

        [Fact]
        public void WithoutHttpAtStartOfLink_NoLinks()
        {
            
            var links = new LinkChecker(null).Getlinks("", "<a href=\"google.com\" />");
            Assert.Equal(links.Count(), 0);
        }

        [Fact]
        public void WithHttpAtStartOfLink_LinksParses()
        {
            var links = new LinkChecker(null).Getlinks("", "<a href=\"http://google.com\" />");
            Assert.Equal(links.Count(), 1);
            Assert.Equal(links.First(), "http://google.com");
        }
    }
}
