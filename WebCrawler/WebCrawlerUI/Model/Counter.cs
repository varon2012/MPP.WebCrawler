using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawlerUI.Model
{
    public class Counter
    {
        public int Count { get; private set; } = 0;

        public void IncrementCount()
        {
            Count++;
        }
    }
}
