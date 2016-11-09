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
            try
            {
                reader = new XmlConfigReader(configPath);
                reader.ReadConfigInformation();
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.Message}. The program exits.");
                Environment.Exit(0);
            }
        }

        public async Task<CrawlResult> StartWebCrawler()
        {
            WebCrawler = new WebCrawler.WebCrawler();
            WebCrawler.Depth = reader.Depth;
            return await WebCrawler.PerformCrawlingAsync(reader.RootUrls);
        }
    }
}
