using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebCrawler.HtmlParser
{
    public class HtmlParser : IHtmlParser
    {
        private const string LinkTag = "a";
        private const string HrefAttribute = "href";

        public async Task<List<string>> ParsePageForUrlAsync(string url)
        {
            if (url == null) throw new ArgumentNullException(nameof(url));

            var urls = new List<string>();
            try
            {
                var page = await LoadPageAsync(url);
                if (page == null) throw new ArgumentNullException(nameof(page));

                urls = GetUrlsFromPage(page);
            }
            catch (Exception)
            {
                
            }

            return urls;
        }

        private async Task<string> LoadPageAsync(string url)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetStringAsync(url);
                return response;
            }
        }

        private List<string> GetUrlsFromPage(string htmlPage)
        {
            var urlsFromPage = new List<string>();

            var parser = new AngleSharp.Parser.Html.HtmlParser();
            var document = parser.Parse(htmlPage);

            foreach (var link in document.QuerySelectorAll(LinkTag))
            {
                try
                {
                    urlsFromPage.Add(link.GetAttribute(HrefAttribute));
                }
                catch (Exception)
                {
                    
                }
            }

            return urlsFromPage;
        }
    }
}