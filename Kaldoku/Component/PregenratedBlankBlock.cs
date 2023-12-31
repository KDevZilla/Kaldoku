using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kaldoku
{
    public class PregenratedBlankBlock
    {
        public static Dictionary<int, List<String>> dicPregenerate = new Dictionary<int, List<string>>();
        public static List<String> LoadPregenrate(int boardSize)
        {

            string filePath = $"{FileUtil.PregeneratedBlankBlock}{boardSize}x{boardSize}.txt";
            List<String> listPregenerate = new List<string>();
            using (System.IO.StreamReader sr = new System.IO.StreamReader(filePath))
            {
                string filecontent = sr.ReadToEnd();
                string[] arrFileContent = filecontent.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                int i;
                for (i = 0; i < arrFileContent.Length; i++)
                {
                    listPregenerate.Add(arrFileContent[i]);
                }
                sr.Close();
                sr.Dispose();
            }
            return listPregenerate;
        }
        public static String GetListString(int boardSize, int mapIndex)
        {
            if (!dicPregenerate.ContainsKey(boardSize))
            {
                dicPregenerate.Add(boardSize, LoadPregenrate(boardSize));
            }
            if (dicPregenerate[boardSize].Count - 1 < mapIndex ||
                0 > mapIndex)
            {
                throw new ArgumentException($"The board size{boardSize} " +
                    $"has map number between 0 to {dicPregenerate[boardSize].Count - 1} while your mapIndex value is {mapIndex}");
            }
            return dicPregenerate[boardSize][mapIndex];
        }

        public static String GetListStringByRandom(int boardSize)
        {
            int mapIndex = Baseclass.MyRandom.Random(0, 99);
            return GetListString(boardSize, mapIndex);

        }

    }
}
