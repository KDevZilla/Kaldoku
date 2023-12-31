using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kaldoku
{
    public class Array2d<T>
    {
        T[,] value = null;
        public int Row => value.GetUpperBound(0);
        public int Col => value.GetUpperBound(1);
        
        public void ForEach(Action<T> action)
        {
            int i;
            int j;
            for (i = 0; i < Row; i++)
            {
                for (j = 0; j < Col; j++)
                {
                    action(value[i, j]);
                }
            }
            /*
            Array2d<int> arr2d=new Array2d<int> ();
            StringBuilder strB = new StringBuilder();
            arr2d.ForEach(x => strB.Append(x));
            Array2d<int> Matrix = new Array2d<int>();
            Matrix.value[0, 1] = 5;

            Matrix.ForEach(x => strB.Append(x));
            Matrix.ForEach(x => x = 0);
            */

        }
       
    }
}
