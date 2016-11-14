using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using WebCrawler.WebCrawlerResults;

namespace WebCrawlerTest.ViewModel.Converter
{
    public class WebCrawlerResultConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var items = new List<TreeViewItem>();

            var crawlResult = value as CrawlResult;
            if (crawlResult != null)
            {
                items = CreateTreeView(crawlResult);
            }

            return items;
        }

        private List<TreeViewItem> CreateTreeView(CrawlResult crawlResult)
        {
            var items = new List<TreeViewItem>();

            foreach (var item in crawlResult.InsertedUrls)
            {
                var newItem = CreateTreeViewItem(item);
                items.Add(newItem);
            }

            return items;
        }

        private TreeViewItem CreateTreeViewItem(CrawlItem crawlItem)
        {
            var treeViewItem = new TreeViewItem {Header = crawlItem.Url};

            foreach (var item in crawlItem.InsertedUrls)
            {
                var newItem = CreateTreeViewItem(item);
                treeViewItem.Items.Add(newItem);
            }

            return treeViewItem;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}