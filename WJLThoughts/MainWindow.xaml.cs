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
            Task.Delay(3000).Wait();

        }

        private void btn_test_Click(object sender, RoutedEventArgs e)
        {
            WinApi.BrowseDirectory.Instance.BroweFolder(out string ret);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           App.SplashScreenPage?.Dispatcher.Invoke((Action)(() => App.SplashScreenPage?.Close()));//在SplashScreenPage的线程上关闭SplashWindow
            this.Activate();//激活主窗体
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {

        }
    }
}
