using System;

using System.Drawing;
using System.IO;

using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

using System.Runtime.Serialization;
using System.Collections.Generic;
using Kaldoku.UI;

namespace Kaldoku
{
    public partial class formTesting : Form
    {
        public formTesting()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           


        }

        private string CurrentPath
        {
            get
            {
                return System.AppDomain.CurrentDomain.BaseDirectory;
            }
        }
        pictureBoxTable pictable = null;
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Icon = Resource1.KaldokuLogo32x32;
            this.btn1.Click += BtnNumber_Click;
            this.btn2.Click += BtnNumber_Click;
            this.btn3.Click += BtnNumber_Click;
            this.btn4.Click += BtnNumber_Click;
            this.btn5.Click += BtnNumber_Click;
            this.btn6.Click += BtnNumber_Click;

        }

        private void BtnNumber_Click(object sender, EventArgs e)
        {
            //  throw new NotImplementedException();
            String Value = "";
            Value = ((Button)sender).Text;
            this.pictable.EnterCellValue(int.Parse (Value));

        }

        Board Board = null;
        private void GenManyBLock()
        {
            int i;
            List<Board> lst = new List<Board>();
            for(i=1;i<=300;i++)
            {
                Board cb = new Board();
                cb = GenBlankBlock();
                lst.Add(cb);
            }
        }
        private Board GenBlankBlock()
        {
            GeneratePiece cGen = new GeneratePiece(new BasicPieceGenerator());
            //cGeneratePiece cGen = new cGeneratePiece(new SimplePieceGenerator());

            int iMaximumAttempt = 50000000;
            int iNoofTryBeforeStepBack = 40;
            int iCount = 0;
            int iCountCannotPut = 0;
            int iNoofTimeNeedToStepBack = 0;
            Boolean IsSuccess = true;

            Piece.PieceType[] arrPieceTest ={ Piece.PieceType.I4,
                                                Piece.PieceType.L2 ,
                                                Piece.PieceType.L3};

            //int iCountArr = 0;
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
                   // this.textBox1.Text = "There must be something wrong.";
                    IsSuccess = false;
                }
            }

            if (IsSuccess)
            {
               // this.textBox1.Text = "Success" + Environment.NewLine + iCount.ToString() + Environment.NewLine + Board.GetBoardPosition();
            }
            else
            {
                // this.textBox1.Text = "Failed" + Environment.NewLine +Board.GetBoardPosition();
                throw new Exception("Sorry I failed to generaete blank block");

            }
            return Board;

        }

        public void SerializeNow(Board pBoard,String fileName)
        {
            // ClassToSerialize c = new ClassToSerialize();
           // File f0 = new File("");
/*
            File f = new File("temp.dat");
            Stream s = f.Open(FileMode.Create);
            BinaryFormatter b = new BinaryFormatter();
            */

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(fileName , FileMode.Create, FileAccess.Write);

            formatter.Serialize(stream, pBoard);
            stream.Close();

            
        }
        public void DeSerializeNow(String fileName,ref Board pBoard)
        {

             Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            IFormatter formatter = new BinaryFormatter();
            pBoard = (Board)formatter.Deserialize(stream);

        }
        private void UnloadPictable()
        {
            if (this.Controls.Contains(pictable))
            {
                this.Controls.Remove(pictable);
            }
        }
        private void LoadBoardToPictable(Board pBoard)
        {
            pictable = new pictureBoxTable(pBoard, 90, this);
            //  this.Controls.Add(pictable);

            pictable.Top = 50;
            pictable.Left = 50;
            pictable.Height = pictable.CellWidth  * pBoard.BoardSize;
            pictable.Width = pictable.CellWidth  * pBoard.BoardSize;
            pictable.Visible = true;
            pictable.Invalidate();
            pictable.CellClickHandler += Pictable_CellClickHandler;
            pictable.ReRender();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
         

        }

        private void Pictable_CellClickHandler(object sender, pictureBoxTable.CellClickEventArgs e)
        {
          
          //  throw new NotImplementedException();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //  this.pictureBox1.Refresh();
            pictable.ReRender();

        }
        int CellWidth = 60;
        private void DrawBorder(PaintEventArgs e,Board pBoard)
        {
            float PenWitdhForBorder = 4;
            float PenWidthBetweenCell = 1;
            int Offset = ((int)PenWitdhForBorder) / 2;
            Color PenColor = Color.Black;


            int i;
            for(i=0;i<pBoard.lstPiece.Count;i++)
            {
                Kaldoku.Piece Piece = pBoard.lstPiece[i];
                int j;
                for(j=0;j<Piece.lstCell.Count;j++)
                {
                    Kaldoku.Cell Cell = Piece.lstCell[j];
                    
                    Pen pen = new Pen(PenColor, PenWitdhForBorder);

                    Point p1 = new Point(0, 0);
                    Point p2 = new Point(0, 0);
                    int Col = Piece.ColPut + Cell.Col;
                    int Row = Piece.RowPut + Cell.Row;
                    Point NorthBegin = new Point( Col * CellWidth, Row * CellWidth);
                    Point NorthEnd = new Point(NorthBegin.X + CellWidth,NorthBegin.Y );

                    Point WestBegin = new Point(Col * CellWidth, Row * CellWidth);
                    Point WestEnd = new  Point(WestBegin.X , WestBegin.Y + CellWidth );


                    Point EastBegin = new Point((Col + 1) * CellWidth, Row* CellWidth);
                    Point EastEnd = new Point(EastBegin.X , (Row + 1) * CellWidth );

                    Point SouthBegin = new Point(Col * CellWidth, (Row + 1) * CellWidth);
                    Point SouthEnd = new Point((Col + 1) * CellWidth, SouthBegin.Y);



                    Boolean IsNorthBorder = true;
                    Boolean IsWestBorder = true;
                    Boolean IsSouthBorder = true;
                    Boolean IsEastBorder = true;


                    int k;
                    for (k = 0; k < Piece.lstCell.Count; k++)
                    {
                        if(k==j)
                        {
                            continue;
                        }
                        if(Piece.lstCell [k].Row == Piece.lstCell [j].Row )
                        {
                            if(Piece.lstCell [k].Col > Piece.lstCell [j].Col )
                            {
                                IsEastBorder = false;
                            } else
                            {
                                IsWestBorder = false;
                            }
                        }
                        if (Piece.lstCell[k].Col == Piece.lstCell[j].Col)
                        {
                            if (Piece.lstCell[k].Row  > Piece.lstCell[j].Row )
                            {
                                IsSouthBorder = false;
                            }
                            else
                            {
                                IsNorthBorder = false;
                            }
                        }
                    }
                    pen=new Pen (PenColor   , PenWitdhForBorder);
                    if(!IsNorthBorder )
                    {
                        pen=new Pen (PenColor , PenWidthBetweenCell);
                    } else
                    {
                      //  NorthBegin.Y -= Offset;
                       // NorthBegin.X -= Offset;
                    }
                    e.Graphics.DrawLine(pen,NorthBegin,NorthEnd);

                    pen = new Pen(PenColor  , PenWitdhForBorder);
                    if (!IsWestBorder )
                    {
                        pen = new Pen(PenColor, PenWidthBetweenCell);
                    }
                    e.Graphics.DrawLine(pen, WestBegin, WestEnd);

                    pen = new Pen(PenColor  , PenWitdhForBorder);
                    if (!IsSouthBorder )
                    {
                        pen = new Pen(PenColor, PenWidthBetweenCell);
                    }
                    e.Graphics.DrawLine(pen, SouthBegin, SouthEnd);

                    pen = new Pen(PenColor  , PenWitdhForBorder);
                    if (!IsEastBorder )
                    {
                        pen = new Pen(PenColor, PenWidthBetweenCell);
                    }
                    e.Graphics.DrawLine(pen, EastBegin, EastEnd);

                  



                }

            }
           

        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            PictureBox Pic = (PictureBox)sender;
            Board Board = (Board)Pic.Tag;
            if(Board==null)
            {
                return;
            }
            DrawBorder(e,Board);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            int RowSelect = 0;
            int ColSelect = 0;
          //  int CellWidth = 50;
            RowSelect = e.Y / CellWidth;
            ColSelect = e.X / CellWidth;
            this.Text = "RowSelect::" + RowSelect.ToString() + "  ColSelect::" + ColSelect.ToString();

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int RowSelect = 0;
            int ColSelect = 0;
            int CellWidth = 50;
            RowSelect = e.X / CellWidth;
            ColSelect = e.Y / CellWidth;
            this.label1.Text = "X::" + e.X.ToString() + "  Y::" + e.Y.ToString();

           // this.Text = "RowSelect::" + RowSelect.ToString() + "  ColSelect::" + ColSelect.ToString();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            this.Text = "Keydown";

        }

        private void btn1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SerializeNow(Board,"");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Board = new Board();

          //  DeSerializeNow(ref Board);
            UnloadPictable();
            LoadBoardToPictable(Board);

         
        }

        public bool IsPalindrome(string s)
        {
          //  s = s.Replace(",", "").Replace(" ", "").Replace(":").ToLower();
            int i;
            string sReverse = "";
            String sAlphaNumeric = "";

            StringBuilder strB = new StringBuilder();
            for(i=0;i<s.Length;i++)
            {
                char c = s.Substring(i, 1)[0];

                if(!char.IsLetterOrDigit (c))
                {
                    continue;
                }
              
                strB.Append(s.Substring(i, 1).ToLower());
            }
            sAlphaNumeric = strB.ToString();
            strB = new StringBuilder();

            int HalfLength = sAlphaNumeric.Length / 2;

            for (i = sAlphaNumeric.Length  - 1; i >= HalfLength ; i--)
            {

                strB.Append(sAlphaNumeric.Substring(i, 1));

            }
            sReverse = strB.ToString();
            if (sReverse.Equals(sAlphaNumeric.Substring (0, sReverse.Length )))
            {
                return true;
            }
            return false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // String s = "aa";
            //  String s = "A man, a plan, a canal: Panama";
            String s = "AB";
            this.Text = IsPalindrome(s).ToString ();

        }

        private void button6_Click(object sender, EventArgs e)
        {



        }

        private void button7_Click(object sender, EventArgs e)
        {
          
        }

        private void button8_Click(object sender, EventArgs e)
        {


        }

       

        private void button10_Click(object sender, EventArgs e)
        {
            Board.IsShowCellTargetValue = true;
            pictable.ReRender(); 


        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Board.CopyTargetNumberToNumberForDebugPurpose();
            pictable.ReRender();


        }

        private void btn4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            this.textBox1.Text = Board.IsCorrectAnswer().ToString();

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            this.textBox1.Text = System.AppDomain.CurrentDomain.BaseDirectory;

        }
    }
    
}
