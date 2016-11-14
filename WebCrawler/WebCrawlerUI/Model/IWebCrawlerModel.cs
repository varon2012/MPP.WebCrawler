using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCrawler;

namespace WebCrawlerUI.Model
{
    public interface IWebCrawlerModel
    {
        Task<CrawlResult> CrawlAsync();
    }
}
