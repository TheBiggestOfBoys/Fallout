namespace Pip_Boy
{
    internal class HeadPiece(string name, string description, double weight, ushort value, string[] effects) : Apparrel(name, description, weight, value, effects)
    {
        public readonly PieceType pieceType = DetermineType(weight);

        public static PieceType DetermineType(double Weight)
        {
            if (Weight <= 0.5)
            {
                return PieceType.Glasses;
            }
            if (Weight <= 1)
            {
                return PieceType.Hat;
            }
            else if (Weight <= 7)
            {
                return PieceType.Helmet;
            }
            else
            {
                return PieceType.Other;
            }
        }

        internal enum PieceType
        {
            Glasses,
            Hat,
            Helmet,
            Other
        }
    }
}
