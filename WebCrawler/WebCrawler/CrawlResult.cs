using System.Collections.Generic;
using System.Collections.Concurrent;

namespace WebCrawler
{
    public class CrawlResult
    {
        public Dictionary<string, CrawlResult> InnerCrawlResults { get; private set; }

        public CrawlResult()
        {
            InnerCrawlResults = new Dictionary<string, CrawlResult>();
        }

    }
}
