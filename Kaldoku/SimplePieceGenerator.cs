using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kaldoku
{
    class SimplePieceGenerator:IPieceSelector 
    {

        public  Piece.PieceType GetPieceTypeByRandom(int iRandom)
        {
            
            if(iRandom <= 0) {                
                return  Piece.PieceType.I2 ;
            }

            if(iRandom <= 50){
                return Piece.PieceType.I3 ;
            }
            if(iRandom <= 80) {
                return Piece.PieceType.I4 ;
            }
        
            return Piece.PieceType.O;

        }
        
    }
}


