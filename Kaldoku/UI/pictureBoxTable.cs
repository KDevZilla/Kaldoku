using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Kaldoku.UI
{
    public class pictureBoxTable : System.Windows.Forms.PictureBox
    {

        public void UnsubscribeAllEvent()
        {

            if (formRefrence != null)
            {
                formRefrence.KeyDown -= FormKeydown;
            }

            this.Paint -= PictureBoxTable_Paint;
            this.MouseDown -= PictureBoxTable_MouseDown;

        }
        public Board Board { get; set; } = null;

        public bool Lock { get; set; } = false;

        public Font TargetNumberFont = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        public Font CellFont = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);


        public Color DefaultCellColor { get; set; } = Color.White;
        public Color BoardBackColor { get; set; } = Color.White; 
        public Color BorderColor { get; set; }
        public Color FontColor { get; set; }
        public Pen PenBorder { get; } = new Pen(Color.Black, 3); 
        public Pen PenLineBetweenCell { get; } = new Pen(Color.FromArgb(49, 81, 90), 0.5f);
        public SolidBrush solidBrushFontvalid { get; } = new SolidBrush(Color.Black);
        
        public SolidBrush solidBrushFontInvalid { get; } = new SolidBrush(Color.Red);
        public SolidBrush solidBrushFontCorrectValue { get; } = new SolidBrush(Color.Blue);
        




        public SolidBrush solidBrushNumberDuplicate { get; } = new SolidBrush(Color.Red);
        public SolidBrush solidBrushNumberPieceIsCorrect { get; } = new SolidBrush(Color.Blue);
        public SolidBrush solidBrushNumberNotComplete { get; } = new SolidBrush(Color.Black);
        public SolidBrush solidBrushNumberPieceIsNotCorrect { get; } = new SolidBrush(Color.Black);
        public SolidBrush solidBrushNumberPieceMatchbutContainDuplicate { get; } = new SolidBrush(Color.Teal);
        public SolidBrush solidBrushCageResult { get; } = new SolidBrush(Color.Blue);
        public SolidBrush solidBrushCellClicked { get; } = new SolidBrush(Color.FromArgb(0, 179, 223));



        private Boolean _Autosize = true;
        public Boolean Autosize
        {
            set
            {
                if (_Autosize != value)
                {
                    if (_Autosize && value)
                    {
                        AdjustSize();
                    }
                }
                _Autosize = value;

            }
            get
            {
                return _Autosize;
            }

        }
        public void AdjustSize()
        {
            this.Height = this.CellWidth * Board.BoardSize + 6;
            this.Width = this.CellWidth * Board.BoardSize + 6;
            this.BackColor = BoardBackColor;
        }
        public pictureBoxTable(Board pBoard, int pCellWidth)
        {
            Board = pBoard;
            CellWidth = pCellWidth;
            InitialValue();


        }
        private void InitialValue()
        {
            this.Paint -= PictureBoxTable_Paint;
            this.MouseDown -= PictureBoxTable_MouseDown;
            this.Paint += PictureBoxTable_Paint;
            this.MouseDown += PictureBoxTable_MouseDown;
            FontColor = Color.Black;

        }
        private Form formRefrence = null;
        public pictureBoxTable(Board pBoard, int pCellWidth, Form f)
        {
            Board = pBoard;
            CellWidth = pCellWidth;
            formRefrence = f;

            formRefrence.Controls.Add(this);
            formRefrence.KeyPreview = true;
            formRefrence.KeyDown -= FormKeydown;
            formRefrence.KeyDown += FormKeydown;
            InitialValue();


        }
        Dictionary<int, int> DicKeyValueMax = new Dictionary<int, int>();
        private int GetKeyValueDMax(int BoardSize)
        {
            if (DicKeyValueMax.Count == 0)
            {
                DicKeyValueMax.Add(4, (int)Keys.D4);
                DicKeyValueMax.Add(5, (int)Keys.D5);
                DicKeyValueMax.Add(6, (int)Keys.D6);
                DicKeyValueMax.Add(7, (int)Keys.D7);
                DicKeyValueMax.Add(8, (int)Keys.D8);
                DicKeyValueMax.Add(9, (int)Keys.D9);
            }
            return DicKeyValueMax[BoardSize];
        }

        Dictionary<int, int> DicKeyValueNumpadMax = new Dictionary<int, int>();
        private int GetKeyValueNumpadMax(int BoardSize)
        {
            if (DicKeyValueNumpadMax.Count == 0)
            {
                DicKeyValueNumpadMax.Add(4, (int)Keys.NumPad4);
                DicKeyValueNumpadMax.Add(5, (int)Keys.NumPad5);
                DicKeyValueNumpadMax.Add(6, (int)Keys.NumPad6);
                DicKeyValueNumpadMax.Add(7, (int)Keys.NumPad7);
                DicKeyValueNumpadMax.Add(8, (int)Keys.NumPad8);
                DicKeyValueNumpadMax.Add(9, (int)Keys.NumPad9);
            }
            return DicKeyValueNumpadMax[BoardSize];
        }
        private void MoveCurrentCell(KeyEventArgs e)
        {
            int newCellClickCol = this.CurrentCellClickCol;
            int newCellClickRow = this.CurrentCellClickRow;

            switch (e.KeyCode)
            {
                case Keys.Left:
                    newCellClickCol -= 1;
                    break;
                case Keys.Right:
                    newCellClickCol += 1;
                    break;
                case Keys.Up:
                    newCellClickRow -= 1;
                    break;
                case Keys.Down:
                    newCellClickRow += 1;
                    break;
            }
            if (newCellClickRow < 0 ||
                newCellClickRow >= Board.BoardSize ||
                newCellClickCol < 0 ||
                newCellClickCol >= Board.BoardSize)
            {
                return;
            }

            this.CurrentCellClickCol = newCellClickCol;
            this.CurrentCellClickRow = newCellClickRow;
            this.ReRender();
            //switch (e.KeyCode)
        }
        private void HandleKeyDown(KeyEventArgs e)
        {
            if (this.Lock)
            {
                return;
            }
            bool isCurrenctCellPostionIsNotValid = this.CurrentCellClickCol == -1 ||
                this.CurrentCellClickRow == -1;

            if (isCurrenctCellPostionIsNotValid)
            {
                return;
            }

            bool isEnterDirectionkey = (e.KeyCode == Keys.Left ||
                e.KeyCode == Keys.Right ||
                e.KeyCode == Keys.Up ||
                e.KeyCode == Keys.Down);

            if (isEnterDirectionkey)
            {
                MoveCurrentCell(e);
                return;
            }

            bool isEnterBackSpaceOrZero = (e.KeyCode == Keys.Back
                || e.KeyCode == Keys.D0
                || e.KeyCode == Keys.NumPad0);

            if (isEnterBackSpaceOrZero)
            {
                EnterCellValue(0);
                return;
            }

            /*
             * Find valid key value from 1 to Boardsize
             * Program also need to support numeric keypad on the keyboard
             * If the value is valid, call EnterCellValue
             */

            //Key from keyboard
            int keyValueDMin = (int)Keys.D1;
            int keyValueDMax = (int)Keys.D9;

            keyValueDMax = GetKeyValueDMax(this.Board.BoardSize);


            //Key from Numpad
            int keyValueNumMin = (int)Keys.NumPad1;
            int keyValueNumMax = (int)Keys.NumPad9;
            keyValueNumMax = GetKeyValueNumpadMax(this.Board.BoardSize);

            Boolean isValidDKey = true;
            Boolean isValidNumKey = true;
            int keyCode = (int)e.KeyCode;
            if (keyCode < keyValueDMin || keyCode > keyValueDMax)
            {
                isValidDKey = false;
            }

            if (keyCode < keyValueNumMin || keyCode > keyValueNumMax)
            {
                isValidNumKey = false;
            }

            if ((!isValidDKey && !isValidNumKey))
            {
                return;
            }

            int keyEnter = 0;
            if (isValidDKey)
            {
                keyEnter = keyCode - keyValueDMin + 1;
            }
            else
            {
                keyEnter = keyCode - keyValueNumMin + 1;
            }

            EnterCellValue(keyEnter);

        }
        private void FormKeydown(object sender, KeyEventArgs e)
        {
            HandleKeyDown(e);
        }

        public void EnterCellValue(int pValue)
        {

            Board.Entervalue(this.CurrentCellClickRow, this.CurrentCellClickCol, pValue);
            Board.CalculateCellStatus();
            this.ReRender();
            if (!Board.IsCorrectAnswer())
            {
                return;
            }

            EnterCorrectValueHandler?.Invoke(this, new EventArgs());

        }
        public void EnterCellValue(int pValue, int RowCell, int ColCell)
        {

            Board.Entervalue(RowCell, ColCell, pValue);
            this.ReRender();


        }
        public event CellClickEventHandler CellClickHandler;
        private void PictureBoxTable_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.Lock)
            {
                return;
            }

            int rowSelect = 0;
            int colSelect = 0;

            rowSelect = e.Y / CellWidth;
            colSelect = e.X / CellWidth;


            CellClickEventArgs eventArgs = new CellClickEventArgs();
            eventArgs.RowClick = rowSelect;
            eventArgs.ColClick = colSelect;
            CurrentCellClickRow = rowSelect;
            CurrentCellClickCol = colSelect;
            this.ReRender();
            this.Invalidate();

            ContextMenu contextMenu = new ContextMenu();
            int i;
            for (i = 1; i <= this.Board.BoardSize; i++)
            {
                contextMenu.MenuItems.Add(i - 1, new MenuItem(i.ToString()));
                contextMenu.MenuItems[i - 1].Click -= MenuItem_Click;
                contextMenu.MenuItems[i - 1].Click += MenuItem_Click;
            }


            switch (e.Button)
            {
                case MouseButtons.Right:
                    {

                        contextMenu.Show(this, new Point(e.X, e.Y));
                    }
                    break;
            }
            if (CellClickHandler != null)
            {
                CellClicked = this.Board.GetCell(rowSelect, colSelect);
                CellClickHandler(this, eventArgs);
            }
        }

        private void MenuItem_Click(object sender, EventArgs e)
        {
            MenuItem item = (MenuItem)sender;
            String value = item.Text;
            int keyValue = 96 + int.Parse(value);
            Keys key = (Keys)Enum.Parse(typeof(Keys), keyValue.ToString());

            var keyEvent = new KeyEventArgs(key);
            HandleKeyDown(keyEvent);

        }

        public Cell CellClicked = null;

        private void PictureBoxTable_Paint(object sender, PaintEventArgs e)
        {
            PictureBox Pic = (PictureBox)sender;

            if (Board == null)
            {
                return;
            }
            RenderBoard(e, Board);
        }

        public int CellWidth { get; private set; } = 60;

        public int CurrentCellClickRow { get; private set; } = 0;
        public int CurrentCellClickCol { get; private set; } = 0;
        private Boolean IsRenderBlockNumber = false;

        int yOffset = 3;
        int xOffset = 3;
        private void RenderBorder(Graphics g, Board pBoard)
        {
            int indexPiece = 0;

            for (indexPiece = 0; indexPiece < pBoard.lstPiece.Count; indexPiece++)
            {
                Kaldoku.Piece Piece = pBoard.lstPiece[indexPiece];
                int indexCell;
                for (indexCell = 0; indexCell < Piece.lstCell.Count; indexCell++)
                {
                    Kaldoku.Cell Cell = Piece.lstCell[indexCell];

                    int indexOtherCell = 0;

                    Point p1 = new Point(0, 0);
                    Point p2 = new Point(0, 0);
                    int col = Piece.ColPut + Cell.Col;
                    int row = Piece.RowPut + Cell.Row;
                    int extrapixel = 2;
                    Point northBegin = new Point(col * CellWidth + xOffset - extrapixel, row * CellWidth + yOffset);
                    Point northEnd = new Point(northBegin.X + CellWidth + extrapixel, northBegin.Y);

                    Point westBegin = new Point(col * CellWidth + xOffset, row * CellWidth + yOffset);
                    Point westEnd = new Point(westBegin.X, westBegin.Y + CellWidth + 1);


                    Point eastBegin = new Point((col + 1) * CellWidth + xOffset, row * CellWidth + yOffset);
                    Point eastEnd = new Point(eastBegin.X, eastBegin.Y + CellWidth + 1);

                    Point southBegin = new Point(col * CellWidth + xOffset - extrapixel, (row + 1) * CellWidth + yOffset);
                    Point southEnd = new Point(southBegin.X + CellWidth + extrapixel, southBegin.Y);



                    Boolean isNeedToDrawNorthBorder = true;
                    Boolean isNeedToDrawWestBorder = true;
                    Boolean isNeedToDrawSouthBorder = true;
                    Boolean isNeedToDrawEastBorder = true;

                    PointF pointF = new PointF(col * CellWidth, row * CellWidth);


                    pointF = new PointF(col * CellWidth + (CellWidth / 4) + 3, row * CellWidth + (CellWidth / 4));

                    for (indexOtherCell = 0; indexOtherCell < Piece.lstCell.Count; indexOtherCell++)
                    {
                        if (indexOtherCell == indexCell)
                        {
                            continue;
                        }
                        if (Piece.lstCell[indexOtherCell].Row == Piece.lstCell[indexCell].Row)
                        {
                            if (Piece.lstCell[indexOtherCell].Col > Piece.lstCell[indexCell].Col)
                            {
                                isNeedToDrawEastBorder = false;
                            }
                            else
                            {
                                isNeedToDrawWestBorder = false;
                            }
                        }
                        if (Piece.lstCell[indexOtherCell].Col == Piece.lstCell[indexCell].Col)
                        {
                            if (Piece.lstCell[indexOtherCell].Row > Piece.lstCell[indexCell].Row)
                            {
                                isNeedToDrawSouthBorder = false;
                            }
                            else
                            {
                                isNeedToDrawNorthBorder = false;
                            }
                        }
                    }

                    Pen pen = isNeedToDrawNorthBorder
                        ? PenBorder
                        : PenLineBetweenCell;
                    g.DrawLine(pen, northBegin, northEnd);


                    pen = isNeedToDrawWestBorder
                        ? PenBorder
                        : PenLineBetweenCell;
                    g.DrawLine(pen, westBegin, westEnd);

                    pen = isNeedToDrawSouthBorder
                        ? PenBorder
                        : PenLineBetweenCell;
                    g.DrawLine(pen, southBegin, southEnd);


                    pen = isNeedToDrawEastBorder
                        ? PenBorder
                        : PenLineBetweenCell;
                    g.DrawLine(pen, eastBegin, eastEnd);
                }
            }
        }
        private void RenderNumber(Graphics g, Board pBoard)
        {
            int i = 0;
            for (i = 0; i < pBoard.lstPiece.Count; i++)
            {
                Kaldoku.Piece Piece = pBoard.lstPiece[i];
                int j;
                for (j = 0; j < Piece.lstCell.Count; j++)
                {
                    Kaldoku.Cell Cell = Piece.lstCell[j];

                    Point p1 = new Point(0, 0);
                    Point p2 = new Point(0, 0);
                    int col = Piece.ColPut + Cell.Col;
                    int row = Piece.RowPut + Cell.Row;

                    Boolean isFirstCellinPiece = false;
                    isFirstCellinPiece = (j == 0);
                    PointF pointF = new PointF(col * CellWidth, row * CellWidth);


                    SolidBrush solidBrushCageResult = solidBrushFontvalid;



                    if (isFirstCellinPiece)
                    {

                        pointF = new PointF(col * CellWidth + 1, row * CellWidth + 1);

                        //Draw text using DrawString
                        String FirstBoxinShapeText = "";
                        if (IsRenderBlockNumber)
                        {
                            FirstBoxinShapeText = i.ToString();
                        }
                        else
                        {
                            FirstBoxinShapeText = Piece.TargetNumber + Piece.OperationString;
                        }
                        if (Board.IsShowCellTargetValue)
                        {
                            solidBrushCageResult = solidBrushFontCorrectValue;

                        }
                        else
                        {
                            if (Piece.IsAnswerMatch)
                            {
                                solidBrushCageResult = solidBrushFontCorrectValue;
                            }
                            else if (!Piece.IsThereEmptyCell)
                            {
                                solidBrushCageResult = solidBrushFontInvalid; // new SolidBrush(FontColorInvalid);
                            }
                        }

                        g.CompositingMode = CompositingMode.SourceOver;
                        g.DrawString(FirstBoxinShapeText, TargetNumberFont,
                            solidBrushCageResult, pointF);
                        g.CompositingMode = CompositingMode.SourceCopy;
                    }

                    pointF = new PointF(col * CellWidth + (CellWidth / 4) + 3, row * CellWidth + (CellWidth / 4));

                    String cellText = "";
                    cellText = Cell.Value.ToString();
                    if (cellText.Trim() == "0")
                    {
                        cellText = "";
                    }

                    SolidBrush solidBrushNumber = solidBrushFontvalid;


                    if (Board.IsShowCellTargetValue)
                    {
                        cellText = Cell.TargetValue.ToString();
                        solidBrushNumber = solidBrushFontCorrectValue;
                    }
                    else
                    {
                        switch (Cell.CellStatus)
                        {
                            case Cell.enCellStatus.Blank:
                                break;
                            case Cell.enCellStatus.Duplicate:
                                solidBrushNumber = solidBrushNumberDuplicate;
                                break;
                            case Cell.enCellStatus.PieceIsCorrect:
                                solidBrushNumber = solidBrushNumberPieceIsCorrect;
                                break;
                            case Cell.enCellStatus.PieceIsNotComplete:
                                solidBrushNumber = solidBrushNumberNotComplete;
                                break;
                            case Cell.enCellStatus.PieceIsNotCorrect:
                                solidBrushNumber = solidBrushNumberPieceIsNotCorrect;
                                break;
                            case Cell.enCellStatus.PieceMatchbutContainDuplicate:
                                solidBrushNumber = solidBrushNumberPieceMatchbutContainDuplicate;
                                break;
                            default:
                                throw new Exception($"Please don't forget to handle case of {Cell.CellStatus}");
                        }
                    }

                    g.CompositingMode = CompositingMode.SourceOver;
                    g.DrawString(cellText, CellFont, solidBrushNumber, pointF);
                    g.CompositingMode = CompositingMode.SourceCopy;

                }

            }
        }
        private void RenderBoard(PaintEventArgs e, Board pBoard)
        {

            Color PenColor = Color.Black;
            e.Graphics.CompositingMode = CompositingMode.SourceCopy;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;


            int i;
            //If we show target value it mean the game is end
            //so we no need to paint current cell background
            if (CurrentCellClickRow != -1
                && CurrentCellClickCol != -1
                && !Board.IsShowCellTargetValue)
            {

                e.Graphics.FillRectangle(solidBrushCellClicked,
                    CurrentCellClickCol * CellWidth + xOffset,
                    CurrentCellClickRow * CellWidth + yOffset,
                    CellWidth,
                    CellWidth);

            }
            RenderBorder(e.Graphics, pBoard);
            RenderNumber(e.Graphics, pBoard);



        }
        public void ReRender()
        {
            this.Update();
            this.Invalidate();
        }
        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            PictureBoxTable_Paint(sender, e);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        public delegate void CellClickEventHandler(object sender, CellClickEventArgs e);
        public class CellClickEventArgs
        {
            public int RowClick { get; set; }
            public int ColClick { get; set; }
        }
        public delegate void EnterCorrectValueForAll(object sender, EventArgs e);
        public event EnterCorrectValueForAll EnterCorrectValueHandler = null;
        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            int RowSelect = 0;
            int ColSelect = 0;
            //  int CellWidth = 50;
            RowSelect = e.Y / CellWidth;
            ColSelect = e.X / CellWidth;
            // this.Text = "RowSelect::" + RowSelect.ToString() + "  ColSelect::" + ColSelect.ToString();

        }


    }


}
