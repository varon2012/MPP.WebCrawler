using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCrawlerUI.Model;
using System.Windows;
using System.Windows.Input;


namespace WebCrawlerUI.ViewModel
{
    public class MainViewModel
    {
        public Counter Counter { get; private set; } = new Counter();

        public ICommand CountCommand { get; set; }

        private void IncrementCount()
        {
            Counter.IncrementCount();
        }
    }
}
