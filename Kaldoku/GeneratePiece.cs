using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kaldoku
{
    public class GeneratePiece
    {
        Random Random = new Random();

        private IPieceSelector _PieceGenerator = null;
        public GeneratePiece(IPieceSelector pPieceGenerator)
        {
            _PieceGenerator = pPieceGenerator;
        }
        public Piece GenPiece(Piece.PieceType ePiece)
        {
            Piece P = null;
            P = Piece.Create(ePiece);
            return P;
        }

        public Piece GenPiece(Piece.PieceType ePiece, Piece.PieceRotation Rotation)
        {
            Piece P = null;
            P = Piece.Create(ePiece, Rotation);
            return P;
        }

        public Piece GenPiece()
        {
            int Result = Random.Next(100);

            Piece.PieceType ePiece;
            ePiece = _PieceGenerator.GetPieceTypeByRandom(Result);

            int iRotation = Random.Next(5);
            Piece.PieceRotation Rotation = Piece.PieceRotation.Rotate360;
            if (iRotation >= 0)
            {

                if (iRotation < 2)
                {
                    Rotation = Piece.PieceRotation.Rotate90;
                }
                else
                {
                    if (iRotation < 3)
                    {
                        Rotation = Piece.PieceRotation.Rotate180;
                    }
                    else
                    {
                        Rotation = Piece.PieceRotation.Rotate270;
                    }
                }

            }
            return GenPiece(ePiece, Rotation);


        }
    }
}
