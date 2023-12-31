using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kaldoku.Baseclass
{
    public class App
    {
        public static bool IsDebugMode { get; set; } = false;
        private static ILog _LogObject = null;
        public static ILog LogObject
        {
            get
            {
                if (_LogObject == null)
                {
                    _LogObject = new txtFileLog(AppPath + @"\Kaldoku.log");
                }
                return _LogObject;
            }
            set
            {
                _LogObject = value;
            }
        }
        private static IShowMessage _ShowMessageObject = null;
        public static IShowMessage ShowMessageObject
        {
            get
            {
                if (_ShowMessageObject == null)
                {
                    _ShowMessageObject = new MsgBox();
                }
                return _ShowMessageObject;
            }
        }
        public static String AppPath => AppDomain.CurrentDomain.BaseDirectory;
        public static string DataFilePath => $@"{AppPath}\Data.xml";

    }
}
