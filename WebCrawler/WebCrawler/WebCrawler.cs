using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using HtmlAgilityPack;

namespace WebCrawler
{
    public class WebCrawler : ISimpleWebCrawler, IDisposable
    {
        private ConsoleLogger consoleLogger = new ConsoleLogger();
        private readonly WebClient webClient = new WebClient();

        private int depth = 0;

        public int Depth
        {
            get
            {
                return depth;
            }
            set
            {
                if (value < 6)
                {
                    depth = value;
                }
                else
                {
                    depth = 6;
                }
            }
        }

        public WebCrawler(int depth)
        {
            Depth = depth;
        }

        public async Task<CrawlResult> PerformCrawlingAsync(string[] rootUrls)
        {
            return await GetCrawlResult(1, rootUrls);
        }

        private async Task<CrawlResult> GetCrawlResult(int currentDepth, string[] rootUrls)
        {
            Dictionary<string, CrawlResult> crawlResult = new Dictionary<string, CrawlResult>();
            foreach (var url in rootUrls)
            {
                try
                {                    
                    crawlResult[url] = await DownloadWebsite(url);
                    if (currentDepth < Depth)
                        crawlResult[url] = await GetCrawlResult(currentDepth + 1, crawlResult[url].Urls.Keys.ToArray());
                }
                catch(Exception e)
                {
                    consoleLogger.WriteLine(e.Message, url);
                }
                
            }

            return new CrawlResult(crawlResult);
        }

        private async Task<CrawlResult> DownloadWebsite(string url)
        {
            try
            {
                string htmlSource = await webClient.DownloadStringTaskAsync(url);
                return GetUrlsFromWebsite(htmlSource);
            }
            catch (WebException e)
            {
                throw new WebException("Can't download a html-source of this website: ");
            }

        }

        private CrawlResult GetUrlsFromWebsite(string htmlSource)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmlSource);
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//a[@href]");

            return new CrawlResult(ParseWebsite(nodes));
        }

        private Dictionary<string, CrawlResult> ParseWebsite(HtmlNodeCollection nodes)
        {
            if (nodes == null)
                throw new ArgumentNullException(nameof(nodes));

            Dictionary<string, CrawlResult> resultUrls = new Dictionary<string, CrawlResult>();
            foreach (HtmlNode link in nodes)
            {
                string url = link.Attributes["href"].Value;
                if (url.Contains("https://") || url.Contains("http://"))
                     resultUrls[url] = new CrawlResult(new Dictionary<string, CrawlResult>());
            }

            return resultUrls;
        }

        public void Dispose()
        {
            if (webClient != null)
                webClient.Dispose();
        }
    }
}
