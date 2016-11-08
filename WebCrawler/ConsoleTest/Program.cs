using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCrawler;
using System.Net.Http;
using HtmlAgilityPack;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string ozdic = "http://ozdic.com/";
            string httpAddress = "https://www.google.by/?gws_rd=ssl";
            List<string> urls = new List<string> {httpAddress, ozdic};
            //string htmlPage = (new HttpClient()).GetStringAsync(httpAddress).GetAwaiter().GetResult();
            //List<string> urls = HtmlParser.GetInnerUrls(htmlPage, httpAddress);

            SimpleWebCrawler webCrawler = new SimpleWebCrawler(2);
            List<CrawlResult> crawlResults = webCrawler.PerformCrawlingAsync(urls).GetAwaiter().GetResult();

            Console.WriteLine("Done");
            string log = webCrawler.Log;
            Console.ReadLine();
            Console.WriteLine(log);
            Console.ReadLine();
        }

        //private void PrintResults(CrawlResult crawlResult, string indent)
        //{
        //    foreach(KeyValuePair<string, CrawlResult>)
        //}
    }
}
