using CheckListConsole;
using System;
using System.Linq;
using Xunit;

namespace CheckListTest
{
    public class UnitTest
    {
        [Fact]
        public void WithoutHttpAtStartOfLink_NoLinks()
        {
            var links = LinkChecker.Getlinks("", "<a href=\"google.com\" />");
            Assert.Equal(links.Count(), 0);
        }

        [Fact]
        public void WithHttpAtStartOfLink_LinksParses()
        {
            var links = LinkChecker.Getlinks("", "<a href=\"http://google.com\" />");
            Assert.Equal(links.Count(), 1);
            Assert.Equal(links.First(), "http://google.com");
        }
    }
}
