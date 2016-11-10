using System.Threading.Tasks;
using WebCrawler.HtmlParser;
using WebCrawler.WebCrawler;
using WebCrawler.WebCrawlerResults;
using WebCrawlerTest.ConfigReader;

namespace WebCrawlerTest.Model
{
    internal class WebCrawlerModel 
    {
        private const string ConfigFileName = "config.xml";

        private readonly IConfigReader _configReader;
        private readonly ISimpleWebCrawler _webCrawler;

        public WebCrawlerModel()
        {
            var htmlParser = new HtmlParser();

            _configReader = new ConfigReader.ConfigReader();
            _configReader.LoadFile(ConfigFileName);
            var depth = _configReader.GetDepth();

            _webCrawler = new SimpleWebCrawler(htmlParser, depth);
        }

        public async Task<CrawlResult> GetWebCrawlingResultAsync()
        {
            var rootUrls = _configReader.GetUrls();
            return await _webCrawler.PerformCrawlingAsync(rootUrls.ToArray());
        }
    }
}