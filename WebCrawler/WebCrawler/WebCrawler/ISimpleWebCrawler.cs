using System.Threading.Tasks;
using WebCrawler.WebCrawlerResults;

namespace WebCrawler.WebCrawler
{
    public interface ISimpleWebCrawler
    {
        int Depth { get; }

        Task<CrawlResult> PerformCrawlingAsync(string[] rootUrls);
    }
}