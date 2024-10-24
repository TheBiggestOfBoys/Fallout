using Pip_Boy.Data_Types;

namespace Pip_Boy.Items
{
    /// <summary>
    /// A Head armor
    /// </summary>
    public class HeadPiece : Apparel
    {
        public PieceType pieceType;

        private static PieceType GetPieceType(float weight) => weight switch
        {
            <= (byte)PieceType.Glasses => PieceType.Glasses,
            <= (byte)PieceType.Hat => PieceType.Hat,
            <= (byte)PieceType.Helmet => PieceType.Helmet,
            _ => PieceType.Other,
        };

        #region Constructors
        public HeadPiece(string name, float weight, ushort value, Effect[] effects, byte DT, bool powerArmor) : base(name, weight, value, effects, DT, powerArmor)
        {
            Icon = IconDeterminer.Determine(pieceType);
            pieceType = GetPieceType(Weight);
        }

        /// <inheritdoc/>
        public HeadPiece() : base() { }
        #endregion

        public enum PieceType : byte
        {
            Glasses = 1,
            Hat = 2,
            Helmet = 7,
            Other
        }
    }
}
