using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NLog;
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
        private static Logger _logger;

        public WebCrawlerModel()
        {
            var htmlParser = new HtmlParser();
            _configReader = new ConfigReader.ConfigReader();
            _logger = LogManager.GetCurrentClassLogger();

            var depth = 0;
            try
            {
                _configReader.LoadFile(ConfigFileName);
                depth = _configReader.GetDepth();
            }
            catch (Exception e)
            {
                _logger.Warn(e.Message);
            }

            _webCrawler = new SimpleWebCrawler(htmlParser, depth);
        }

        public async Task<CrawlResult> GetWebCrawlingResultAsync()
        {
            var rootUrls = new List<string>();

            try
            {
                rootUrls = _configReader.GetUrls();
            }
            catch (Exception e)
            {
                _logger.Warn(e.Message);
            }

            return await _webCrawler.PerformCrawlingAsync(rootUrls.ToArray());
        }
    }
}