using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WebCrawlerTest
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int clickCount = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ClickButton_Click(object sender, RoutedEventArgs e)
        {
           //ClickingInfoLabel.Content = $"You have clicked {++clickCount} times.";
            
        }
    }
}
