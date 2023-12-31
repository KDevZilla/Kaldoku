using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Kaldoku
{
    public partial class FormGenerateBlankBlock : Form
    {
        public FormGenerateBlankBlock()
        {
            InitializeComponent();
        }
        private void GenerateBlankBlocks(Board board)
        {
            /*
             * The algorithm is
             * 1. Generate a random piece
             * 2. Try to put the piece the generate to the board
             *      if we can put, repeate #1
             * 3. If we cannot put we keep doing #1
             *    if number of time we cannot put >  numberOfTryBeforeUnputLastPiece
             *    we unput the last piece
             * 4. if #3 still not good enough we call UnputLastNPiece to unput more than 
             *    one piece
             *      
             */
            GeneratePiece generatePiece = new GeneratePiece(new BasicPieceGenerator());
          
            int maximumAttemptLoopAllow = 50000000;
            int numberOfTryBeforeUnputLastPiece = 40;
            int numberofTimesUnputLastPiece = 0;
            int numberofTimeBeforeUnputLastNPiece = 30;
            int countLoop = 0;
            int countCannotPut = 0;

            int numberofUnputLastNPiece = 3;
            Boolean isSuccess = true;

            while (!board.IsFullWithBlock() && countLoop < maximumAttemptLoopAllow)
            {

                Piece generatedPiece = generatePiece.GenPiece();

                Boolean canPut = board.TryToPut(generatedPiece);
                countLoop++;

                if (canPut)
                {
                    countCannotPut = 0;
                    continue;
                }

                countCannotPut++;
                if (countCannotPut >= numberOfTryBeforeUnputLastPiece)
                {
                    countCannotPut = 0;
                    board.UnputLastPiece();
                    numberofTimesUnputLastPiece++;
                        
                    if (numberofTimesUnputLastPiece >= numberofTimeBeforeUnputLastNPiece)
                    {
                        board.UnputLastNPiece(numberofUnputLastNPiece);
                        numberofTimesUnputLastPiece = 0;
                    }
                }

                if (countLoop >= maximumAttemptLoopAllow)
                {
                     isSuccess = false;
                }
            }

            if (!isSuccess)
            {
                throw new Exception("Sorry I failed to generaete blank blocks, you can try again");
            }

        }
        private void btnGenereateBlankBlock_Click(object sender, EventArgs e)
        {
            int i;
            try
            {
                /* Keep the existing generate block to make sure that the 
                 * output will not duplicate
                */
                HashSet<String> hshGenBlock = new HashSet<string>();

                int boardSize = int.Parse(this.txtBoardSize.Text);
                int numberOfBoardNeedToGenerate = int.Parse(this.txtNumberofBoard.Text);
                StringBuilder strB = new StringBuilder();

                for (i = 0; i < numberOfBoardNeedToGenerate; i++)
                {
                    bool hasGenerate = false;
                    while (!hasGenerate)
                    {
                        var Board = new Board(boardSize);
                        GenerateBlankBlocks(Board);
                        string ListPiece = Board.GetListPieceString();
                        if (hshGenBlock.Contains(ListPiece))
                        {
                            continue;
                        }
                        hshGenBlock.Add(ListPiece);
                        hasGenerate = true;
                        strB.Append(ListPiece).Append(Environment.NewLine);
                    }

                }
                this.txtOutput.Text = strB.ToString();
            } catch (Exception ex)
            {
                this.txtOutput.Text = ex.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(this.txtOutput.Text.Trim() == "")
            {
                MessageBox.Show("There is nothing to copy");
                return;
            }
            Clipboard.SetText(this.txtOutput.Text);
        }

        private void FormGenerateBlankBlock_Load(object sender, EventArgs e)
        {
            this.Icon = Resource1.KaldokuLogo32x32;
        }
    }
}
