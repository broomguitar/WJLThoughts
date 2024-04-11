using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WJLThoughts
{
    class TaskTest
    {
        public ObservableCollection<Log> Logs = new ObservableCollection<Log>();
        Random r = new Random(Environment.TickCount);
        private bool c1_pre, c2_pre, c3_pre, c4_pre, c5_pre, c6_pre;
        private bool c1
        {
            get
            {
                bool b = r.Next(0, 1000) == 100;
                bool ret = !c1_pre && b;
                c1_pre = b;
                return ret;
            }
        }
        private bool c2
        {
            get
            {
                bool b = r.Next(0, 1000) == 200;
                bool ret = !c2_pre && b;
                c2_pre = b;
                return ret;
            }
        }
        private bool c3
        {
            get
            {
                bool b = r.Next(0, 1000) == 300;
                bool ret = !c3_pre && b;
                c3_pre = b;
                return ret;
            }
        }
        private bool c4
        {
            get
            {
                bool b = r.Next(0, 1000) == 400;
                bool ret = !c4_pre && b;
                c4_pre = b;
                return ret;
            }
        }
        private bool c5
        {
            get
            {
                bool b = r.Next(0, 1000) == 500;
                bool ret = !c5_pre && b;
                c5_pre = b;
                return ret;
            }
        }
        private bool c6
        {
            get
            {
                bool b = r.Next(0, 1000) == 600;
                bool ret = !c6_pre && b;
                c6_pre = b;
                return ret;
            }
        }
        LoopWorker loopWorker1, loopWorker2, loopWorker3, loopWorker4, loopWorker5, loopWorker6;
        private void p1()
        {
            addLog("进入p1");
            for (int i = 0; i < 10000; i++)
            {
                for (int j = 0; j < 10000; j++)
                {
                    ;
                }
            }
            addLog("退出p1");

        }
        private void p2()
        {
            addLog("进入p2");
            for (int i = 0; i < 10000; i++)
            {
                for (int j = 0; j < 10000; j++)
                {
                    ;
                }
            }
            addLog("退出p2");
        }
        private void p3()
        {
            addLog("进入p3");
            for (int i = 0; i < 10000; i++)
            {
                for (int j = 0; j < 10000; j++)
                {
                    ;
                }
            }
            addLog("退出p3");
        }
        private void p4()
        {
            addLog("进入p4");
            for (int i = 0; i < 10000; i++)
            {
                for (int j = 0; j < 10000; j++)
                {
                    ;
                }
            }
            addLog("退出p4");
        }
        private void p5()
        {
            addLog("进入p5");
            for (int i = 0; i < 10000; i++)
            {
                for (int j = 0; j < 10000; j++)
                {
                    ;
                }
            }
            addLog("退出p5");
        }
        private void p6()
        {
            addLog("进入p6");
            for (int i = 0; i < 10000; i++)
            {
                for (int j = 0; j < 10000; j++)
                {
                    ;
                }
            }
            addLog("退出p6");

        }

        private void addLog(string msg)
        {
            try
            {
                App.Current?.Dispatcher.Invoke(
                    () =>
                    {
                        Logs.Insert(0, new Log(msg));
                    });
            }
            catch
            {

            }
        }
        public void Test()
        {
            for (int i = 0; i < 100; i++)
            {
                LoopWorker.Start(()=>c1, p1);
                LoopWorker.Start(() => c2, p2);
                LoopWorker.Start(() => c3, p3);
                LoopWorker.Start(() => c4, p4);
                LoopWorker.Start(() => c5, p5);
                LoopWorker.Start(() => c6, p6);
            }
        }


    }
    class Log
    {
        public Log(string msg)
        {
            Msg = msg;
        }
        public DateTime Time => DateTime.Now;
        public string Msg { get; set; }
    }
   public class LoopWorker
    {
     public static void Start(Func<bool> condition, Action worker)
        {
            Task.Factory.StartNew(
                () =>
                {
                    while (true)
                    {
                        if (condition())
                        {
                            Task.Factory.StartNew(worker);
                        }
                        Thread.Sleep(10);
                    }

                }, TaskCreationOptions.LongRunning
                );
        }
    }
}
