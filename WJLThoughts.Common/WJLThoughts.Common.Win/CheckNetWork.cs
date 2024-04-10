using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;

namespace WJLThoughts.Common.Win
{
    public class CheckNetWork
    {
        //InternetGetConnectedState返回的状态标识位的含义：
        private const int INTERNET_CONNECTION_MODEM = 1;
        private const int INTERNET_CONNECTION_LAN = 2;
        private const int INTERNET_CONNECTION_PROXY = 4;
        private const int INTERNET_CONNECTION_MODEM_BUSY = 8;
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int connectionDescription, int reservedValue);
        /// <summary>
        /// 是否可以连接外网
        /// </summary>
        /// <param name="netstatus">状态描述</param>
        /// <returns></returns>
        public static bool CanConectOutNet(out string netstatus)
        {
            try
            {
                netstatus = string.Empty;
                var connection = 0;
                if (!InternetGetConnectedState(out connection, 0))
                {
                    netstatus = "未联网!";
                    return false;
                }
                else
                {
                    if ((connection & INTERNET_CONNECTION_MODEM) != 0)
                        netstatus += " 采用调治解调器上网|";
                    if ((connection & INTERNET_CONNECTION_LAN) != 0)
                        netstatus += " 采用网卡上网|";
                    if ((connection & INTERNET_CONNECTION_PROXY) != 0)
                        netstatus += " 采用代理上网|";
                    if ((connection & INTERNET_CONNECTION_MODEM_BUSY) != 0)
                        netstatus += " MODEM被其他非INTERNET连接占用|";
                    IPAddress[] addresslist = Dns.GetHostAddresses("www.baidu.com");
                    if (addresslist[0].ToString().Length <= 6)
                    {
                        netstatus += " 无法访问外网|";
                        return false;
                    }
                }
                return true;
            }
            catch (Exception)
            {
                netstatus = "检测异常!";
                return false;
            }
        }
        /// <summary>
        /// 是否有本地网络
        /// </summary>
        /// <returns></returns>
        public static bool CanConectNet()
        {
            try
            {
                var connection = 0;
                return InternetGetConnectedState(out connection, 0);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
