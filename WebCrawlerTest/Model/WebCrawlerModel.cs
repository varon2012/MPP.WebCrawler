using System.Threading.Tasks;
using WebCrawler;

namespace WebCrawlerTest.Model
{
    internal class WebCrawlerModel
    {
        private string configPath = "config.xml";
        private XmlConfigReader reader;

        public void ReadConfigInformation()
        {
            reader = new XmlConfigReader(configPath);
            reader.ReadConfigInformation();
        }

        public async Task<CrawlResult> StartWebCrawler()
        {
            using (WebCrawler.WebCrawler webCrawler = new WebCrawler.WebCrawler(reader.Depth))
            {
                return await webCrawler.PerformCrawlingAsync(reader.RootUrls);
            }
        }
    }
}
