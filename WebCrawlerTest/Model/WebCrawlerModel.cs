using System;
using System.Threading.Tasks;
using System.Windows;
using WebCrawler;

namespace WebCrawlerTest.Model
{
    internal class WebCrawlerModel
    {
        private string configPath = "config.xml";
        private XmlConfigReader reader;

        public WebCrawler.WebCrawler WebCrawler { get; set; }

        public void ReadConfigInformation()
        {
            reader = new XmlConfigReader(configPath);
            reader.ReadConfigInformation();
        }

        public async Task<CrawlResult> StartWebCrawler()
        {
            WebCrawler = new WebCrawler.WebCrawler(reader.Depth);
            return await WebCrawler.PerformCrawlingAsync(reader.RootUrls);
        }
    }
}
