using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler
{
    internal interface ILogger
    {
        void WriteLine(string exceptionMessage, string url);
    }
}
