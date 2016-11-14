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
        private const int MaxDepthLimit = 6;
        private const int InitialDepth = 0;
        private const int DefaultDepth = 1;

        private readonly int _maxDepth;
        private IWebCrawlerHtmlParser _webCrawlerHtmlParser;

        private string _log;

        public string Log
        {
            get { return _log; }
        }
        
        public SimpleWebCrawler() : this(DefaultDepth, new WebCrawlerHtmlParser()) { }

        public SimpleWebCrawler(int depth) : this(depth, new WebCrawlerHtmlParser()) { }

        public SimpleWebCrawler(int depth, IWebCrawlerHtmlParser webCrawlerHtmlParser)
        {
             _maxDepth = (depth <= MaxDepthLimit) ? depth : MaxDepthLimit;
             _log = string.Empty;
            _webCrawlerHtmlParser = webCrawlerHtmlParser;
        }

        public async Task<CrawlResult> PerformCrawlingAsync(List<string> rootUrls)
        {
            CrawlResult crawlResult = new CrawlResult();
            foreach (string rootUrl in rootUrls)
            {
                if (!crawlResult.InnerCrawlResults.ContainsKey(rootUrl))
                {
                    crawlResult.InnerCrawlResults.Add(rootUrl, await CrawlAsync(rootUrl, InitialDepth));
                }
            }
            return crawlResult;
        }


        private async Task<List<string>> GetHtmlPageInnerUrlsAsync(string url)
        {
            HttpClient httpClient = new HttpClient();
            try
            {
                string htmlPage = await httpClient.GetStringAsync(url);
                WebCrawlerHtmlParser wchp = new WebCrawlerHtmlParser();
                return wchp.GetInnerUrls(htmlPage, url);
            }
            catch(Exception e)
            {
                _log += e.Message + "\r\n"; 
                return null;
            }
            finally
            {
                if (httpClient != null)
                {
                    httpClient.Dispose();
                }
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
                        if (!crawlResult.InnerCrawlResults.ContainsKey(innerUrl))
                        {
                            crawlResult.InnerCrawlResults.Add(innerUrl, await CrawlAsync(innerUrl, currentDepth));
                        }
                    }
                }
                else
                {
                    foreach (string innerUrl in innerUrls)
                    {
                        if (!crawlResult.InnerCrawlResults.ContainsKey(innerUrl))
                        {
                            crawlResult.InnerCrawlResults.Add(innerUrl, null);
                        }
                    }
                }
            }
            return crawlResult;
        }

    }
}
