namespace Pip_Boy.Items
{
    /// <summary>
    /// A Head armor
    /// </summary>
    public class HeadPiece : Apparel
    {
        #region Constructors
        public HeadPiece(string name, float weight, ushort value, Effect[] effects, byte DT, bool powerArmor) : base(name, weight, value, effects, DT, powerArmor) { }

        public HeadPiece() : base() { }
        #endregion

        public PieceType pieceType => Weight switch
        {
            <= (byte)PieceType.Glasses => PieceType.Glasses,
            <= (byte)PieceType.Hat => PieceType.Hat,
            <= (byte)PieceType.Helmet => PieceType.Helmet,
            _ => PieceType.Other,
        };

        public enum PieceType : byte
        {
            Glasses = 1,
            Hat = 2,
            Helmet = 7,
            Other
        }

        public override string GetIcon() => pieceType switch
        {
            PieceType.Glasses => "👓",
            PieceType.Hat => "🧢",
            PieceType.Helmet => "⛑️",
            PieceType.Other => "?",
            _ => "?"
        };
    }
}
