
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace WJLThoughts.Common.Win
{
    public class PingHelper
    {
        /// <summary>
        /// 检测远程IP是否用
        /// </summary>
        /// <param name="remoteIp">对方IP</param>
        /// <param name="timeOut">超时时间 ，单位毫秒</param>
        /// <returns></returns>
        public static bool CheckRemoteStatus(IPAddress remoteIp, int timeOut = 1000)
        {
            try
            {
                Ping ping = new Ping();
                PingReply pingReply = ping.Send(remoteIp, timeOut);
                return pingReply.Status == IPStatus.Success;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 检测远程地址
        /// </summary>
        /// <param name="remoteIp">对方地址</param>
        /// <param name="timeOut">超时时间 ，单位毫秒</param>
        /// <returns></returns>
        public static bool CheckRemoteStatus(string address, int timeOut = 1000)
        {
            try
            {
                Ping ping = new Ping();
                PingReply pingReply = ping.Send(address, timeOut);
                return pingReply.Status == IPStatus.Success;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

