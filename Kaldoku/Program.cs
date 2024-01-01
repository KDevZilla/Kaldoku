using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Kaldoku.Baseclass;
using System.Threading;
namespace Kaldoku
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new FormKaldoku());
            //Application.Run(new FormGenerateBlankBlock());
           

        }


        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {// All exceptions thrown by the main thread are handled over this method

            //ShowExceptionDetails(e.Exception);
            //ILog tlog = new txtFileLog(cApplication.AppPath + "PasswordManager.log");
            HandleException.Handle(e.Exception, Baseclass.App.LogObject, Baseclass.App.ShowMessageObject);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {// All exceptions thrown by additional threads are handled in this method

            //ShowExceptionDetails(e.ExceptionObject as Exception);



            HandleException.Handle(e.ExceptionObject as Exception, Baseclass.App.LogObject, Baseclass.App.ShowMessageObject);
            // Suspend the current thread for now to stop the exception from throwing.
           // Thread.CurrentThread.Suspend();
        }
    }
}
