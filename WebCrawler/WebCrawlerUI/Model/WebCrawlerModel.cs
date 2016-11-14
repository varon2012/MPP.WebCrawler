using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCrawler;

namespace WebCrawlerUI.Model
{
    public class WebCrawlerModel : IWebCrawlerModel
    {
        public const string ConfigXMLFileName = "Config/config.xml";
        public const string ConfigXSDFileName = "Config/config.xsd";
        public const string TargetNamespace = "http://bsuir.by/webcrawler";

        public async Task<CrawlResult> CrawlAsync()
        {
            IXmlConfigReader xmlConfigReader = new XmlConfigReader(ConfigXMLFileName, ConfigXSDFileName, TargetNamespace);
            Config config = xmlConfigReader.ReadConfig();
            ISimpleWebCrawler webCrawler = new SimpleWebCrawler(config.Depth);
            return await webCrawler.PerformCrawlingAsync(new List<string>(config.RootUrls));
        }
    }
}
