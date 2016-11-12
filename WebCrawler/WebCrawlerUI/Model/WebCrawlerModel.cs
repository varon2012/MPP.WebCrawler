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
        public static readonly string CONFIG_XML_FILE_NAME = "config.xml";
        public static readonly string CONFIG_XSD_FILE_NAME = "config.xsd";
        public static readonly string TARGET_NAMESPACE = "http://bsuir.by/webcrawler";

        public async Task<CrawlResult> CrawlAsync()
        {
            IXmlConfigReader xmlConfigReader = new XmlConfigReader(CONFIG_XML_FILE_NAME, CONFIG_XSD_FILE_NAME, TARGET_NAMESPACE);
            Config config = xmlConfigReader.ReadConfig();
            ISimpleWebCrawler webCrawler = new SimpleWebCrawler(config.Depth);
            return await webCrawler.PerformCrawlingAsync(new List<string>(config.RootUrls));
        }
    }
}
