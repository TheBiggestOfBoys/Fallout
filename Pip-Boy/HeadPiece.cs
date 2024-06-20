namespace Pip_Boy
{
    internal class HeadPiece(string name, double weight, ushort value, Effect[] effects, byte DT, bool powerArmor) : Apparrel(name, weight, value, effects, DT, powerArmor)
    {
        public readonly PieceType pieceType = DetermineType(weight);

        public static PieceType DetermineType(double Weight) => Weight switch
        {
            <= (byte)PieceType.Glasses => PieceType.Glasses,
            <= (byte)PieceType.Hat => PieceType.Hat,
            <= (byte)PieceType.Helmet => PieceType.Helmet,
            _ => PieceType.Other,
        };

        internal enum PieceType : byte
        {
            Glasses = 1,
            Hat = 2,
            Helmet = 7,
            Other
        }
    }
}
