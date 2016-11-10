using System;
using System.Windows;
using WebCrawler;
using WebCrawlerTest.Model;

namespace WebCrawlerTest.ViewModel
{
    internal class WebCrawlerViewModel : BaseViewModel
    {
        private CrawlResult crawlResult;

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

        private ClickingCommand clickingCommand;

        public ClickingCommand ClickingCommand
        {
            get
            {
                if (clickingCommand == null)
                    clickingCommand = new ClickingCommand(() => { ClickCount++; });

                return clickingCommand;
            }
        }

        private int clickCount = 0;

        public int ClickCount
        {
            get
            {
                return clickCount;
            }
            set
            {
                clickCount = value;
                OnPropertyChanged(nameof(ClickCount));
            }
        }

        public CrawlingCommand CrawlingCommand { get; private set; }

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
