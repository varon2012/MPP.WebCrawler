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
        private bool isCorrectlyRead;

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
            if (ReadConfiguration(webCrawlerModel))
                ExecuteCrawlingCommand(webCrawlerModel);
        }

        private bool ReadConfiguration(WebCrawlerModel webCrawlerModel)
        {
            try
            {
                webCrawlerModel.ReadConfigInformation();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.Message}");
                return false;
            }

        }

        private void ExecuteCrawlingCommand(WebCrawlerModel webCrawlerModel)
        {
            try
            {
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
            finally
            {
                if (webCrawlerModel.WebCrawler != null)
                    webCrawlerModel.WebCrawler.Dispose();
            }
        }
    }
}
