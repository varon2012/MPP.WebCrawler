using System.Collections.Generic;

namespace WebCrawler
{
    public class CrawlResult
    {
        public Dictionary<string, CrawlResult> Urls { get; }

        public CrawlResult(Dictionary<string, CrawlResult> urls)
        {
            Urls = urls;
        }

    }
}
