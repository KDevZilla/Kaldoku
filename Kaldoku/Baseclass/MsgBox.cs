using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kaldoku.Baseclass
{
    public class MsgBox : IShowMessage
    {

        public void Show(string Message)
        {
            System.Windows.Forms.MessageBox.Show(Message);
        }
    }
}
