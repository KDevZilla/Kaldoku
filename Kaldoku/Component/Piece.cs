using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kaldoku
{

    public class Piece
    {
        public enum PieceOperation
        {
            Add,
            Subtract,
            Multiply,
            Divide
        }
        public enum PieceType
        {
            I2,
            I3,
            I4,

            L2,
            L3,

            J2,
            J3,

            S,
            T,
            Z,
            O,
            Dot,


        }
        public String Key
        {
            get
            {
                String PieceTypeString = Enum.GetName(typeof(PieceType), this.PType);
                String PieceRotation = Enum.GetName(typeof(PieceRotation), this.Rotation);

                return PieceTypeString + "_" + PieceRotation;
            }
        }
        public String keyAndPosition => Key + "_" + RowPut + "_" + ColPut;

        public Boolean HasPutNumber { get; set; } = false;

        public enum PieceRotation
        {
            Rotate90,
            Rotate180,
            Rotate270,
            Rotate360
        }
        private Board _Board;

        public PieceOperation Operation { get; set; } = PieceOperation.Add;

        public String OperationString
        {
            get
            {
                switch (Operation)
                {
                    case PieceOperation.Add: return "+";
                    case PieceOperation.Subtract: return "-";
                    case PieceOperation.Multiply: return "×";
                    case PieceOperation.Divide: return "÷";
                    default: return "";
                }
            }
        }
        public int NumberFromCalculate1  => _Number1;
        public int NumberFromCalculate2 => _Number2;
        private int _Number1 = int.MinValue;
        private int _Number2 = int.MinValue;

        public int TargetNumber { get; private set; } = -1;
        private Boolean _IsAnswerMatch = false;
        public Boolean IsAnswerMatch
        {
            get
            {
                if (IsThereZeroValueCell())
                {
                    return false;
                }

                CalNumber();
                _IsAnswerMatch = (TargetNumber == _Number1) ||
                    (TargetNumber == _Number2);
                return _IsAnswerMatch;
            }
        }
        public void CalTargetNumber()
            => TargetNumber = CalculateTargetNumber();

        public void CalNumber()
        {
            CalculateNumber(ref _Number1,ref  _Number2);
        }
            

        public int CalculateTargetNumber()
        {
            int i;
            int iSum = 0;
            for (i = 0; i < this.lstCell.Count; i++)
            {
                switch (this.Operation)
                {
                    case PieceOperation.Add:
                        iSum += lstCell[i].TargetValue;
                        break;
                    case PieceOperation.Subtract:
                        if (i == 0)
                        {
                            iSum = lstCell[i].TargetValue;
                        }
                        else
                        {
                            iSum -= lstCell[i].TargetValue;
                        }
                        break;
                    case PieceOperation.Multiply:
                        if (i == 0)
                        {
                            iSum = lstCell[i].TargetValue;
                        }
                        else
                        {
                            iSum *= lstCell[i].TargetValue;
                        }
                        break;
                    case PieceOperation.Divide:
                        if (i == 0)
                        {
                            iSum = lstCell[i].TargetValue;
                        }
                        else
                        {
                            iSum = iSum / lstCell[i].TargetValue;
                        }
                        break;

                }

            }
            return iSum;
        }

        private Boolean IsThereZeroValueCell()
        {
            int i = 0;
            for (i = 0; i < this.lstCell.Count; i++)
            {
                if (this.lstCell[i].Value == 0)
                {
                    return true;
                }

            }
            return false;
        }
        private const int InvalidValue = int.MinValue;
        public void CalculateNumber(ref int Number1, ref int Number2)
        {
            int i;
            //int iSum = 0;
            Number1 = InvalidValue;
            Number2 = InvalidValue;

            int numberTemp = 0;
            for (i = 0; i < this.lstCell.Count; i++)
            {
                switch (this.Operation)
                {
                    case PieceOperation.Add:
                        if (i == 0)
                        {
                            Number1 = lstCell[i].Value;
                        }
                        else
                        {
                            Number1 += lstCell[i].Value;
                        }
                        break;
                    case PieceOperation.Subtract:
                        if (i == 0)
                        {
                            numberTemp  = lstCell[i].Value;
                        }
                        else
                        {
                            Number1 = numberTemp - lstCell[i].Value;
                            Number2 = lstCell[i].Value - numberTemp;
                            //iSum -= lstCell[i].Value;
                        }
                        break;
                    case PieceOperation.Multiply:
                        if (i == 0)
                        {
                            Number1 = lstCell[i].Value;
                        }
                        else
                        {
                            Number1 *= lstCell[i].Value;
                        }
                        break;
                    case PieceOperation.Divide:
                        if (i == 0)
                        {
                            numberTemp  = lstCell[i].Value;
                        }
                        else
                        {
                            int tempNumber1 = CalculateDivide(numberTemp, lstCell[i].Value);
                            int tempNumber2 = CalculateDivide(lstCell[i].Value, numberTemp);
                            if(tempNumber1 != InvalidValue)
                            {
                                Number1 = tempNumber1;
                            }
                            if(tempNumber2 != InvalidValue)
                            {
                                Number2 = tempNumber2;
                            }
                            
                        }
                        break;

                }

            }
        }
        private int CalculateDivide(int numA, int numB)
        {
            //For this game there is no case that the number is 0
            try
            {
                if (numA.Equals(0) ||
                    numB.Equals(0))
                {
                    return InvalidValue;
                }
                double result = (double)numA / (double)numB;
                bool isTheResultFromDividingInt = Math.Ceiling(result) == Math.Floor(result);
                if (!isTheResultFromDividingInt)
                {
                    return InvalidValue;
                }

                return numA / numB;
            }catch (Exception ex)
            {
                return InvalidValue ;
            }
        }
        public void SetBoard(Board pBoard)
        {
            _Board = pBoard;
        }

        public int RowPut { get; private set; } = -1;
        public int ColPut { get; private set; } = -1;


        public event EventHandler PieceBeingPutOnBoard;

        public void BeingPutAt(int Row, int Col)
        {
            RowPut = Row;
            ColPut = Col;
            if (PieceBeingPutOnBoard != null)
            {
                EventArgs evtarg = new EventArgs();

                PieceBeingPutOnBoard(this, evtarg);
            }
        }



        public List<Cell> lstCell = null;
        public PieceType PType { get; } = PieceType.Dot;
        public PieceRotation Rotation { get; } = PieceRotation.Rotate360;

        public bool IsThereEmptyCell
        {
            get
            {
                int i;
                for (i = 0; i < lstCell.Count; i++)
                {
                    if (lstCell[i].Value <= 0)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        private Piece(PieceType pType, PieceRotation pRotation, List<Cell> pListCell)
        {
            this.PType = pType;
            this.Rotation = pRotation;
            this.lstCell = pListCell;
            int i;
            for (i = 0; i < lstCell.Count; i++)
            {
                lstCell[i].SetPiece(this);

            }
        }
        private static HashSet<PieceType> _hshCannotRotate = null;
        private static HashSet<PieceType> hshCannotRotate
        {
            get
            {
                if (_hshCannotRotate == null)
                {
                    _hshCannotRotate = new HashSet<PieceType>();
                    _hshCannotRotate.Add(PieceType.I2);
                    _hshCannotRotate.Add(PieceType.I3);
                    _hshCannotRotate.Add(PieceType.I4);
                    _hshCannotRotate.Add(PieceType.S);
                    _hshCannotRotate.Add(PieceType.Z);

                }
                return _hshCannotRotate;
            }
        }
        public static Piece Create(PieceType pType)
        {
            return Create(pType, PieceRotation.Rotate360);
        }
        public Piece Clone()
        {
            Piece NewPiece = Piece.Create(this.PType, this.Rotation);
            return NewPiece;
        }
        private static Dictionary<String, PieceType> dicPieceType = new Dictionary<string, PieceType>()
        {
            {"Dot",Piece.PieceType.Dot },
            {"I2",Piece.PieceType.I2 },
{"I3",Piece.PieceType.I3 },
{"I4",Piece.PieceType.I4 },
{"J2",Piece.PieceType.J2 },
{"J3",Piece.PieceType.J3 },
{"L2",Piece.PieceType.L2 },
{"L3",Piece.PieceType.L3 },
{"O",Piece.PieceType.O },
{"S",Piece.PieceType.S },
{"T",Piece.PieceType.T },
{"Z",Piece.PieceType.Z },



        };


        private static Dictionary<String, PieceRotation> dicRotation = new Dictionary<string, PieceRotation>()
        {
            {"Rotate360",Piece.PieceRotation.Rotate360},
            {"Rotate270",Piece.PieceRotation.Rotate270 },
            {"Rotate180",Piece.PieceRotation.Rotate180 },
            {"Rotate90",Piece.PieceRotation.Rotate90 }
        };

        public static Piece Create(String pieceTypeString)
        {
            string[] arrTemp = pieceTypeString.Split('_');
            Piece.PieceType pieceType = dicPieceType[arrTemp[0]];
            Piece.PieceRotation pieceRotation = dicRotation[arrTemp[1]];
            return Create(pieceType, pieceRotation);
        }
        public static Piece Create(PieceType pType, PieceRotation pRotation)
        {
            Piece p = null;
            List<Cell> lst = new List<Cell>();

            Dictionary<PieceType, Dictionary<PieceRotation, List<System.Drawing.Point>>> Dic = new Dictionary<PieceType, Dictionary<PieceRotation, List<System.Drawing.Point>>>();


            if (hshCannotRotate.Contains(pType))
            {
                if (pRotation == PieceRotation.Rotate180 ||
                    pRotation == PieceRotation.Rotate270)
                {
                    pRotation = PieceRotation.Rotate360;
                }
            }


            switch (pType)
            {
                case PieceType.Dot:
                    lst.Add(new Cell(0, 0));
                    break;
                case PieceType.I2:

                    if (pRotation == PieceRotation.Rotate180 ||
                        pRotation == PieceRotation.Rotate270)
                    {
                        pRotation = PieceRotation.Rotate360;
                    }
                    switch (pRotation)
                    {
                        case PieceRotation.Rotate360:
                            lst.Add(new Cell(0, 0));
                            lst.Add(new Cell(1, 0));
                            break;
                        case PieceRotation.Rotate90:
                            lst.Add(new Cell(0, 0));
                            lst.Add(new Cell(0, 1));
                            break;
                            /*
                        case  PieceRotation.Rotate180 :
                            throw new Exception(" Piece type does not support 180 Rotation");                            
                        case PieceRotation.Rotate270 :
                            throw new Exception(" Piece type does not support 270 Rotation");                                                        
                            */

                    }
                    break;
                case PieceType.I3:
                    if (pRotation == PieceRotation.Rotate180 ||
                        pRotation == PieceRotation.Rotate270)
                    {
                        pRotation = PieceRotation.Rotate360;
                    }
                    switch (pRotation)
                    {
                        case PieceRotation.Rotate360:
                            lst.Add(new Cell(0, 0));
                            lst.Add(new Cell(1, 0));
                            lst.Add(new Cell(2, 0));

                            break;
                        case PieceRotation.Rotate90:
                            lst.Add(new Cell(0, 0));
                            lst.Add(new Cell(0, 1));
                            lst.Add(new Cell(0, 2));

                            break;

                        case PieceRotation.Rotate180:
                            throw new Exception(" Piece type I3 does not support 180 Rotation");
                        case PieceRotation.Rotate270:
                            throw new Exception(" Piece type I3 does not support 270 Rotation");

                    }
                    break;
                case PieceType.I4:
                    if (pRotation == PieceRotation.Rotate180 ||
                        pRotation == PieceRotation.Rotate270)
                    {
                        pRotation = PieceRotation.Rotate360;
                    }

                    switch (pRotation)
                    {
                        case PieceRotation.Rotate360:
                            lst.Add(new Cell(0, 0));
                            lst.Add(new Cell(1, 0));
                            lst.Add(new Cell(2, 0));
                            lst.Add(new Cell(3, 0));
                            break;
                        case PieceRotation.Rotate90:
                            lst.Add(new Cell(0, 0));
                            lst.Add(new Cell(0, 1));
                            lst.Add(new Cell(0, 2));
                            lst.Add(new Cell(0, 3));
                            break;

                        case PieceRotation.Rotate180:
                            throw new Exception(" Piece type I4 does not support 180 Rotation");
                        case PieceRotation.Rotate270:
                            throw new Exception(" Piece type I4 does not support 270 Rotation");


                    }
                    break;
                case PieceType.L2:
                    switch (pRotation)
                    {
                        case PieceRotation.Rotate360:
                            lst.Add(new Cell(0, 0));
                            lst.Add(new Cell(1, 0));
                            lst.Add(new Cell(1, 1));
                            break;

                        case PieceRotation.Rotate90:
                            lst.Add(new Cell(0, 0));
                            lst.Add(new Cell(0, 1));
                            lst.Add(new Cell(1, 0));
                            break;
                        case PieceRotation.Rotate180:
                            lst.Add(new Cell(0, 0));
                            lst.Add(new Cell(0, 1));
                            lst.Add(new Cell(1, 1));
                            break;
                        case PieceRotation.Rotate270:
                            lst.Add(new Cell(0, 0));
                            lst.Add(new Cell(1, 0));
                            lst.Add(new Cell(1, -1));
                            break;

                    }
                    break;
                case PieceType.L3:
                    switch (pRotation)
                    {
                        case PieceRotation.Rotate360:
                            lst.Add(new Cell(0, 0));
                            lst.Add(new Cell(1, 0));
                            lst.Add(new Cell(2, 0));
                            lst.Add(new Cell(2, 1));
                            break;

                        case PieceRotation.Rotate90:
                            lst.Add(new Cell(0, 0));
                            lst.Add(new Cell(0, 1));
                            lst.Add(new Cell(0, 2));
                            lst.Add(new Cell(1, 0));
                            break;
                        case PieceRotation.Rotate180:
                            lst.Add(new Cell(0, 0));
                            lst.Add(new Cell(0, 1));
                            lst.Add(new Cell(1, 1));
                            lst.Add(new Cell(2, 1));
                            break;
                        case PieceRotation.Rotate270:
                            lst.Add(new Cell(0, 0));
                            lst.Add(new Cell(1, 0));
                            lst.Add(new Cell(1, -1));
                            lst.Add(new Cell(1, -2));
                            break;

                    }
                    break;

                case PieceType.J2:
                    switch (pRotation)
                    {
                        case PieceRotation.Rotate360:
                            lst.Add(new Cell(0, 0));
                            lst.Add(new Cell(1, 0));
                            lst.Add(new Cell(1, -1));
                            break;
                        case PieceRotation.Rotate90:
                            lst.Add(new Cell(0, 0));
                            lst.Add(new Cell(1, 0));
                            lst.Add(new Cell(1, 1));
                            break;
                        case PieceRotation.Rotate180:
                            lst.Add(new Cell(0, 0));
                            lst.Add(new Cell(0, 1));
                            lst.Add(new Cell(1, 1));
                            break;
                        case PieceRotation.Rotate270:
                            lst.Add(new Cell(0, 0));
                            lst.Add(new Cell(0, 1));
                            lst.Add(new Cell(1, 1));
                            break;

                    }
                    break;

                case PieceType.J3:
                    switch (pRotation)
                    {
                        case PieceRotation.Rotate360:
                            lst.Add(new Cell(0, 0));
                            lst.Add(new Cell(1, 0));
                            lst.Add(new Cell(2, 0));
                            lst.Add(new Cell(2, -1));
                            break;
                        case PieceRotation.Rotate90:
                            lst.Add(new Cell(0, 0));
                            lst.Add(new Cell(1, 0));
                            lst.Add(new Cell(1, 1));
                            lst.Add(new Cell(1, 2));
                            break;
                        case PieceRotation.Rotate180:
                            lst.Add(new Cell(0, 0));
                            lst.Add(new Cell(0, 1));
                            lst.Add(new Cell(1, 1));
                            lst.Add(new Cell(2, 1));
                            break;
                        case PieceRotation.Rotate270:
                            lst.Add(new Cell(0, 0));
                            lst.Add(new Cell(0, 1));
                            lst.Add(new Cell(0, 2));
                            lst.Add(new Cell(1, 2));
                            break;

                    }
                    break;
                case PieceType.S:
                    switch (pRotation)
                    {
                        case PieceRotation.Rotate360:
                            lst.Add(new Cell(0, 0));
                            lst.Add(new Cell(0, 1));
                            lst.Add(new Cell(1, -1));
                            lst.Add(new Cell(1, -0));
                            break;
                        case PieceRotation.Rotate90:
                            lst.Add(new Cell(0, 0));
                            lst.Add(new Cell(1, 0));
                            lst.Add(new Cell(1, 1));
                            lst.Add(new Cell(2, 1));
                            break;
                        case PieceRotation.Rotate180:
                            throw new Exception(" Piece type S does not support 180 Rotation");

                        case PieceRotation.Rotate270:
                            throw new Exception(" Piece type S does not support 270 Rotation");


                    }
                    break;

                case PieceType.Z:
                    switch (pRotation)
                    {
                        case PieceRotation.Rotate360:
                            lst.Add(new Cell(0, 0));
                            lst.Add(new Cell(0, 1));
                            lst.Add(new Cell(1, 1));
                            lst.Add(new Cell(1, 2));
                            break;
                        case PieceRotation.Rotate90:
                            lst.Add(new Cell(0, 0));
                            lst.Add(new Cell(1, 0));
                            lst.Add(new Cell(1, -1));
                            lst.Add(new Cell(2, -1));
                            break;
                        case PieceRotation.Rotate180:
                            throw new Exception(" Piece type Z does not support 180 Rotation");

                        case PieceRotation.Rotate270:
                            throw new Exception(" Piece type Z does not support 270 Rotation");


                    }
                    break;
                case PieceType.T:
                    switch (pRotation)
                    {
                        case PieceRotation.Rotate360:
                            lst.Add(new Cell(0, 0));
                            lst.Add(new Cell(0, 1));
                            lst.Add(new Cell(0, 2));
                            lst.Add(new Cell(1, 1));
                            break;
                        case PieceRotation.Rotate90:
                            lst.Add(new Cell(0, 0));
                            lst.Add(new Cell(1, -1));
                            lst.Add(new Cell(1, 0));
                            lst.Add(new Cell(2, 0));

                            break;
                        case PieceRotation.Rotate180:
                            lst.Add(new Cell(0, 0));
                            lst.Add(new Cell(1, -1));
                            lst.Add(new Cell(1, 0));
                            lst.Add(new Cell(1, 1));

                            break;
                        case PieceRotation.Rotate270:
                            lst.Add(new Cell(0, 0));
                            lst.Add(new Cell(1, -1));
                            lst.Add(new Cell(1, 0));
                            lst.Add(new Cell(2, 1));

                            break;

                    }
                    break;
                case PieceType.O:
                    lst.Add(new Cell(0, 0));
                    lst.Add(new Cell(0, 1));
                    lst.Add(new Cell(1, 0));
                    lst.Add(new Cell(1, 1));
                    break;

            }
            //p=new Piece ();
            p = new Piece(pType, pRotation, lst);
            p.lstCell = lst;

            return p;
        }
    }
}
