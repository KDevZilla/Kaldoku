using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace Kaldoku
{

    public class Board
    {


        Cell[,] CellTable = new Cell[6, 6];

        private const int BlankCellValue = -1;

        public int BoardSize { get; private set; } = 6;
        public string GetListPieceString()
        {

            StringBuilder strB = new StringBuilder();
            lstPiece.ForEach(x => strB.Append(x.keyAndPosition).Append("|"));
            return strB.ToString();
        }
        public Board() => this.ClearBoard();

        public Board(int pBoardSize)
        {
            this.BoardSize = pBoardSize;
            this.ClearBoard();
        }
        public static Board Create(int boardSize, String piecesString, int[,] tarGetCellValue, Boolean IsAcceptNegativeTargetNumber)
        {
            Board newBoard = new Board(boardSize);
            /*
             * Example of Parameter value
             * boardSize:4
             * pieceString:J3_Rotate270_0_0|J2_Rotate180_1_0|I2_Rotate360_0_3|J3_Rotate90_2_0|L2_Rotate180_2_2|
             * int[,] targetNumber = new int[,]
            {
                { 1,2,3,4 },
                { 2,3,4,1},
                { 3,4,1,2},
                { 4,1,2,3}
            };
             */

            string[] arrPieces = piecesString.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            int i;

            /*loop though arrPieces to create Piece object to put into a newBoard
             * Example J3_Rotate270_0_1 means
             * J3 piece type
             * Rotate at 270
             * Row in a board 0
             * Column in a board 1
            */
            for (i = 0; i < arrPieces.Length; i++)
            {

                string[] arrPieceType = arrPieces[i].Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                string pieceTypeString = arrPieceType[0] + "_" + arrPieceType[1];
                int rowPut = int.Parse(arrPieceType[2]);
                int colPut = int.Parse(arrPieceType[3]);
                Piece piece = Piece.Create(pieceTypeString);
                newBoard.PutPiece(rowPut, colPut, piece);
            }

            newBoard.SetTargetNumber(tarGetCellValue);
            newBoard.AssignOperationToPiece(IsAcceptNegativeTargetNumber);
            newBoard.CalTargetNumberForAllPieces();
            return newBoard;
        }


        public List<Point> Dir = new List<Point>()
        {
             new Point(-1, 0) ,
             new Point(0, 1) ,
             new Point(1, 0) ,
             new Point(0, -1) ,
        };

        public Boolean HasIsolateCell()
        {
            return HasIsolateCell(this.CellTable);
        }
        private Boolean _IsShowCellTargetValue = false;
        public Boolean IsShowCellTargetValue
        {
            get { return _IsShowCellTargetValue; }
            set { _IsShowCellTargetValue = value; }
        }
        public Boolean IsThisCellValueDupliate(int iRow, int iCol)
        {
            int i;

            if (CellTable[iRow, iCol].Value == 0)
            {
                return false;
            }

            for (i = 0; i < CellTable.GetLength(0); i++)
            {
                if (i == iRow)
                {
                    continue;
                }
                if (CellTable[i, iCol].Value == CellTable[iRow, iCol].Value)
                {
                    return true;
                }

            }

            for (i = 0; i < CellTable.GetLength(0); i++)
            {
                if (i == iCol)
                {
                    continue;
                }
                if (CellTable[iRow, i].Value == CellTable[iRow, iCol].Value)
                {
                    return true;
                }

            }
            return false;
        }
        public Boolean IsValidNumberInBoard()
        {
            int i;
            int j;
            int iMax = CellTable.GetLength(0);
            for (i = 0; i < CellTable.GetLength(0); i++)
            {
                for (j = 0; j < CellTable.GetLength(1); j++)
                {
                    if (CellTable[i, j].Value == BlankCellValue)
                    {
                        return false;
                    }

                }
            }
            for (i = 0; i < CellTable.GetLength(0); i++)
            {
                if (!IsValidNumberInRow(i))
                {
                    return false;
                }

                if (!IsValidNumberInCol(i))
                {
                    return false;
                }
            }


            return true;
        }
        public Boolean IsValidNumberInRow(int iRow)
        {

            HashSet<int> hshRow = new HashSet<int>();

            int j = 0;

            for (j = 0; j < CellTable.GetLength(1); j++)
            {
                if (CellTable[iRow, j].Value == 0)
                {
                    continue;
                }
                if (hshRow.Contains(CellTable[iRow, j].Value))
                {
                    return false;
                }
                else
                {
                    hshRow.Add(CellTable[iRow, j].Value);
                }
            }

            return true;
        }

        public Boolean IsValidNumberInCol(int iCol)
        {
            //List<HashSet<int>> lstRow = new List<HashSet<int>>();
            HashSet<int> hshCol = new HashSet<int>();
            int i = 0;
            int j = 0;



            for (j = 0; j < CellTable.GetLength(0); j++)
            {
                if (CellTable[j, iCol].Value == 0)
                {
                    continue;
                }

                if (hshCol.Contains(CellTable[j, iCol].Value))
                {
                    return false;
                }
                else
                {
                    hshCol.Add(CellTable[j, iCol].Value);
                }
            }

            return true;
        }
        public Boolean HasIsolateCell(Cell[,] cellTable)
        {
            int i;
            int j;
            int k;
            StringBuilder strB = new StringBuilder();
            List<Cell> lstCell = new List<Cell>();

            for (i = 0; i < cellTable.GetLength(0); i++)
            {
                for (j = 0; j < cellTable.GetLength(1); j++)
                {

                    if (cellTable[i, j].Value != BlankCellValue)
                    {
                        continue;
                    }

                    Boolean hasBlankNeighbor = false;
                    for (k = 0; k < Dir.Count; k++)
                    {
                        int neighborCellRow = i + Dir[k].X;
                        int neighborCellCol = j + Dir[k].Y;
                        if (!IsPositionInRange(neighborCellRow, neighborCellCol))
                        {
                            continue;
                        }
                        if (cellTable[neighborCellRow, neighborCellCol].Value == BlankCellValue)
                        {
                            hasBlankNeighbor = true;
                            continue;
                        }
                    }
                    if (!hasBlankNeighbor)
                    {
                        return true;
                    }
                }

            }
            return false;
        }

        public Cell GetCell(int iRow, int iCol)
        {
            int i;
            for (i = 0; i < this.lstPiece.Count; i++)
            {
                int j;
                for (j = 0; j < this.lstPiece[i].lstCell.Count; j++)
                {
                    if (lstPiece[i].lstCell[j].RowOnBoard != iRow)
                    {
                        continue;
                    }
                    if (lstPiece[i].lstCell[j].ColOnBoard != iCol)
                    {
                        continue;
                    }
                    return lstPiece[i].lstCell[j];

                }
            }
            return null;
        }


        public List<Cell> GetIsolateCell()
        {
            return GetIsolateCell(this.CellTable);
        }
        public List<Cell> GetIsolateCell(Cell[,] cellTable)
        {
            int i;
            int j;
            int k;
            StringBuilder strB = new StringBuilder();
            List<Cell> lstCell = new List<Cell>();

            for (i = 0; i < cellTable.GetLength(0); i++)
            {
                for (j = 0; j < cellTable.GetLength(1); j++)
                {
                    if (cellTable[i, j].Value != BlankCellValue)
                    {
                        continue;
                    }
                    Boolean hasBlankNeighbor = false;
                    for (k = 0; k < Dir.Count; k++)
                    {
                        int neighborCellRow = i + Dir[k].X;
                        int neighborCellCol = j + Dir[k].Y;
                        if (!IsPositionInRange(neighborCellRow, neighborCellCol))
                        {
                            continue;
                        }
                        if (cellTable[neighborCellRow, neighborCellCol].Value == BlankCellValue)
                        {
                            hasBlankNeighbor = true;
                            continue;
                        }
                    }
                    if (!hasBlankNeighbor)
                    {
                        lstCell.Add(new Cell(i, j, BlankCellValue));
                    }
                }

            }
            return lstCell;

        }
        public String GetBoardPosition()
        {
            int i;
            int j;
            StringBuilder strB = new StringBuilder();
            for (i = 0; i < CellTable.GetLength(0); i++)
            {
                for (j = 0; j < CellTable.GetLength(1); j++)
                {
                    strB.Append(CellTable[i, j].Value).Append(" ");
                }
                strB.Append(Environment.NewLine);
            }
            return strB.ToString();
        }
        public List<Piece> lstPiece = new List<Piece>();

        public void UnputLastPiece()
        {
            if (lstPiece.Count > 0)
            {

                int iLastIndex = lstPiece.Count - 1;
                UnPutPiece(iLastIndex);
            }
        }

        public void UnputLastNPiece(int NoofUnput)
        {
            if (lstPiece.Count > NoofUnput)
            {

                int i;
                for (i = 0; i < NoofUnput; i++)
                {
                    int iLastIndex = lstPiece.Count - 1;
                    UnPutPiece(iLastIndex);
                }

            }
        }
        public Boolean IsFullWithBlock()
        {
            int i;
            int j;
            for (i = 0; i < CellTable.GetLength(0); i++)
            {
                for (j = 0; j < CellTable.GetLength(1); j++)
                {
                    if (CellTable[i, j].Value == BlankCellValue)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public Boolean IsPositionInRange(int RowCheck, int ColCheck) => RowCheck >= 0
               && RowCheck < this.CellTable.GetLength(1)
               && ColCheck >= 0
               && ColCheck < this.CellTable.GetLength(0);

        public void Entervalue(int iRow, int iCol, int iValue)
        {
            if (!IsPositionInRange(iRow, iCol))
            {
                throw new ArgumentException($"Your Row and Col is {iRow}, {iCol} " +
                    $"which is not valid, please enter the value between (0 to {CellTable.GetLength(0) - 1}) ,(0 to {CellTable.GetLength(1) - 1}");
            }
            CellTable[iRow, iCol].Value = iValue;

        }

        public void EnterArrayvalue(int[,] arrValue)
        {
            int i;
            int j;
            for (i = 0; i < arrValue.GetLength(0); i++)
            {
                for (j = 0; j < arrValue.GetLength(1); j++)
                {
                    Entervalue(i, j, arrValue[i, j]);
                }
            }

        }
        public void PutPiece(int iRow, int iCol, Piece pPiece)
        {
            PutPiece(iRow, iCol, pPiece, this.CellTable);
        }
        public void PutPiece(int iRow, int iCol, Piece pPiece, Cell[,] cellTable)
        {

            int k = 0;
            for (k = 0; k < pPiece.lstCell.Count; k++)
            {
                int iPieceRow = pPiece.lstCell[k].Row;
                int iPieceCol = pPiece.lstCell[k].Col;
                cellTable[iRow + iPieceRow, iCol + iPieceCol] = pPiece.lstCell[k];

            }
            pPiece.BeingPutAt(iRow, iCol);
            lstPiece.Add(pPiece);

        }
        public void PutNumber(Piece pPiece, List<int> lst)
        {
            int k = 0;
            int iRow = pPiece.RowPut;
            int iCol = pPiece.ColPut;
            for (k = 0; k < pPiece.lstCell.Count; k++)
            {
                int iPieceRow = pPiece.lstCell[k].Row;
                int iPieceCol = pPiece.lstCell[k].Col;
                CellTable[iRow + iPieceRow, iCol + iPieceCol].Value = lst[k];
            }
            pPiece.HasPutNumber = true;

        }


        public void UnPutNumber(Piece pPiece)
        {
            int k = 0;
            int iRow = pPiece.RowPut;
            int iCol = pPiece.ColPut;
            for (k = 0; k < pPiece.lstCell.Count; k++)
            {
                int iPieceRow = pPiece.lstCell[k].Row;
                int iPieceCol = pPiece.lstCell[k].Col;
                CellTable[iRow + iPieceRow, iCol + iPieceCol].Value = BlankCellValue;

            }
            pPiece.HasPutNumber = false;
        }
        public void UnPutPiece(int indexPiece)
        {


            Piece pPiece = lstPiece[indexPiece];
            int iRow = pPiece.RowPut;
            int iCol = pPiece.ColPut;


            int k = 0;
            for (k = 0; k < pPiece.lstCell.Count; k++)
            {
                int iPieceRow = pPiece.lstCell[k].Row;
                int iPieceCol = pPiece.lstCell[k].Col;
                //Need to change from setting value to BlankCell 
                // to just create a new instance of cell table 
                Cell cell = CellTable[iRow + iPieceRow, iCol + iPieceCol];
                cell.Value = BlankCellValue;
                cell.UnboundFromPiece();
                //cell.SetPiece(null);
                //cell.SetPiece ()

            }

            lstPiece.RemoveAt(indexPiece);

        }

        public void CalculateCellStatus()
        {
            int i;
            int j;
            Cell.enCellStatus defaultCellValue = Cell.enCellStatus.PieceIsNotComplete;
            for (i = 0; i < CellTable.GetLength(0); i++)
            {
                for (j = 0; j < CellTable.GetLength(1); j++)
                {
                    CellTable[i, j].CellStatus = defaultCellValue;
                    if (CellTable[i, j].Value == 0)
                    {
                        CellTable[i, j].CellStatus = Cell.enCellStatus.Blank;
                        continue;
                    }
                    if (IsThisCellValueDupliate(i, j))
                    {
                        CellTable[i, j].CellStatus = Cell.enCellStatus.Duplicate;
                        continue;
                    }

                }
            }
            for (i = 0; i < lstPiece.Count; i++)
            {
                var piece = lstPiece[i];
                //First set newCellStautus to be IsCorrect or NotCorrect 
                Cell.enCellStatus newCellStatus = piece.IsAnswerMatch
    ? Cell.enCellStatus.PieceIsCorrect
    : Cell.enCellStatus.PieceIsNotCorrect;

                //If it is not correct but it also has empty cell, we count it as Notcomplete
                if (newCellStatus == Cell.enCellStatus.PieceIsNotCorrect)
                {
                    if (piece.IsThereEmptyCell)
                    {
                        newCellStatus = Cell.enCellStatus.PieceIsNotComplete;
                    }
                }


                //Find if there is at least one cell in a piece has a duplicate cell
                bool hasDuplicateCell = false;
                for (j = 0; j < piece.lstCell.Count; j++)
                {
                    if (piece.lstCell[j].CellStatus == Cell.enCellStatus.Duplicate)
                    {
                        hasDuplicateCell = true;
                        break;
                    }
                }

                //If piece it self is correct
                //but cell value is duplicate with other piece
                //We count it as Match but contain duplicate
                if (newCellStatus == Cell.enCellStatus.PieceIsCorrect
                    && hasDuplicateCell)
                {
                    newCellStatus = Cell.enCellStatus.PieceMatchbutContainDuplicate;
                }


                for (j = 0; j < piece.lstCell.Count; j++)
                {

                    var cell = piece.lstCell[j];
                    if (cell.CellStatus == Cell.enCellStatus.Duplicate
                    || cell.CellStatus == Cell.enCellStatus.Blank)
                    {
                        continue;
                    }
                    cell.CellStatus = newCellStatus;
                }
            }

        }
        public void ClearBoard()
        {
            int i;
            int j;
            CellTable = new Cell[this.BoardSize, this.BoardSize];
            for (i = 0; i < CellTable.GetLength(0); i++)
            {
                for (j = 0; j < CellTable.GetLength(1); j++)
                {
                    CellTable[i, j] = new Cell(i, j, BlankCellValue);
                }
            }
        }
        public StringBuilder strB = new StringBuilder();
        public void LogMore(String str)
        {
            strB.Append(str).Append(Environment.NewLine);
        }





        public void CopyTargetNumberToNumberForDebugPurpose()
        {
            int i;
            for (i = 0; i < this.lstPiece.Count; i++)
            {
                int j;
                for (j = 0; j < this.lstPiece[i].lstCell.Count; j++)
                {
                    this.lstPiece[i].lstCell[j].Value = this.lstPiece[i].lstCell[j].TargetValue;

                    this.CellTable[this.lstPiece[i].lstCell[j].RowOnBoard,
                        this.lstPiece[i].lstCell[j].ColOnBoard].Value = this.lstPiece[i].lstCell[j].TargetValue;


                }
            }


        }
        public Boolean IsCorrectAnswer()
        {

            int i;

            for (i = 0; i < this.lstPiece.Count; i++)
            {
                if (!this.lstPiece[i].IsAnswerMatch)
                {
                    return false;
                }
            }



            int j = 0;

            Dictionary<int, HashSet<int>> dicRow = new Dictionary<int, HashSet<int>>();
            Dictionary<int, HashSet<int>> dicColumn = new Dictionary<int, HashSet<int>>();

            try
            {
                for (i = 1; i <= this.BoardSize; i++)
                {
                    dicRow.Add(i, new HashSet<int>());
                    for (j = 1; j <= this.BoardSize; j++)
                    {
                        int CellValue = this.CellTable[i - 1, j - 1].Value;
                        if (dicRow[i].Contains(CellValue))
                        {
                            return false;
                        }
                        dicRow[i].Add(CellValue);
                    }
                }

                for (i = 1; i <= this.BoardSize; i++)
                {

                    dicColumn.Add(i, new HashSet<int>());
                    for (j = 1; j <= this.BoardSize; j++)
                    {
                        int CellValue = this.CellTable[j - 1, i - 1].Value;
                        if (dicColumn[i].Contains(CellValue))
                        {
                            return false;
                        }
                        dicColumn[i].Add(CellValue);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
                //  LogMore ()
                //  return false;
            }
            return true;


        }


        public void SetTargetNumber(int[,] TargetCellValue)
            => SetArrayToListPiecesTargetValue(TargetCellValue);

        private void CalTargetNumberForAllPieces()
            => this.lstPiece.ForEach(x => x.CalTargetNumber());

        /*
        Dictionary<Piece.PieceOperation, int> defaultDicOperationChance = new Dictionary<Piece.PieceOperation, int>()
        {
            { Piece.PieceOperation.Add ,10 }, // We actully don't need add
            { Piece.PieceOperation.Subtract ,40 },
            { Piece.PieceOperation.Multiply  ,90 },
            {Piece.PieceOperation.Divide ,100 }
        };
        */
        Dictionary<Piece.PieceOperation, int> defaultDicOperationChance = new Dictionary<Piece.PieceOperation, int>()
        {
            { Piece.PieceOperation.Add ,10 }, // We actully don't need add
            { Piece.PieceOperation.Subtract ,40 },
            {Piece.PieceOperation.Divide ,60 }
        };
        private void AssignOperationToPiece(Boolean IsAcceptNegativeTargetNumber, Dictionary<Piece.PieceOperation, int> dicOpertorChance)
        {
            int i;


            // Set some of them to use divided
            for (i = 0; i < this.lstPiece.Count; i++)
            {
                var piece = lstPiece[i];
                // Set default Operation to be Add for all of the pieces

                Piece.PieceOperation DefaultOperator = Piece.PieceOperation.Add;
                piece.Operation = DefaultOperator;




                // Calculate the chance that it will be other value more than add

                int iOperationChance = Baseclass.MyRandom.Random(0, 100);
                foreach (Piece.PieceOperation Oper in dicOpertorChance.Keys)
                {
                    int iSum = 0;
                    if (iOperationChance > dicOpertorChance[Oper])
                    {
                        continue;
                    }

                    var AssignOperator = Oper;
                    if (AssignOperator == Piece.PieceOperation.Subtract)
                    {
                        if (!IsAcceptNegativeTargetNumber)
                        {
                            /*Set AssignOperator to be Add if the result is a Negative
                              This is a process to prevent the result to be negative number.
                                                              
                             Supposing this is the value of the cell in piece
                             cell1:5
                             cell2:3
                             cell3:4
                             1. set iSum to be 5
                             2. iSum - 3 - 4;
                             3. if iSum < 0 we will not use Substract as Operator
                             we will ust use Add instead.
                            */
                            //1. Assign iSum to be the first cell value
                            iSum = piece.lstCell[0].TargetValue;
                            int j;

                            //2. substrat iSum value from the rest of the cell in piece
                            for (j = 1; j < piece.lstCell.Count; j++)
                            {
                                iSum -= piece.lstCell[j].TargetValue;
                            }

                            //3. If iSum is less than 0 we will forece the operation to be Add instead.
                            if (iSum < 0)
                            {

                                AssignOperator = DefaultOperator;
                            }

                        }

                    }
                    else if (AssignOperator == Piece.PieceOperation.Divide)
                    {
                        /*The condition that we will use Divide.
                            1. The number of cell must be 2 
                            2. The result after from the division must be integer 

                            For example
                            case 1
                            cell1:6
                            cell2:3
                            This case is valid because 2=6/3

                            case 2
                            cell1:5
                            cell2:2
                            This case is invalid becasue 2.5=5/2

                        */
                        if (piece.lstCell.Count != 2)
                        {
                            AssignOperator = DefaultOperator;
                        }
                        else
                        {

                            double doubleResult = (double)(piece.lstCell[0].TargetValue) /
                                            (double)(piece.lstCell[1].TargetValue);

                            Boolean IsResultAfterDiviedInteger = (doubleResult % 1 == 0);
                            if (!IsResultAfterDiviedInteger)
                            {
                                AssignOperator = DefaultOperator;
                            }
                        }
                    }


                    if (piece.PType == Piece.PieceType.Dot)
                    {
                        //For Dot piece type, we don't allow other operator beside Add
                        piece.Operation = Piece.PieceOperation.Add;
                    }
                    else
                    {
                        piece.Operation = AssignOperator;
                    }
                    break;
                }



            }
        }
        private void AssignOperationToPiece(Boolean IsAcceptNegativeTargetNumber)
        {
            AssignOperationToPiece(IsAcceptNegativeTargetNumber, defaultDicOperationChance);
        }
        private void SetArrayToListPieces()
        {

            int j;
            int k;
            for (j = 0; j < this.lstPiece.Count; j++)
            {
                for (k = 0; k < this.lstPiece[j].lstCell.Count; k++)
                {
                    int iPieceRow = this.lstPiece[j].RowPut + this.lstPiece[j].lstCell[k].Row;
                    int iPieceCol = this.lstPiece[j].ColPut + this.lstPiece[j].lstCell[k].Col;
                    this.lstPiece[j].lstCell[k].Value = CellTable[iPieceRow, iPieceCol].Value;
                }

            }
        }

        private void SetArrayToListPiecesTargetValue(int[,] pCellValue)
        {

            int j;
            int k;
            for (j = 0; j < this.lstPiece.Count; j++)
            {
                for (k = 0; k < this.lstPiece[j].lstCell.Count; k++)
                {
                    int iPieceRow = this.lstPiece[j].RowPut + this.lstPiece[j].lstCell[k].Row;
                    int iPieceCol = this.lstPiece[j].ColPut + this.lstPiece[j].lstCell[k].Col;
                    this.lstPiece[j].lstCell[k].TargetValue = pCellValue[iPieceRow, iPieceCol];
                }

            }
        }

        public Boolean CanUseTheseNumber(Piece pPiece, List<int> lst)
        {

            int k;
            int iRow = pPiece.RowPut;
            int iCol = pPiece.ColPut;
            Board testBoard = this.Clone();
            for (k = 0; k < pPiece.lstCell.Count; k++)
            {
                int iPieceRow = pPiece.lstCell[k].Row;
                int iPieceCol = pPiece.lstCell[k].Col;

                testBoard.CellTable[iRow + iPieceRow, iCol + iPieceCol].Value = lst[k];
                if (!testBoard.IsValidNumberInRow(iRow + iPieceRow))
                {
                    return false;
                }

                if (!testBoard.IsValidNumberInCol(iCol + iPieceCol))
                {
                    return false;
                }
            }
            return true;
        }
        public Boolean CanPut(int iRow, int iCol, Piece pPiece)
        {

            int k;
            for (k = 0; k < pPiece.lstCell.Count; k++)
            {
                int iPieceRow = pPiece.lstCell[k].Row;
                int iPieceCol = pPiece.lstCell[k].Col;

                if (!IsPositionInRange(iRow + iPieceRow, iCol + iPieceCol))
                {
                    return false;
                }
                if (CellTable[iRow + iPieceRow, iCol + iPieceCol].Value != BlankCellValue)
                {
                    return false;
                }
            }

            if (IsPreventIsolateCell)
            {

                Board NewBoard = this.Clone();
                NewBoard.PutPiece(iRow, iCol, pPiece);

                if (NewBoard.HasIsolateCell())
                {
                    return false;
                }

            }
            return true;
        }
        public Board Clone()
        {
            Board NewBoard = new Board();
            // NewBoard.CellValue =(int[,]) this.CellValue.Clone();
            NewBoard.CellTable = (Cell[,])this.CellTable.Clone();
            int i;
            for (i = 0; i < lstPiece.Count; i++)
            {
                NewBoard.lstPiece.Add(lstPiece[i].Clone());
            }
            return NewBoard;
        }
        private HashSet<String> _hshPieceAttempCache = null;
        private HashSet<String> hshPieceAttempCache
        {
            get
            {
                if (_hshPieceAttempCache == null)
                {
                    _hshPieceAttempCache = new HashSet<string>();
                }
                return _hshPieceAttempCache;
            }
        }
        private void AddPieceAttemptoCache(Piece pPiece) =>
            hshPieceAttempCache.Add(pPiece.Key);

        private Boolean IsExistsInCannotPutCache(Piece pPiece) =>
            hshPieceAttempCache.Contains(pPiece.Key);

        //  private Boolean _IsPreventIsolateCell = true;
        private Boolean IsPreventIsolateCell { get; } = true;
        private void ClearPieceCannotPutCache() => _hshPieceAttempCache = null;

        private Boolean IsKeepCannotPutincache = false;

        public Boolean TryToPut(Piece pPiece)
        {
            int i;
            int j;

            if (IsKeepCannotPutincache)
            {
                if (IsExistsInCannotPutCache(pPiece))
                {
                    return false;
                }
            }
            Boolean CanTrytoPut = false;
            for (i = 0; i < CellTable.GetLength(0) && !CanTrytoPut; i++)
            {
                for (j = 0; j < CellTable.GetLength(1); j++)
                {
                    if (CellTable[i, j].Value != BlankCellValue)
                    {
                        continue;
                    }

                    CanTrytoPut = CanPut(i, j, pPiece);
                    if (!CanTrytoPut)
                    {
                        continue;
                    }
                    PutPiece(i, j, pPiece);

                    if (IsKeepCannotPutincache)
                    {
                        ClearPieceCannotPutCache();
                    }
                    break;
                }
            }
            if (!CanTrytoPut)
            {
                if (IsKeepCannotPutincache)
                {
                    AddPieceAttemptoCache(pPiece);
                }
            }
            return CanTrytoPut;
        }
    }
}
