using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebCrawler.HtmlParser
{
    public interface IHtmlParser
    {
        List<Exception> ParserErrors { get; }

        Task<List<string>> ParsePageForUrlAsync(string url);
    }
}