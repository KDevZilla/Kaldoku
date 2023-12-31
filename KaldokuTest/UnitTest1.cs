using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kaldoku;


namespace KaldokuTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CreatePiece()
        {
            Piece doesNotSupportRotationPieceType = Piece.Create(Piece.PieceType.I2, Piece.PieceRotation.Rotate180);
            Assert.AreEqual(doesNotSupportRotationPieceType.PType, Piece.PieceType.I2);
            Assert.AreEqual(doesNotSupportRotationPieceType.Rotation, Piece.PieceRotation.Rotate360);
            Assert.AreEqual(doesNotSupportRotationPieceType.Key, "I2_Rotate360");
            Assert.AreEqual(doesNotSupportRotationPieceType.keyAndPosition, "I2_Rotate360_-1_-1");


            Piece piece = Piece.Create(Piece.PieceType.J3, Piece.PieceRotation.Rotate180);
           
            Assert.AreEqual(piece.PType, Piece.PieceType.J3);
            Assert.AreEqual(piece.Rotation,  Piece.PieceRotation.Rotate180);
            Assert.AreEqual(piece.Key , "J3_Rotate180");
            Assert.AreEqual(piece.keyAndPosition, "J3_Rotate180_-1_-1");
            Assert.AreEqual(piece.RowPut, -1);
            Assert.AreEqual(piece.ColPut , -1);

            Board board = new Board(6);
            board.PutPiece(0, 2, piece);
            Assert.AreEqual(piece.RowPut, 0);
            Assert.AreEqual(piece.ColPut, 2);
            Assert.AreEqual(piece.keyAndPosition, "J3_Rotate180_0_2");




        }
        [TestMethod]
        public void CreateBoard()
        {
            Board Board = new Board(4);
            Piece piece = Piece.Create(Piece.PieceType.I2, Piece.PieceRotation.Rotate180);
            piece.BeingPutAt(1, 2);

            Board.PutPiece(1, 2, piece);
            Board Board2 = new Board(4);

            Piece piece2 = Piece.Create(Piece.PieceType.I2, Piece.PieceRotation.Rotate180);
            Board2.PutPiece(1, 2, piece2);

            Assert.IsTrue(BoardUtil.IsBoardsTheSame(Board, Board2));
            Assert.IsFalse(Board.IsCorrectAnswer());
        }

        [TestMethod]
        public void CreateBoardByListPieceString()
        {
            string listPieceString = @"J3_Rotate270_0_0|J2_Rotate180_1_0|I2_Rotate360_0_3|J3_Rotate90_2_0|L2_Rotate180_2_2|";
            //int[,] targetNumber = PregenratedNumber.getTargetCellNumber(4);
            var targetNumber = new int[,]{
{1,2,3,4,},
{2,1,4,3,},
{4,3,1,2,},
{3,4,2,1,},
};
            Boolean isAcceptNegativeTargetNumber = false;
            Board board = Board.Create(4, listPieceString, targetNumber, isAcceptNegativeTargetNumber);

            Assert.AreEqual(board.lstPiece.Count, 5);
            string[] arrPieceString = listPieceString.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            int i;
            for (i = 0; i < board.lstPiece.Count; i++)
            {
                Assert.AreEqual(board.lstPiece[i].keyAndPosition, arrPieceString[i]);
            }

            for (i = 0; i < board.lstPiece.Count; i++)
            {
                Piece newPiece = Piece.Create(arrPieceString[i]);
                Assert.AreEqual(board.lstPiece[i].Key , newPiece.Key);
            }

            Assert.AreEqual(listPieceString, board.GetListPieceString());
            
            //Assert.AreEqual (board.lstPiece [0].keyAndPosition,)
            //J3_Rotate270_0_0|J2_Rotate180_1_0|I2_Rotate360_0_3|J3_Rotate90_2_0|L2_Rotate180_2_2|
        }
        [TestMethod]
        public void EnterArrayNumber()
        {
            // string listPieceString = @"J3_Rotate270_0_0|J2_Rotate180_1_0|I2_Rotate360_0_3|J3_Rotate90_2_0|L2_Rotate180_2_2|";
            string listPieceString = @"I2_Rotate360_0_0|L3_Rotate180_0_1|I4_Rotate90_3_0|I3_Rotate360_0_3|L2_Rotate270_1_1|";
            var targetNumber = new int[,]{
{1,2,3,4,},
{2,1,4,3,},
{4,3,1,2,},
{3,4,2,1,},
};
            Boolean isAcceptNegativeTargetNumber = false;
            Board board = Board.Create(4, listPieceString, targetNumber, isAcceptNegativeTargetNumber);

            Assert.AreEqual(board.lstPiece.Count, 5);
            var enterNumber = new int[,]
            {
                {3,2,1,3,},
{4,0,4,2,},
{0,0,3,4,},
{0,0,4,0,},
            };
            board.EnterArrayvalue(enterNumber);

            int i;
            int j;
            for(i=0;i<enterNumber.GetLength(0); i++)
            {
                for (j = 0; j < enterNumber.GetLength(1); j++)
                {
                    Assert.AreEqual(board.GetCell(i, j).Value, enterNumber[i, j]);
                }
            }
        }
        [TestMethod]
        public void CalculateCellStatus2()
        {
            string listPieceString = @"I2_Rotate360_0_0|L3_Rotate180_0_1|I4_Rotate90_3_0|I3_Rotate360_0_3|L2_Rotate270_1_1|";

            var targetNumber = new int[,]{
{1,2,3,4,},
{2,1,4,3,},
{4,3,1,2,},
{3,4,2,1,},
};
            Boolean isAcceptNegativeTargetNumber = false;
            Board board = Board.Create(4, listPieceString, targetNumber,isAcceptNegativeTargetNumber);

            Assert.AreEqual(board.lstPiece.Count, 5);
           
            var arrEnterNumber = new int[,]
            {
{3,2,1,3,},
{4,0,4,2,},
{0,0,3,4,},
{0,0,4,0,},
            };
            board.EnterArrayvalue(arrEnterNumber);
            board.CalculateCellStatus();
            var arrExptectedCellStatus = new Kaldoku.Cell.enCellStatus[,]
{
                { Cell.enCellStatus.Duplicate, Cell.enCellStatus.PieceMatchbutContainDuplicate,Cell.enCellStatus.PieceMatchbutContainDuplicate, Cell.enCellStatus.Duplicate },
                { Cell.enCellStatus.Duplicate, Cell.enCellStatus.Blank,Cell.enCellStatus.Duplicate, Cell.enCellStatus.PieceMatchbutContainDuplicate },
                { Cell.enCellStatus.Blank, Cell.enCellStatus.Blank,Cell.enCellStatus.PieceMatchbutContainDuplicate, Cell.enCellStatus.PieceMatchbutContainDuplicate },
                { Cell.enCellStatus.Blank, Cell.enCellStatus.Blank,Cell.enCellStatus.Duplicate, Cell.enCellStatus.Blank},

};

            int i;
            int j;

            for (i = 0; i < arrEnterNumber.GetLength(0); i++)
            {
                for (j = 0; j < arrEnterNumber.GetLength(1); j++)
                {
                    Assert.AreEqual(board.GetCell(i, j).CellStatus, arrExptectedCellStatus[i, j]);
                }
            }




          
        }

        [TestMethod]
        public void EnterCorrectValue()
        {
            string listPieceString = @"J3_Rotate270_0_0|J2_Rotate180_1_0|I2_Rotate360_0_3|J3_Rotate90_2_0|L2_Rotate180_2_2|";

            int[,] targetNumber = new int[,]
            {
                { 1,2,3,4 },
                { 2,3,4,1},
                { 3,4,1,2},
                { 4,1,2,3}
            };
            Boolean isAcceptNegativeTargetNumber = false;
            Board board = Board.Create(4, listPieceString, targetNumber, isAcceptNegativeTargetNumber);


            int i;
            int j;
            Assert.IsFalse(board.IsCorrectAnswer());
            for (i = 0; i <= targetNumber.GetUpperBound(0); i++)
            {
                for (j = 0; j <= targetNumber.GetUpperBound(1); j++)
                {
                    board.Entervalue(i, j, targetNumber[i, j]);
                }
            }
            Assert.IsTrue(board.IsCorrectAnswer());
           
        }
        [TestMethod]
        public void CalculateCellStatus()
        {
            // string listPieceString = @"J3_Rotate270_0_0|J2_Rotate180_1_0|I2_Rotate360_0_3|J3_Rotate90_2_0|L2_Rotate180_2_2|";
            string listPieceString = @"I2_Rotate360_0_0|L3_Rotate180_0_1|I4_Rotate90_3_0|I3_Rotate360_0_3|L2_Rotate270_1_1|";

            var targetNumber = new int[,]{
{1,2,3,4,},
{2,1,4,3,},
{4,3,1,2,},
{3,4,2,1,},
};
            Boolean isAcceptNegativeTargetNumber = false;
            Board board = Board.Create(4, listPieceString,targetNumber,isAcceptNegativeTargetNumber);

            Assert.AreEqual(board.lstPiece.Count, 5);
            var arrEnterNumber = new int[,]
{
{0,0,0,0,},
{0,0,0,0,},
{0,0,0,0,},
{0,0,0,0,},
};
            board.EnterArrayvalue(arrEnterNumber);
            board.CalculateCellStatus();
            int i;
            int j;
            for (i = 0; i < arrEnterNumber.GetLength(0); i++)
            {
                for (j = 0; j < arrEnterNumber.GetLength(1); j++)
                {
                    Assert.AreEqual(board.GetCell(i, j).CellStatus, Cell.enCellStatus.Blank);
                }
            }

           

            board.EnterArrayvalue(targetNumber);
            board.CalculateCellStatus();
            for (i = 0; i < arrEnterNumber.GetLength(0); i++)
            {
                for (j = 0; j < arrEnterNumber.GetLength(1); j++)
                {
                    Assert.AreEqual(board.GetCell(i, j).CellStatus, Cell.enCellStatus.PieceIsCorrect);
                }
            }
        }

        [TestMethod]
        public void EnterCorrectValue2()
        {
            string listPieceString = @"J3_Rotate270_0_0|J2_Rotate180_1_0|I2_Rotate360_0_3|J3_Rotate90_2_0|L2_Rotate180_2_2|";

            int[,] targetNumber = new int[,]
            {
                { 1,2,3,4 },
                { 2,3,4,1},
                { 3,4,1,2},
                { 4,1,2,3}
            };
            Boolean isAcceptNegativeTargetNumber = false;
            Board board = Board.Create(4, listPieceString, targetNumber, isAcceptNegativeTargetNumber );


            int i;
            int j;
            Assert.IsFalse(board.IsCorrectAnswer());
            for(i=0;i<=targetNumber.GetUpperBound(0); i++)
            {
                for(j=0;j<=targetNumber.GetUpperBound(1); j++)
                {
                    board.Entervalue(i, j, targetNumber[i, j]);
                }
            }
            Assert.IsTrue(board.IsCorrectAnswer());
            //board.IsCorrectAnswer 
            //Assert.AreEqual (board.lstPiece [0].keyAndPosition,)
            //J3_Rotate270_0_0|J2_Rotate180_1_0|I2_Rotate360_0_3|J3_Rotate90_2_0|L2_Rotate180_2_2|
        }
        [TestMethod]
        public void EnterValue()
        {
            Board Board = new Board(4);
            Piece piece = Piece.Create(Piece.PieceType.I2, Piece.PieceRotation.Rotate180);
            piece.BeingPutAt(1, 2);

            Board.PutPiece(1, 2, piece);

            Board.Entervalue(1, 2, 3);
            Assert.AreEqual(Board.lstPiece[0].lstCell[0].Value, 3);

            Assert.AreEqual(Board.GetCell(1, 2).Value, 3);

        }
    }
}
