using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using WebCrawler;

namespace WebCrawlerTest.Converter
{
    internal class CrawlResultConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IEnumerable<TreeViewItem> treeItems = new List<TreeViewItem>();
            CrawlResult crawlResult = (CrawlResult)value;

            if (crawlResult != null)
                treeItems = ConvertCrawlResultToTreeItems(crawlResult);

            return treeItems;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<TreeViewItem> ConvertCrawlResultToTreeItems(CrawlResult crawlResult)
        {
            List<TreeViewItem> resultTree = new List<TreeViewItem>();
            foreach(var url in crawlResult.Urls)
            {
                TreeViewItem treeViewItem = new TreeViewItem();
                treeViewItem.Header = url.Key;

                if (url.Value != null)
                    foreach (TreeViewItem childrenItems in ConvertCrawlResultToTreeItems(url.Value))
                        treeViewItem.Items.Add(childrenItems);

                resultTree.Add(treeViewItem);
            }

            return resultTree;
        }
    }
}
