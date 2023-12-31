using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kaldoku.Baseclass
{
    public class HandleException
    {
        public static String Message = "There is a problem. Please contact Administrator to look in to the log file";
        public static void Handle(Exception ex, ILog l, IShowMessage pIShow)
        {
            l.Log("Exception::" + ex.Message);

            //System.Windows.Forms.MessageBox.Show("There is a problem. Please contact Administrator to look in to the log file");
            pIShow.Show(Message);

        }
    }
}
