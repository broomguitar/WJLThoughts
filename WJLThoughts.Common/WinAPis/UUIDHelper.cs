using System;
using System.Collections.Generic;
using System.Management;
using System.Text;

namespace WJLThoughts.Common.WinAPis
{
    public class UUIDHelper
    {
        /// <summary>
        /// 获取机器的UUID
        /// </summary>
        /// <returns></returns>
        public static string GetUUID()
        {
            try
            {
                string code = null;
                SelectQuery query = new SelectQuery("select * from Win32_ComputerSystemProduct");
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
                {
                    foreach (var item in searcher.Get())
                    {
                        using (item) code = item["UUID"].ToString();
                    }
                }
                return code;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
