using log4net.Repository.Hierarchy;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WJLThoughts.Common.Win;
using WJLThoughts.Common.WPF.Image;
using WJLThoughts.HardwareDevice.Camera;

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
            TT.Test();
            return;
            var dd = MyCamera_HIKArea.GetDeviceList(CameraConnectTypes.GigE);
            IMyCamera data = new MyCamera_HIKArea(CameraConnectTypes.GigE, dd.First()?.SN);
            if (data.Open())
            {
                data.NewImageEvent += Data_NewImageEvent;
                data.ContinousGrab();
            }
        }
       WriteableBitmapHelper writeableBitmapHelper;
        private void Data_NewImageEvent(object sender, Bitmap e)
        {
           if(e != null)
            {
                try
                {
                    if(writeableBitmapHelper== null||writeableBitmapHelper.WriteableBitmap.Width!=e.Width||writeableBitmapHelper.WriteableBitmap.Height!=e.Height)
                    {
                        writeableBitmapHelper=new WriteableBitmapHelper();
                        writeableBitmapHelper.InitialWriteableBitmap(e.Width,e.Height,e.PixelFormat);
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            img.Source = writeableBitmapHelper.WriteableBitmap;
                        });
                    }
                    writeableBitmapHelper.GetImage(e);
                }
                catch (Exception ex)
                {
                    Common.Core.LogUtils.Logger.Instance.Error("Camera",ex);
                }
                finally
                {
                    e.Dispose();
                }
                
            }
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
