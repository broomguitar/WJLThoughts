using log4net;
using log4net.Config;
using log4net.Repository;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace WJLThoughts.Common.Core.Logger
{
    public class LogHelper
    {
        private static readonly object _lockObj = new object();
        private static LogHelper _instance;
        public static LogHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new LogHelper();
                        }
                    }
                }
                return _instance;
            }
        }
        private ILoggerRepository repository { get; set; }
        private ILog _log;
        private void Config(string repositoryName = "WJLLog4net", string configFile = "log4net.config")
        {
            repository = LogManager.CreateRepository(repositoryName);
            XmlConfigurator.Configure(repository, new FileInfo(configFile));
            _log = LogManager.GetLogger(repositoryName, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }
        private LogHelper()
        {
            Config();
        }
        /// <summary>
        /// 调试日志信息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="exception"></param>
        public void Debug(object msg)
        {
            if (_log.IsDebugEnabled)
            {
                _log.Debug($"【{msg}】");
            }
        }
        /// <summary>
        /// 调试日志信息
        /// </summary>
        /// <param name="type"></param>
        /// <param name="content"></param>
        public void Debug(string type, string content)
        {
            if (_log.IsDebugEnabled)
            {
                _log.DebugFormat("【{0}】====>>>【{1}】", type, content);
            }
        }
        /// <summary>
        /// 错误日志信息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="exception"></param>
        public void Error(object msg, Exception exception = null)
        {
            if (!_log.IsErrorEnabled) return;
            if (exception == null)
            {
                _log.Error($"【{msg}】");
            }
            else
            {
                _log.Error($"【{msg}】", exception);
            }
        }
        /// <summary>
        /// 错误日志信息
        /// </summary>
        /// <param name="type"></param>
        /// <param name="content"></param>
        public void Error(string type, string content)
        {
            if (_log.IsErrorEnabled)
            {
                _log.ErrorFormat("【{0}】====>>>【{1}】", type, content);
            }
        }
        /// <summary>
        /// 致命异常日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="exception"></param>
        public void Fatal(object msg, Exception exception = null)
        {
            if (!_log.IsFatalEnabled) return;
            if (exception == null)
            {
                _log.Fatal($"【{msg}】");
            }
            else
            {
                _log.Fatal($"【{msg}】", exception);
            }
        }
        /// <summary>
        /// 信息日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="exception"></param>
        public void Info(object msg)
        {
            if (_log.IsInfoEnabled)
            {
                _log.Info($"【{msg}】");
            }
        }
        /// <summary>
        /// 信息日志
        /// </summary>
        /// <param name="type"></param>
        /// <param name="content"></param>
        public void Info(string type, string content)
        {
            if (_log.IsInfoEnabled)
            {
                _log.InfoFormat("【{0}】====>>>【{1}】", type, content);
            }
        }
        /// <summary>
        /// 警告日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="exception"></param>
        public void Warn(object msg, Exception exception = null, [CallerFilePath] string callerPath = "", [CallerMemberName] string callerFunc = "")
        {
            if (!_log.IsWarnEnabled) return;
            if (exception == null)
            {
                _log.Warn($"【{msg}】");
            }
            else
            {
                _log.Warn($"【{msg}】", exception);
            }
        }
    }
}
