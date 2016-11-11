using System;
using System.Windows;
using WebCrawler;
using WebCrawlerTest.Model;

namespace WebCrawlerTest.ViewModel
{
    internal class WebCrawlerViewModel : BaseViewModel
    {
        private WebCrawlerModel webCrawlerModel;

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


        public CrawlingCommand CrawlingCommand
        {
            get
            {
               return CreateCrawlingCommand(webCrawlerModel);
            }
        }
        
        private string exceptionMessage = string.Empty;

        public string ExceptionMessage
        {
            get
            {
                return exceptionMessage;
            }
            set
            {
                exceptionMessage = value;
                OnPropertyChanged(nameof(ExceptionMessage));
            }
        }

        public WebCrawlerViewModel()
        {
            webCrawlerModel = new WebCrawlerModel();
        }

        private bool ReadConfiguration(WebCrawlerModel webCrawlerModel)
        {
            try
            {
                webCrawlerModel.ReadConfigInformation();
                ExceptionMessage = string.Empty;
                return true;
            }
            catch (Exception e)
            {
                ExceptionMessage = e.Message;
                return false;
            }
        }

        private CrawlingCommand CreateCrawlingCommand(WebCrawlerModel webCrawlerModel)
        {
            System.Diagnostics.Debug.WriteLine(System.Threading.Thread.CurrentThread.ManagedThreadId);
            CrawlingCommand crawlingCommand = new CrawlingCommand(
                    async () =>
                    {
                        if (ReadConfiguration(webCrawlerModel))
                        {
                            if (CrawlingCommand.CanExecute(null))
                            {
                                CrawlingCommand.Disable();
                                CrawlResult = await webCrawlerModel.StartWebCrawler();
                                CrawlingCommand.Enable();
                            }
                        }
                    });
            return crawlingCommand;
        }
    }
}
