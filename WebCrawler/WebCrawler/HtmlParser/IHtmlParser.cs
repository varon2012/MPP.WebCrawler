using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebCrawler.HtmlParser
{
    public interface IHtmlParser
    {
        Task<List<string>> ParsePageForUrlAsync(string url);
    }
}