using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kaldoku
{

    public class Cell
    {

        private Piece _Piece = null;

        public enum enCellStatus
        {
            Blank,
            Duplicate,  //Duplicate with other cell in row/column
            PieceIsNotComplete, //Contain blank cell
            PieceIsNotCorrect,  //No blank cell but contains duplicate or does not match with target value
            PieceIsCorrect, // no match 
            PieceMatchbutContainDuplicate,
        }
        public enCellStatus CellStatus { get; set; } = enCellStatus.Blank;
        public void SetPiece(Piece pPiece)
        {
            _Piece = pPiece;
            _Piece.PieceBeingPutOnBoard += _Piece_PieceBeingPutOnBoard;
        }
        public void UnboundFromPiece()
        {
            _Piece = null;
        }
        private void _Piece_PieceBeingPutOnBoard(object sender, EventArgs e)
        {

            this.RowOnBoard = this.Row + _Piece.RowPut;
            this.ColOnBoard = this.Col + _Piece.ColPut;
        }

        public int Row { get; private set; }
        public int Col { get; private set; }
        public int Value { get; set; }
        public int TargetValue { get; set; }
        public int RowOnBoard { get; private set; }
        public int ColOnBoard { get; private set; }
        public Cell(int pRow, int pCol)
        {
            this.InitializeProperty(pRow, pCol, 0);

        }
        private void InitializeProperty(int pRow, int pCol, int pValue)
        {
            Row = pRow;
            Col = pCol;
            Value = pValue;
        }
        public Cell(int pRow, int pCol, int pValue)
        {
            this.InitializeProperty(pRow, pCol, pValue);
        }
    }
}
