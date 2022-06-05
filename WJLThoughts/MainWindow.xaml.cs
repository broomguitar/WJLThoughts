using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
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
using WJLThoughts.Common.WinAPis;

namespace WJLThoughts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public  partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Task.Delay(1000).Wait();
            this.log.ItemsSource = TT.Logs;

        }
        TaskTest TT = new TaskTest();
        private void btn_test_Click(object sender, RoutedEventArgs e)
        {
          BrowseDirectory.Instance.BroweFolder(out string ret);
            //TT.Test();
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           App.SplashScreenPage?.Dispatcher.Invoke((Action)(() => App.SplashScreenPage?.Close()));//在SplashScreenPage的线程上关闭SplashWindow
            this.Activate();//激活主窗体
        }
    }
}
