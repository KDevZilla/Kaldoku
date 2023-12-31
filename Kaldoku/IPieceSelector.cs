using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kaldoku
{
    public interface IPieceSelector
    {
        Piece.PieceType GetPieceTypeByRandom(Int32 RandomNumber);
    }
}
