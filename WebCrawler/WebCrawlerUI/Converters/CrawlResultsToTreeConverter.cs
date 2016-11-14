using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WebCrawler;
using System.Windows.Controls;

namespace WebCrawlerUI.Converters
{
    class CrawlResultsToTreeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            CrawlResult crawlResult = value as CrawlResult;

            if (crawlResult == null)
            {
                return null;
            }


            List<TreeViewItem> rootNodes = new List<TreeViewItem>();

            rootNodes = CreateTreeViewItem(crawlResult);            

            return rootNodes;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static List<TreeViewItem> CreateTreeViewItem(CrawlResult crawlResult)
        {
            List<TreeViewItem> result = new List<TreeViewItem>();
            foreach (KeyValuePair<string, CrawlResult> urlAndCrawlResult in crawlResult.InnerCrawlResults)
            {
                TreeViewItem treeViewItem = new TreeViewItem();
                treeViewItem.Header = urlAndCrawlResult.Key;
                if (urlAndCrawlResult.Value != null)
                {
                    foreach (TreeViewItem nestedTreeViewItem in CreateTreeViewItem(urlAndCrawlResult.Value))
                    {
                        treeViewItem.Items.Add(nestedTreeViewItem);

                    }
                }
                result.Add(treeViewItem);
            }
            return result;
        }
    }
}
