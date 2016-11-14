using System;
using System.Collections.Generic;

namespace WebCrawler.WebCrawlerResults
{
    public class CrawlResult
    {
        public List<CrawlItem> InsertedUrls { get; }

        public CrawlResult()
        {
            InsertedUrls = new List<CrawlItem>();
        }

        public void Add(CrawlItem newItem)
        {
            if (newItem == null) throw new ArgumentNullException(nameof(newItem));

            InsertedUrls.Add(newItem);
        }
    }
}