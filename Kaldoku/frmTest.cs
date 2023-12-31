using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace Kaldoku
{
    public partial class frmTest : Form
    {
        public frmTest()
        {
            InitializeComponent();
        }
        private Board GenBlankBlock()
        {
            GeneratePiece cGen = new GeneratePiece(new BasicPieceGenerator());
            //cGeneratePiece cGen = new cGeneratePiece(new SimplePieceGenerator());

            int iMaximumAttempt = 10000000;
            int iNoofTryBeforeStepBack = 40;
            int iCount = 0;
            int iCountCannotPut = 0;
            int iNoofTimeNeedToStepBack = 0;
            Boolean IsSuccess = true;

            Piece.PieceType[] arrPieceTest ={ Piece.PieceType.I4,
                                                Piece.PieceType.L2 ,
                                                Piece.PieceType.L3};
           

            while (!Board.IsFullWithBlock() && iCount < iMaximumAttempt)
            {
                /*
                 * I4,L2,L3
                 */
                Piece Pie = cGen.GenPiece();
                /*
                if (iCountArr < arrPieceTest.GetLength(0))
                {
                    Pie = Piece.Create(arrPieceTest[iCountArr]);
                    iCountArr++;
                }
                 **/

                Boolean CanPut = Board.TryToPut(Pie);
                iCount++;

                if (CanPut)
                {
                    iCountCannotPut = 0;
                }
                else
                {
                    iCountCannotPut++;
                    if (iCountCannotPut >= iNoofTryBeforeStepBack)
                    {
                        iCountCannotPut = 0;
                        Board.UnputLastPiece();
                        iNoofTimeNeedToStepBack++;

                        if (iNoofTimeNeedToStepBack >= 30)
                        {
                            Board.UnputLastNPiece(5);
                            iNoofTimeNeedToStepBack = 0;
                        }
                    }
                }


                if (iCount >= iMaximumAttempt)
                {
                    this.textBox1.Text = "There must be something wrong.";
                    IsSuccess = false;
                }
            }

            if (IsSuccess)
            {
                this.textBox1.Text = "Success" + Environment.NewLine + iCount.ToString() + Environment.NewLine +
                    Board.GetBoardPosition();
            }
            else
            {
                this.textBox1.Text = "Failed" + Environment.NewLine +
                    Board.GetBoardPosition();
            }
            return Board;

        }
        Board Board = new Board();
        private void button1_Click(object sender, EventArgs e)
        {
            Board = new Board();
            Board = GenBlankBlock();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Piece P = null;
            P = Piece.Create(Piece.PieceType.I2, Piece.PieceRotation.Rotate360);

            if (P.lstCell[0].Row != 0 ||
               P.lstCell[0].Col != 0 ||
                P.lstCell[1].Row != 1 ||
                P.lstCell[1].Col != 0)
            {
                this.textBox1.Text += " Invalid case of I2 create";
            }
            else
            {
                this.textBox1.Text += " Valid case of I2 create";
            }

            //P = Piece.Create(Piece.PieceType.L3, Piece.PieceRotation.Rotate360);


        }

        private void button3_Click(object sender, EventArgs e)
        {
            //cPermutation.ClearResult();
            //cPermutation.GetPer("123456".ToCharArray ());
            StringBuilder strB = new StringBuilder();
            foreach (var c in Permutation.CombinationsWithRepetition(new int[] { 1, 2, 3, 4, 5, 6 }, 6))
            {
                strB.Append(c).Append(Environment.NewLine);
            }

          
            /*
            for (i = 0; i < cPermutation.lstResult.Count; i++)
            {
                strB.Append(cPermutation.lstResult[i]).Append(Environment.NewLine);
            }
             */

            this.textBox1.Text = strB.ToString();


        }
        private string CurrentPath
        {
            get
            {
                return System.AppDomain.CurrentDomain.BaseDirectory;
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {

            Board = new Board();
            Board = GenBlankBlock();
          //  Board.GenerateTargetNumber(CurrentPath + @"\PreGeneratedDigits\");

            this.textBox1.Text = Board.strB.ToString();


        }

        private void frmTest_Load(object sender, EventArgs e)
        {
            this.Icon = Resource1.KaldokuLogo32x32;
        }

        //List<String> lst = new List<string>();
        //Dictionary<int, List<int>> DicList = new Dictionary<int, List<int>>();
        Dictionary<int, List<List<int>>> DicList = new Dictionary<int, List<List<int>>>();
        private void button5_Click(object sender, EventArgs e)
        {
            StringBuilder strB = new StringBuilder();
            DicList =new Dictionary<int,List<List<int>>> ();
            
            DicList.Add (1,new List<List<int>>());
            DicList.Add (2,new List<List<int>>());
            DicList.Add (3,new List<List<int>>());
            DicList.Add (4,new List<List<int>>());
            DicList.Add (5,new List<List<int>>());
            DicList.Add (6,new List<List<int>>());

            
            foreach (var c in Permutation.CombinationsWithRepetition   (new int[] { 1, 2, 3, 4, 5, 6 }, 6))
            {
                //strB.Append(c).Append(Environment.NewLine);
                //strB.Append(c).Append(Environment.NewLine);
                HashSet<String> Hsh = new HashSet<string>();

                Boolean IsValid =true;
                foreach (char ca in c)
                {
                    if(Hsh.Contains (ca.ToString ())){
                        IsValid =false ;
                        continue ;
                    }

                    Hsh.Add (ca.ToString ());
                }
                if (IsValid)
                {
                    //lst.Add(c.ToString ());
                    int First = int.Parse(c.Substring(0, 1));
                    List<int> lstTemp = new List<int>();
                    foreach (char ca in c)
                    {
                        //DicList[First].Add( int.Parse (ca.ToString ()));
                        lstTemp.Add(int.Parse(ca.ToString()));
                    }
                    DicList[First].Add(lstTemp);

                }
            }

            /*
            var result = cPermutation.GetPermutations  (new int[] { 1, 2, 3, 4, 5, 6,6,6 }, 6);
            foreach (var perm in result)
            {
                foreach (var c in perm)
                {
                    //Console.Write(c + " ");
                    strB.Append (c);
                }
                strB.Append (Environment.NewLine );
            }
            */

            /*
            for (i = 0; i < cPermutation.lstResult.Count; i++)
            {
                strB.Append(cPermutation.lstResult[i]).Append(Environment.NewLine);
            }
             */

            this.textBox1.Text = strB.ToString();
        }
        Random R = new Random();
        private void button6_Click(object sender, EventArgs e)
        {
            int i;
            int j;
            Board c = new Board();
            int[,] array = new int[6, 6];
            int[] arrRowTemp = new int[6];
            Dictionary<int, HashSet<int>> DicDup = new Dictionary<int, HashSet<int>>();

            DicDup.Add(1, new HashSet<int>());
            DicDup.Add(2, new HashSet<int>());
            DicDup.Add(3, new HashSet<int>());
            DicDup.Add(4, new HashSet<int>());
            DicDup.Add(5, new HashSet<int>());
            DicDup.Add(6, new HashSet<int>());

            
            for (i = 1; i <= 6; i++)
            {
                Boolean IsValid = false;
                while (!IsValid)
                {
                    int indexRandommed = 0;
                    indexRandommed = R.Next(DicList[i].Count);
                    List<int> lst = DicList[i][indexRandommed];
                    IsValid = true;
                    for (j = 0; j < lst.Count && IsValid; j++)
                    {
                        int k;
                        for (k = 0; k <= 5; k++)
                        {
                            if (array[k, j] == lst[j])
                            {
                                IsValid = false;
                                
                            }
                        }
                    }

                    if (IsValid)
                    {
                        for (j = 0; j < lst.Count; j++)
                        {
                            //array[i - 1, j] = int.Parse(iResult.ToString().Substring(j, 1));
                            array[i - 1, j] = lst[j];

                        }
                    }

                    
                }
            }

            //Swap Row;
            //int i;
            for (i = 0; i <= 30; i++)
            {
                int iFirstRow = R.Next(6);
                int iSecondRow = R.Next(6);
                if (iFirstRow == iSecondRow)
                {
                    continue;
                }
                //int j;

                for (j = 0; j <= 5; j++)
                {
                    arrRowTemp[j] = array[iSecondRow , j];
                    array[iSecondRow, j] = array[iFirstRow, j];
                    array[iFirstRow, j] = arrRowTemp[j];

                }

            }
            StringBuilder strB = new StringBuilder();
            for (i = 0; i < array.GetLength(0); i++)
            {
                for (j = 0; j < array.GetLength(1); j++)
                {
                    strB.Append(array[i, j]).Append(" ");
                }
                strB.Append(Environment.NewLine);
            }
            this.textBox1.Text = strB.ToString();

        }
     

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Board = null;

            int BoardSize = 9;
            Board = new Board(BoardSize);

            String PregenerateBlankBlock = CurrentPath + @"\Pregenerated_Blank_Block\";
            String fileName = @"";
            int i;
            StringBuilder strB = new StringBuilder();
            for (i = 0; i <= 99; i++)
            {
                int iFileName = i;
                fileName = Board.BoardSize + "_" + iFileName.ToString("000") + "Board.bn";
                fileName = PregenerateBlankBlock + Board.BoardSize + @"\" + fileName;

            
                strB.Append(Board.GetListPieceString()).Append(Environment.NewLine);

            }

            this.textBox1.Text = strB.ToString();

        }
    }
}
