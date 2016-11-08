using System;
using System.Threading.Tasks;
using WebCrawler.HtmlParser;
using WebCrawler.WebCrawlerResults;

namespace WebCrawler.WebCrawler
{
    public class SimpleWebCrawler : ISimpleWebCrawler
    {
        private readonly IHtmlParser _htmlParser;

        private const int MaxDepth = 6;
        private const int MinDepth = 1;

        public int Depth { get; }

        public SimpleWebCrawler(IHtmlParser parser, int depth)
        {
            if (parser == null) throw new ArgumentNullException(nameof(parser));
            _htmlParser = parser;

            Depth = IsDepthCorrect(depth) ? depth : MinDepth;
        }

        public async Task<CrawlResult> PerformCrawlingAsync(string[] rootUrls)
        {
            if (rootUrls == null) throw new ArgumentNullException(nameof(rootUrls));

            const int startDepth = 1;

            var crawlResult = new CrawlResult();

            foreach (var rootUrl in rootUrls)
            {
                var crawlItem = await ProcessUrlAsync(rootUrl, startDepth);
                crawlResult.Add(crawlItem);
            }

            return crawlResult;
        }

        private async Task<CrawlItem> ProcessUrlAsync(string currentUrl, int currentDepth)
        {
            var crawlItem = new CrawlItem(currentUrl);

            if (currentDepth <= Depth)
            {
                var foundUrls = await _htmlParser.ParsePageForUrlAsync(currentUrl);
                foreach (var foundUrl in foundUrls)
                {
                    var newItem = await ProcessUrlAsync(foundUrl, currentDepth + 1);
                    crawlItem.Add(newItem);
                }
            }

            return crawlItem;
        }

        private bool IsDepthCorrect(int depth)
        {
            return depth >= MinDepth && depth <= MaxDepth;
        }
    }
}