using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;

namespace WebCrawler
{
    public class SimpleWebCrawler : ISimpleWebCrawler
    {
        private static readonly int MAX_DEPTH_LIMIT = 6;
        private static readonly int INITIAL_DEPTH = 0;
        private static readonly int DEFAULT_DEPTH = 1;

        private readonly int _maxDepth;
        private IWebCrawlerHtmlParser _webCrawlerHtmlParser;

        private string _log;

        public string Log
        {
            get { return _log; }
        }
        
        public SimpleWebCrawler() : this(DEFAULT_DEPTH, new WebCrawlerHtmlParser()) { }

        public SimpleWebCrawler(int depth) : this(depth, new WebCrawlerHtmlParser()) { }

        public SimpleWebCrawler(int depth, IWebCrawlerHtmlParser webCrawlerHtmlParser)
        {
             _maxDepth = (depth <= MAX_DEPTH_LIMIT) ? depth : MAX_DEPTH_LIMIT;
             _log = string.Empty;
            _webCrawlerHtmlParser = webCrawlerHtmlParser;
        }

        public async Task<CrawlResult> PerformCrawlingAsync(List<string> rootUrls)
        {
            CrawlResult crawlResult = new CrawlResult();
            foreach (string rootUrl in rootUrls)
            {
                crawlResult.InnerCrawlResults.TryAdd(rootUrl, await CrawlAsync(rootUrl, INITIAL_DEPTH));
            }
            return crawlResult;
        }

        private async Task<List<string>> GetHtmlPageInnerUrlsAsync(string url)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                string htmlPage = await httpClient.GetStringAsync(url);
                WebCrawlerHtmlParser wchp = new WebCrawlerHtmlParser();
                return wchp.GetInnerUrls(htmlPage, url);
            }
            catch(Exception e)
            {
                Volatile.Write(ref _log, Volatile.Read(ref _log) +  e.Message + "\r\n");
                return null;
            }
        }


        private async Task<CrawlResult> CrawlAsync(string url, int currentDepth)
        {
            currentDepth++;
            CrawlResult crawlResult = new CrawlResult();
            List<string> innerUrls = await GetHtmlPageInnerUrlsAsync(url);
            if (innerUrls != null)
            {
                if (currentDepth < _maxDepth)
                {
                    foreach (string innerUrl in innerUrls)
                    {
                        crawlResult.InnerCrawlResults.TryAdd(innerUrl, await CrawlAsync(innerUrl, currentDepth));
                    }
                }
                else
                {
                    foreach (string innerUrl in innerUrls)
                    {
                        crawlResult.InnerCrawlResults.TryAdd(innerUrl, null);
                    }
                }
            }
            return crawlResult;
        }

    }
}
