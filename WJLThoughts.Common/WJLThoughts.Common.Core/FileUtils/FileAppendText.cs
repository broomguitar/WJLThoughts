using System;
using System.Collections.Generic;
using System.Text;
using WJLThoughts.Common.Core.LogUtils;

namespace WJLThoughts.Common.Core.FileUtils
{
    public class FileAppendText
    {
        private readonly static ReaderWriterLockSlim _logWriteLock = new ReaderWriterLockSlim();
        public static void AppendText(string path, string content)
        {
            try
            {
                _logWriteLock.EnterWriteLock();
                File.AppendAllText(path, content);
            }
            catch (Exception ex)
            {

                Logger.Instance.Error(ex.ToString());
            }
            finally
            {
                _logWriteLock.ExitWriteLock();
            }

        }
    }
}
