using System;
using System.Collections.Generic;

namespace WebCrawler.WebCrawlerResults
{
    public class CrawlItem
    {
        public string Url { get; }
        public List<CrawlItem> InsertedUrls { get; }

        public CrawlItem(string url)
        {
            if (url == null) throw new ArgumentNullException(nameof(url));

            Url = url;
            InsertedUrls = new List<CrawlItem>();
        }

        public void Add(CrawlItem newItem)
        {
            if (newItem == null) throw new ArgumentNullException(nameof(newItem));

            InsertedUrls.Add(newItem);
        }
    }
}