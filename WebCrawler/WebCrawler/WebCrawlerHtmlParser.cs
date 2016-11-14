using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace WebCrawler
{
    public class WebCrawlerHtmlParser : IWebCrawlerHtmlParser
    {       
        public List<string> GetInnerUrls(string htmlPage, string basePath)
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlPage);
            HtmlNodeCollection linkNodes = htmlDocument.DocumentNode.SelectNodes("//a");
            List<string> urls = new List<string>();

            if (linkNodes != null)
            {
                foreach (HtmlNode linkNode in linkNodes)
                {
                    if (linkNode.Attributes["href"] != null)
                    {
                        String hrefValue = linkNode.Attributes["href"].Value;
                        Uri uri = null;
                        if (Uri.IsWellFormedUriString(hrefValue, UriKind.Absolute))
                        {
                            uri = new Uri(hrefValue);

                        }
                        else
                        {
                            if (Uri.IsWellFormedUriString(hrefValue, UriKind.Relative))
                            {
                                Uri baseUri = new Uri(basePath);
                                Uri realtiveUri = new Uri(hrefValue, UriKind.Relative);
                                uri = new Uri(baseUri, realtiveUri);
                            }
                        }

                        if ((uri != null) && ((uri.Scheme == "http") || (uri.Scheme == "https")))
                        {
                            urls.Add(uri.ToString());
                        }
                    }
                }
            }

            return urls;
        }
    }
}
