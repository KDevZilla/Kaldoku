using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kaldoku
{
    public class PregenratedNumber
    {

        static Dictionary<int, List<List<int>>> _DicList = new Dictionary<int, List<List<int>>>();
        static Dictionary<int, Dictionary<int, List<List<int>>>> AllDiclist = new Dictionary<int, Dictionary<int, List<List<int>>>>();

        private static Dictionary<int, List<List<int>>> GetDicList(int BoardSize, String DicListPath)
        {
            if (!AllDiclist.ContainsKey(BoardSize))
            {
                AllDiclist.Add(BoardSize, LoadDiclist(BoardSize, DicListPath));

            }

            return AllDiclist[BoardSize];


        }

        public static Dictionary<int, List<List<int>>> LoadDiclist(int NumberofDigit, String DicListPath)
        {

            Dictionary<int, List<List<int>>> DicList = new Dictionary<int, List<List<int>>>();
            int[] number = new int[NumberofDigit];
            int i = 0;
            for (i = 1; i <= NumberofDigit; i++)
            {
                DicList.Add(i, new List<List<int>>());
                number[i - 1] = i;
            }


            for (i = 1; i <= NumberofDigit; i++)
            {

                //string fileName = DicListPath + NumberofDigit.ToString() + "_" + i.ToString() + ".txt";
                string fileName = $"{DicListPath}{NumberofDigit.ToString()}_{i.ToString()}.txt";
                System.IO.StreamReader SR = new System.IO.StreamReader(fileName);
                string fileContent = SR.ReadToEnd();
                SR.Close();
                SR.Dispose();
                string[] arrLine = fileContent.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (String line in arrLine)
                {
                    List<int> lst = new List<int>();
                    foreach (char strchar in line.ToCharArray())
                    {
                        lst.Add(int.Parse(strchar.ToString()));
                    }
                    DicList[i].Add(lst);
                }



            }
            return DicList;
        }


        public static int[,] getTargetCellNumber(int boardSize)
        {
            int i;
            int j;

            var TargetCellValue = new int[boardSize, boardSize];
            int[] arrRowTemp = new int[boardSize];

            Dictionary<int, List<List<int>>> DicList = GetDicList(boardSize, FileUtil.PregeneratedDigitsPath);


            for (i = 1; i <= boardSize; i++)
            {
                Boolean IsValid = false;
                while (!IsValid)
                {
                    int indexRandommed = 0;
                    indexRandommed = Baseclass.MyRandom.Random(0, DicList[i].Count);
                    List<int> lst = DicList[i][indexRandommed];
                    IsValid = true;
                    for (j = 0; j < lst.Count && IsValid; j++)
                    {
                        int k;
                        for (k = 0; k <= boardSize - 1; k++)
                        {
                            if (TargetCellValue[k, j] == lst[j])
                            {
                                IsValid = false;

                            }
                        }
                    }

                    if (IsValid)
                    {
                        for (j = 0; j < lst.Count; j++)
                        {
                            TargetCellValue[i - 1, j] = lst[j];

                        }
                    }


                }
            }

            //Swap Row;
            for (i = 0; i <= 30; i++)
            {
                int iFirstRow = Baseclass.MyRandom.Random(0, boardSize);
                int iSecondRow = Baseclass.MyRandom.Random(0, boardSize);
                if (iFirstRow == iSecondRow)
                {
                    continue;
                }

                for (j = 0; j <= boardSize - 1; j++)
                {
                    arrRowTemp[j] = TargetCellValue[iSecondRow, j];
                    TargetCellValue[iSecondRow, j] = TargetCellValue[iFirstRow, j];
                    TargetCellValue[iFirstRow, j] = arrRowTemp[j];

                }

            }
            return TargetCellValue;
        }
    }
}
