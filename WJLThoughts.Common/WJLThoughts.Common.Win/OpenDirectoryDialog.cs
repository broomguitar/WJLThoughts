using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace WJLThoughts.Common.Win
{
    public class OpenDirectoryDialog
    {
        public string DirectoryPath { get;private set; }
        public bool Open()
        {
            try
            {
                IntPtr pidlRet = IntPtr.Zero;
                int publicOptions = (int)Win32BrowseDirectory.BffStyles.RestrictToFilesystem |
                (int)Win32BrowseDirectory.BffStyles.RestrictToDomain;
                int privateOptions = (int)Win32BrowseDirectory.BffStyles.NewDialogStyle;
                // Construct a BROWSEINFO.
                Win32BrowseDirectory.BROWSEINFO bi = new Win32BrowseDirectory.BROWSEINFO();
                IntPtr buffer = Marshal.AllocHGlobal(1024);
                int mergedOptions = (int)publicOptions | (int)privateOptions;
                bi.pidlRoot = IntPtr.Zero;
                bi.pszDisplayName = buffer;
                bi.lpszTitle = "文件夹";
                bi.ulFlags = mergedOptions;
                Win32BrowseDirectoryInstance w = new Win32BrowseDirectoryInstance();
                bool bSuccess = false;
                IntPtr P = w.GetHandle(ref bSuccess);
                if (true == bSuccess)
                {
                    bi.hwndOwner = P;
                }
                pidlRet = Win32BrowseDirectory.Shell32.SHBrowseForFolder(ref bi);
                Marshal.FreeHGlobal(buffer);
                if (pidlRet == IntPtr.Zero)
                {
                    // User clicked Cancel.
                    return false;
                }
                byte[] pp = new byte[2048];
                if (0 == Win32BrowseDirectory.Shell32.SHGetPathFromIDList(pidlRet, pp))
                {
                    return true;
                }

                int nSize = 0;
                for (int i = 0; i < 2048; i++)
                {
                    if (0 != pp[i])
                    {
                        nSize++;
                    }
                    else
                    {
                        break;
                    }

                }

                if (0 == nSize)
                {
                    return true;
                }

                byte[] pReal = new byte[nSize];
                Array.Copy(pp, pReal, nSize);
                Encoding utf8 = Encoding.UTF8;
                byte[] utf8Bytes = Encoding.Convert(Encoding.GetEncoding("Gb2312"), utf8, pReal);
                string utf8String = utf8.GetString(utf8Bytes);
                utf8String = utf8String.Replace("\0", "");
                DirectoryPath= utf8String.Replace("\\", "/") + "/";
                return true;
            }
            catch (Exception e)
            {
               Core.LogUtils.Logger.Instance.Error("获取文件夹目录出错:" + e.Message);
                return false;
            }
        }
    }
    class Win32BrowseDirectory
    {
        // C# representation of the IMalloc interface.
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
        Guid("00000002-0000-0000-C000-000000000046")]
        public interface IMalloc
        {
            [PreserveSig]
            IntPtr Alloc([In] int cb);
            [PreserveSig]
            IntPtr Realloc([In] IntPtr pv, [In] int cb);
            [PreserveSig]
            void Free([In] IntPtr pv);
            [PreserveSig]
            int GetSize([In] IntPtr pv);
            [PreserveSig]
            int DidAlloc(IntPtr pv);
            [PreserveSig]
            void HeapMinimize();
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct BROWSEINFO
        {
            public IntPtr hwndOwner;
            public IntPtr pidlRoot;
            public IntPtr pszDisplayName;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpszTitle;
            public int ulFlags;
            [MarshalAs(UnmanagedType.FunctionPtr)]
            public Shell32.BFFCALLBACK lpfn;
            public IntPtr lParam;
            public int iImage;
        }

        [Flags]
        public enum BffStyles
        {
            RestrictToFilesystem = 0x0001, // BIF_RETURNONLYFSDIRS
            RestrictToDomain = 0x0002, // BIF_DONTGOBELOWDOMAIN
            RestrictToSubfolders = 0x0008, // BIF_RETURNFSANCESTORS
            ShowTextBox = 0x0010, // BIF_EDITBOX
            ValidateSelection = 0x0020, // BIF_VALIDATE
            NewDialogStyle = 0x0040, // BIF_NEWDIALOGSTYLE
            BrowseForComputer = 0x1000, // BIF_BROWSEFORCOMPUTER
            BrowseForPrinter = 0x2000, // BIF_BROWSEFORPRINTER
            BrowseForEverything = 0x4000, // BIF_BROWSEINCLUDEFILES
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class OpenFileName
        {
            public int structSize = 0;
            public IntPtr dlgOwner = IntPtr.Zero;
            public IntPtr instance = IntPtr.Zero;
            public String filter = null;
            public String customFilter = null;
            public int maxCustFilter = 0;
            public int filterIndex = 0;
            public String file = null;
            public int maxFile = 0;
            public String fileTitle = null;
            public int maxFileTitle = 0;
            public String initialDir = null;
            public String title = null;
            public int flags = 0;
            public short fileOffset = 0;
            public short fileExtension = 0;
            public String defExt = null;
            public IntPtr custData = IntPtr.Zero;
            public IntPtr hook = IntPtr.Zero;
            public String templateName = null;
            public IntPtr reservedPtr = IntPtr.Zero;
            public int reservedInt = 0;
            public int flagsEx = 0;
        }

        public class Shell32
        {
            public delegate int BFFCALLBACK(IntPtr hwnd, uint uMsg, IntPtr lParam, IntPtr lpData);

            [DllImport("Shell32.DLL")]
            public static extern int SHGetMalloc(out IMalloc ppMalloc);

            [DllImport("Shell32.DLL")]
            public static extern int SHGetSpecialFolderLocation(
              IntPtr hwndOwner, int nFolder, out IntPtr ppidl);

            [DllImport("Shell32.DLL")]
            public static extern int SHGetPathFromIDList(
              IntPtr pidl, byte[] pszPath);

            [DllImport("Shell32.DLL", CharSet = CharSet.Auto)]
            public static extern IntPtr SHBrowseForFolder(ref BROWSEINFO bi);
        }

        public class User32
        {
            public delegate bool delNativeEnumWindowsProc(IntPtr hWnd, IntPtr lParam);

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern bool EnumWindows(delNativeEnumWindowsProc callback, IntPtr extraData);

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern int GetWindowThreadProcessId(HandleRef handle, out int processId);
        }
    }
    //-------------------------------------------------------------------------
    class Win32BrowseDirectoryInstance
    {
        //-------------------------------------------------------------------------
        private HandleRef unityWindowHandle;
        private bool bUnityHandleSet;
        //-------------------------------------------------------------------------
        public IntPtr GetHandle(ref bool bSuccess)
        {
            bUnityHandleSet = false;
            Win32BrowseDirectory.User32.EnumWindows(__EnumWindowsCallBack, IntPtr.Zero);
            bSuccess = bUnityHandleSet;
            return unityWindowHandle.Handle;
        }
        //-------------------------------------------------------------------------
        private bool __EnumWindowsCallBack(IntPtr hWnd, IntPtr lParam)
        {
            int procid;
            int returnVal =
             Win32BrowseDirectory.User32.GetWindowThreadProcessId(new HandleRef(this, hWnd), out procid);

            int currentPID = System.Diagnostics.Process.GetCurrentProcess().Id;

            HandleRef handle =
             new HandleRef(this,
             System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle);

            if (procid == currentPID)
            {
                unityWindowHandle = new HandleRef(this, hWnd);
                bUnityHandleSet = true;
                return false;
            }
            return true;
        }
        //-------------------------------------------------------------------------
    }
}
