using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler
{
    internal class ConsoleLogger : ILogger
    {
        public void WriteLine(string exceptionMessage, string url)
        {
            System.Diagnostics.Debug.WriteLine($"{exceptionMessage} {url}");
        }
    }
}
