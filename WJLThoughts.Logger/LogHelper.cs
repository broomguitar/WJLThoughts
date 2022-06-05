using log4net;
using log4net.Config;
using log4net.Repository;
using System;
using System.IO;

namespace WJLThoughts.Logger
{
    public class LogHelper
    {
        private static ILoggerRepository repository { get; set; }
        private static ILog _log;
        private static ILog log
        {
            get
            {
                if (_log == null)
                {
                    Configure();
                }
                return _log;
            }
        }
        public static void Configure(string repositoryName = "NETCoreRepository", string configFile = "log4net.config")
        {
            repository = LogManager.CreateRepository(repositoryName);
            XmlConfigurator.Configure(repository, new FileInfo(configFile));
            _log = LogManager.GetLogger(repositoryName, "");
        }
        public static void Debug(object msg) { log.Debug(msg); }
        public static void Info(object msg) { log.Info(msg); }
        public static void Warn(object msg) { log.Warn(msg); }
        public static void Error(object msg) { log.Error(msg); }
        public static void Debug(object msg, Exception ex) { log.Debug(msg, ex); }
        public static void Info(object msg, Exception ex) { log.Info(msg,ex); }
        public static void Warn(object msg, Exception ex) { log.Warn(msg,ex); }
        public static void Error(object msg, Exception ex) { log.Error(msg,ex); }
    }
}
