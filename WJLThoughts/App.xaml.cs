using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WJLThoughts.Common.Core.Logger;

namespace WJLThoughts
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static SplashScreen SplashScreenPage = null;
        private Mutex mutex;
        protected override void OnStartup(StartupEventArgs e)
        {
            mutex = new Mutex(true, "SoftPainter_MainVision", out bool flag);
            if (!flag)
            {
                MessageBox.Show(string.Format("程序已经运行!", "提示"));
                Environment.Exit(9);//强关
            }
            Application.Current.DispatcherUnhandledException += new System.Windows.Threading.DispatcherUnhandledExceptionEventHandler(Application_DispatcherUnhandledException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Thread t = new Thread(() =>
            {
                SplashScreenPage = new SplashScreen();
                SplashScreenPage.ShowDialog();//不能用Show
            });
            t.SetApartmentState(ApartmentState.STA);//设置单线程
            t.Start();
            base.OnStartup(e);
        }
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            string str = "";
            Exception error = e.Exception as Exception;
            if (error != null)
            {
                str = string.Format("Application UnhandledException:{0};\n堆栈信息:{1}", error.Message, error.StackTrace);
            }
            else
            {
                str = string.Format("Application UnhandledError:{0}", e);
            }
            //阻止程序崩溃
            e.Handled = true;
            try
            {
                LogHelper.Instance.Error(str, error);
            }
            catch { }
        }

        private void CurrentDomain_UnhandledException(Object sender, UnhandledExceptionEventArgs e)
        {
            string str = "";
            Exception error = e.ExceptionObject as Exception;
            if (error != null)
            {
                str = string.Format("Application UnhandledException:{0};\n堆栈信息:{1}", error.Message, error.StackTrace);
            }
            else
            {
                str = string.Format("Application UnhandledError:{0}", e);
            }
            try
            {
               LogHelper.Instance.Error(str);
            }
            catch { }
        }
    }
}
