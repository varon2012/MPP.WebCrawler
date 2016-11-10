using System.ComponentModel;
using System.Runtime.CompilerServices;
using WebCrawler.WebCrawlerResults;
using WebCrawlerTest.Model;
using WebCrawlerTest.ViewModel.Commands;

namespace WebCrawlerTest.ViewModel
{
    internal class WebCrawlerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public AsyncCommand CrawlingCommand { get; private set; }
        public ClickCommand ClickingCommand { get; private set; }

        private readonly WebCrawlerModel _webCrawlerModel;
        private CrawlResult _crawlResult;

        private int _clickCount;
        
        public CrawlResult WebCrawlerResult
        {
            get { return _crawlResult; }
            set
            {
                if (_crawlResult != value)
                {
                    _crawlResult = value;
                    OnPropertyChanged();
                }
            }
        }

        public int ClickCount
        {
            get { return _clickCount; }
            set
            {
                if (_clickCount != value)
                {
                    _clickCount = value;
                    OnPropertyChanged();
                }
            }
        }

        public WebCrawlerViewModel()
        {
            _webCrawlerModel = new WebCrawlerModel();

            InitializeCrawlingCommand();
            InitializeClickingCommand();
        }

        private void InitializeCrawlingCommand()
        {
            CrawlingCommand = new AsyncCommand(
                async () =>
                {
                    if (CrawlingCommand.CanExecute)
                    {
                        CrawlingCommand.CanExecute = false;
                        try
                        {
                            WebCrawlerResult = await _webCrawlerModel.GetWebCrawlingResultAsync();
                        }
                        finally
                        {
                            CrawlingCommand.CanExecute = true;
                        }
                    }
                }
            );
        }

        private void InitializeClickingCommand()
        {
            ClickingCommand = new ClickCommand(
                () =>
                {
                    if (ClickingCommand.CanExecute)
                    {
                        ClickingCommand.CanExecute = false;
                        ClickCount++;
                        ClickingCommand.CanExecute = true;
                    }
                });
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}