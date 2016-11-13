using System.Collections.Generic;
using System.Threading.Tasks;
using WebCrawlerUI.Model;
using System.Windows.Input;
using WebCrawler;
using System.IO;
using System;
using System.Windows;
using WebCrawlerUI.Converters;


namespace WebCrawlerUI.ViewModel
{
    internal class MainViewModel : ObservableObject
    {

        private CrawlResult _crawlResult;

        public MainViewModel()
        {
            CountCommand = new Command(IncrementCountCommand);
            Counter = new Counter();
            CrawlCommand = new AsyncCommand(CrawlAsyncCommand);
        }

        public Counter Counter { get; private set; }

        public CrawlResult CrawlResult
        {
            get { return _crawlResult; }
            set
            {
                _crawlResult = value;
                OnPropertyChanged(nameof(CrawlResult));
            }
        } 

        public ICommand CountCommand { get; private set; }

        public IAsyncCommand  CrawlCommand { get; private set; }

        private void IncrementCountCommand()
        {
            Counter.IncrementCount();
            OnPropertyChanged(nameof(Counter));
        }
        
        private async Task<CrawlResult> CrawlAsyncCommand()
        {
            return await CrawlAsyncCommand(new WebCrawlerModel());
        }

        private async Task<CrawlResult> CrawlAsyncCommand(IWebCrawlerModel webCrawlerModel)
        {
            try
            {
                CrawlResult = await webCrawlerModel.CrawlAsync();
                
                return CrawlResult;
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Configuration file is not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;    
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Configuration file is not valid.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            catch (Exception)
            {
                MessageBox.Show("An error ocurred while crawling.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
    }
}
