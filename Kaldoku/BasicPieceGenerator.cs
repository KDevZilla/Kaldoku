using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kaldoku
{
    public class BasicPieceGenerator : IPieceSelector
    {


        public Piece.PieceType GetPieceTypeByRandom(int iRandom)
        {

            if (iRandom <= 0)
            {
                return Piece.PieceType.Dot;
            }
            if (iRandom <= 5)
            {
                return Piece.PieceType.I2;
            }
            if (iRandom <= 10)
            {
                return Piece.PieceType.I3;
            }
            if (iRandom <= 20)
            {
                return Piece.PieceType.I4;
            }
            if (iRandom <= 30)
            {
                return Piece.PieceType.J2;
            }
            if (iRandom <= 40)
            {
                return Piece.PieceType.J3;
            }
            if (iRandom <= 50)
            {
                return Piece.PieceType.L2;
            }
            if (iRandom <= 60)
            {
                return Piece.PieceType.L3;
            }
            if (iRandom <= 70)
            {
                return Piece.PieceType.O;
            }
            if (iRandom <= 80)
            {
                return Piece.PieceType.S;
            }
            if (iRandom <= 90)
            {
                return Piece.PieceType.T;
            }

            return Piece.PieceType.Z;

        }

    }
}
