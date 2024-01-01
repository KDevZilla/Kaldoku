using Kaldoku.UI;
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
    public partial class FormKaldoku : Form
    {
        public FormKaldoku()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show ("Are you sure you would like to exit ?","", MessageBoxButtons.OKCancel )!= DialogResult.OK)
            {
                return;
            }
            Application.Exit();
        }
        private Boolean IsCurrentGameStillPlay = false;
        Board Board = null;
        pictureBoxTable pictable = null;

        private DateTime _TimeBegin;
        private DateTime _TimeEnd;

        private void NewGame(int BoardSize)
        {
            if (!Kaldoku.Baseclass.App.IsDebugMode)
            {
                if (IsCurrentGameStillPlay)
                {
                    if (DialogResult.OK != MessageBox.Show("Do you want to end the current game ?", "", MessageBoxButtons.OKCancel))
                    {
                        return;
                    }
                }
            }
            _TimeBegin = DateTime.Now;

            UnloadPictable();
            pictable = null;
            Board = null;


            
            int[,] targetCellNumber = PregenratedNumber.getTargetCellNumber(BoardSize);
            String listPieceString=  PregenratedBlankBlock.GetListStringByRandom(BoardSize);
            Boolean isAcceptNegativeTargetNumber = false;
            // listPieceString = @"O_Rotate90_0_0|L3_Rotate270_0_4|I2_Rotate360_2_0|L2_Rotate90_2_1|I2_Rotate90_0_2|O_Rotate270_2_3|I2_Rotate90_4_0|J3_Rotate90_3_2|";

            // listPieceString = @"J2_Rotate90_0_0|Z_Rotate360_0_1|L2_Rotate90_2_0|Z_Rotate360_2_2|S_Rotate360_3_1|Dot_Rotate270_0_3|I3_Rotate360_0_4|Dot_Rotate270_4_2|I2_Rotate90_4_3|";
            //listPieceString = @"I2_Rotate360_0_0|S_Rotate360_0_3|T_Rotate270_1_1|L3_Rotate180_2_2|I2_Rotate360_1_4|I2_Rotate360_3_0|J2_Rotate90_3_1|I2_Rotate90_0_1|I2_Rotate360_3_4|";

            Board = Board.Create(BoardSize, listPieceString, targetCellNumber, isAcceptNegativeTargetNumber);
            //this.Text = Board.IsFullWithBlock().ToString ();
            //Testing value
            // listPieceString = @"I2_Rotate360_0_0|L3_Rotate180_0_1|I4_Rotate90_3_0|I3_Rotate360_0_3|L2_Rotate270_1_1|";
            /*
            int z = 0;
            for (z = 1; z <= 100000; z++)
            {
                int[,] targetNumber = PregenratedNumber.getTargetCellNumber(BoardSize);
                Board = Board.Create(BoardSize, listPieceString, targetNumber);
                if(Board.lstPiece [0].TargetNumber !=-1)
                {
                    continue;
                }
                
                if(Board.lstPiece [1].TargetNumber != 10)
                {
                    continue;
                }
                if (Board.lstPiece[2].TargetNumber != 10)
                {
                    continue;
                }
                if (Board.lstPiece [3].TargetNumber != 9)
                {
                    continue;
                }
                if (Board.lstPiece[4].TargetNumber != -6)
                {
                    continue;
                }
                StringBuilder strB = new StringBuilder();
                strB.Append("targetNumber = new int[,]");
                strB.Append("{").Append (Environment.NewLine);


                for(int row=0;row<targetNumber.GetLength(0); row++)
                {
                    strB.Append("{");
                    for(int col=0;col <targetNumber.GetLength(1); col++)
                    {
                        strB.Append(targetNumber[row, col]).Append (",");
                    }
                    strB.Append("},").Append (Environment.NewLine);
                }
                strB.Append("};").Append(Environment.NewLine);
                break;
            }
            */

            // Board.IsAcceptNegativeTargetNumber = false;

            if (pictable !=null)
            {
               pictable.EnterCorrectValueHandler -= Pictable_EnterCorrectValueHandler;
            }
            pictable = new pictureBoxTable(Board, 70, this);
            pictable.EnterCorrectValueHandler -= Pictable_EnterCorrectValueHandler;
            pictable.EnterCorrectValueHandler += Pictable_EnterCorrectValueHandler;
          
            LoadBoardToPictable(Board, pictable);

            AdjustFormSize();
            IsCurrentGameStillPlay = true;
            this.BackColor = pictable.BackColor;

        }

        private void FinishedGame()
        {
   
            this.IsCurrentGameStillPlay = false;
            this.pictable.Lock = true;
            this.pictable.ReRender();
            this.IsCurrentGameStillPlay = false;

        }
        private void Pictable_EnterCorrectValueHandler(object sender, EventArgs e)
        {
           // throw new NotImplementedException();
           if(!this.IsCurrentGameStillPlay )
            {
                return;
            }
            _TimeEnd = DateTime.Now;
            TimeSpan T=  _TimeEnd - _TimeBegin ;
            String Wording = "";
            Wording = "Congreatulations, you solved this puzzle. It took you " + T.Minutes.ToString ("00") + ":" + T.Seconds.ToString ("00") + " to finish.";
            MessageBox.Show(Wording);
            FinishedGame();

        }

        private void AdjustFormSize()
        {
           
            this.Height = pictable.Height + (pictable.Top * 2) +10;
            this.Width = pictable.Width + (pictable.Left * 2) + 6;
        }
        private void UnloadPictable()
        {
            if (this.Controls.Contains(pictable))
            {
                pictable.UnsubscribeAllEvent();
                this.Controls.Remove(pictable);
            }
        }
        private void LoadBoardToPictable(Board pBoard,pictureBoxTable pPictable)
        {

            this.Controls.Add(pPictable);

            pPictable.Top = 30;
            pPictable.Left = 10;
            pPictable.AdjustSize();
            pPictable.Visible = true;
            pPictable.Invalidate();
            pPictable.ReRender();
        }


        private void CheckRequiredFileExists()
        {
            StringBuilder strBuilderWarningMessage = new StringBuilder();
            if(!FileUtil.IsFolderExist(FileUtil.PregeneratedDigitsPath))
            {
                strBuilderWarningMessage.Append($"Please copy {FileUtil.PregeneratedDigitsPath} folder to {FileUtil.CurrentPath}")
                    .Append(Environment.NewLine);
                
            }
            if (!FileUtil.IsFolderExist(FileUtil.PregeneratedBlankBlock))
            {
                strBuilderWarningMessage.Append($"Please copy {FileUtil.PregeneratedBlankBlock} folder to {FileUtil.CurrentPath}")
                    .Append(Environment.NewLine);

            }
            if(strBuilderWarningMessage.ToString ().Trim() == "")
            {
                return;
            }

            MessageBox.Show(strBuilderWarningMessage.ToString());

        }
        private void frmKaldoku_Load(object sender, EventArgs e)
        {
            this.Icon = Resource1.KaldokuLogo32x32;

            //Uncomment this below line and you will see show answer menu
            //You can use it for debugging purpose
            //Kaldoku.Baseclass.App.IsDebugMode = true;


            showAnswerToolStripMenuItem.Visible = Kaldoku.Baseclass.App.IsDebugMode;
            CheckRequiredFileExists();

            
            List<ToolStripMenuItem> listMenuNew = new List<ToolStripMenuItem>()
            {
                this.toolStripMenuNew4 ,
                this.toolStripMenuNew5 ,
                this.toolStripMenuNew6 ,
                this.toolStripMenuNew7 ,
                this.toolStripMenuNew8 ,
                this.toolStripMenuNew9
            };
            int i;
            //Assign click Event for each new Menu to create new game
            for (i = 0; i < listMenuNew.Count; i++)
            {
                int boardSizeStoredInTag = i + 4;
                listMenuNew[i].Tag = boardSizeStoredInTag; 
                listMenuNew[i].Click += (o, ev) =>
                {
                    int boardSize = int.Parse(((ToolStripMenuItem)o).Tag.ToString ());
                    NewGame(boardSize);
                };
            }

        }


        private void giveupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Board == null)
            {
                MessageBox.Show("Cannot give up becasue you haven't choose to play yet");
                return;
            }


            // When you debug program sometime you would like to ignore message box
            if (!Kaldoku.Baseclass.App.IsDebugMode)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to give up ?", "", MessageBoxButtons.OKCancel);
                if (result != DialogResult.OK)
                {
                    return;
                }
            }


            this.Board.IsShowCellTargetValue = true;
            FinishedGame();
        }

        private void showAnswerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Board.CopyTargetNumberToNumberForDebugPurpose();
            this.pictable.ReRender();

        }

        private void saveBoardToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuNew4_Click(object sender, EventArgs e)
        {

        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //  Clipboard.SetText(Board.GetListPieceString());
            NewGame(9);
          //  this.Board.IsShowCellTargetValue = true;
           // FinishedGame();
        }

        private void test2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Board.GetListPieceString());
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormAbout f = new FormAbout();
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
        }
    }
}
