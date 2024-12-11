using Pip_Boy.Data_Types;

namespace Pip_Boy.Items
{
    /// <summary>
    /// A torso armor
    /// </summary>
    public class TorsoPiece : Apparel
    {
        #region Constructors
        public TorsoPiece(string name, float weight, ushort value, Effect[] effects, byte DT, bool powerArmor) : base(name, weight, value, effects, DT, powerArmor) { }

        /// <inheritdoc/>
        public TorsoPiece() : base() { }
        #endregion
    }
}
