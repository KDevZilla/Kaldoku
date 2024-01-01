using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kaldoku
{
    public class BoardUtil
    {
        public static bool IsCellsTheSame(Cell cell1, Cell cell2)
        {
            bool IsInvalid = false;
            IsInvalid = (cell1 == null || cell2 == null)
                || (cell1.Row != cell2.Row)
                || (cell1.Col != cell2.Col)
                || (cell1.ColOnBoard != cell2.ColOnBoard)
                || (cell1.RowOnBoard != cell2.RowOnBoard)
                || (cell1.TargetValue != cell2.TargetValue)
                || (cell1.Value != cell2.Value);
            return !IsInvalid;

        }
        public static bool IsPiecesTheSame(Piece piece1, Piece piece2)
        {
            if(piece1 ==null || piece2 == null)
            {
                return false;
            }
            piece1.CalNumber();
            piece2.CalNumber();
            bool IsInvalid = false;
            IsInvalid = 
                   (piece1.lstCell.Count != piece2.lstCell.Count)
                || (piece1.PType != piece2.PType)
                || (piece1.TargetNumber != piece2.TargetNumber)
                || (piece1.RowPut != piece2.RowPut)
                || (piece1.ColPut != piece2.ColPut)
                || (piece1.NumberFromCalculate1  != piece2.NumberFromCalculate1 )
                || (piece1.NumberFromCalculate2  != piece2.NumberFromCalculate2 )
                || (piece1.HasPutNumber != piece2.HasPutNumber)
                || (piece1.IsAnswerMatch != piece2.IsAnswerMatch)
                || (piece1.Key != piece2.Key)
                || (piece1.keyAndPosition != piece2.keyAndPosition)
                || (piece1.Operation != piece2.Operation)
                || (piece1.OperationString != piece2.OperationString);
            if (IsInvalid)
            {
                return false;
            }
            int i;
            int j;
            for (i = 0; i < piece1.lstCell.Count; i++)
            {


                if (!IsCellsTheSame(piece1.lstCell[0], piece2.lstCell[0]))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool IsBoardsTheSame(Board board1, Board board2)
        {
            bool IsInvalid = false;
            IsInvalid = (board1 == null || board2 == null)
                || (board1.lstPiece.Count != board2.lstPiece.Count)
                || (board1.IsShowCellTargetValue != board2.IsShowCellTargetValue)
                || (board1.IsFullWithBlock() != board2.IsFullWithBlock())
                || (board1.BoardSize != board2.BoardSize);
            if (IsInvalid)
            {
                return false;
            }

            int i;
            int j;
            for (i = 0; i < board1.lstPiece.Count; i++)
            {
                if (!IsPiecesTheSame(board1.lstPiece[i], board2.lstPiece[i]))
                {
                    return false;
                }
            }
            return true;
        }


    }
}
