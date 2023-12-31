using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kaldoku
{
    public class FileUtil
    {
        public static string CurrentPath => System.AppDomain.CurrentDomain.BaseDirectory;
        public static string PregeneratedDigitsPath => $"{FileUtil.CurrentPath}PreGeneratedDigits\\";
        public static string PregeneratedBlankBlock => $"{FileUtil.CurrentPath}PregeneratedBlankBlock\\";

        //Old unused
        //public static string Pregenerated_Blank_Block => $"{FileUtil.CurrentPath}Pregenerated_Blank_Block\\";


        public static bool IsFolderExist(String path) => System.IO.Directory.Exists(path);
        //Pregenerated_Blank_Block

    }
}
