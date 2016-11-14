using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler
{
    public interface IWebCrawlerHtmlParser
    {
        List<string> GetInnerUrls(string htmlPage, string basePath);
    }
}
