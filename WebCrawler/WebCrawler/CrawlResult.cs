using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
