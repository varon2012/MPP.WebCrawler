using System.Collections.Generic;
using System.Collections.Concurrent;

namespace WebCrawler
{
    public class CrawlResult
    {
        public ConcurrentDictionary<string, CrawlResult> InnerCrawlResults { get; private set; }

        public CrawlResult()
        {
            InnerCrawlResults = new ConcurrentDictionary<string, CrawlResult>();
        }

    }
}
