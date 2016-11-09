using System;
using System.Windows;
using WebCrawler;
using WebCrawlerTest.Model;

namespace WebCrawlerTest.ViewModel
{
    internal class WebCrawlerViewModel : BaseViewModel
    {
        public CrawlResult crawlResult;
        public CrawlingCommand CrawlingCommand { get; set; }

        public CrawlResult CrawlResult
        {
            get
            {
                return crawlResult;
            }
            set
            {
                crawlResult = value;
                OnPropertyChanged(nameof(CrawlResult));
            }
        }

        public WebCrawlerViewModel()
        {
            WebCrawlerModel webCrawlerModel = new WebCrawlerModel();

            try
            {
                webCrawlerModel.ReadConfigInformation();
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.Message}. The program exits.");
                Environment.Exit(0);
            }

            CrawlingCommand = new CrawlingCommand(
                async () =>
                {
                    if (CrawlingCommand.CanExecute(null))
                    {
                        CrawlingCommand.Disable();

                        CrawlResult = await webCrawlerModel.StartWebCrawler();
                        CrawlingCommand.Enable();

                    }
                });

        }
    }
}
