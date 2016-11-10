using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebCrawler.HtmlParser
{
    public interface IHtmlParser
    {
        ConcurrentBag<Exception> ParserErrors { get; }

        Task<List<string>> ParsePageForUrlAsync(string url);
    }
}