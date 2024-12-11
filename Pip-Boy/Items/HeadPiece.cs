using Pip_Boy.Data_Types;
using System.Runtime.Serialization;

namespace Pip_Boy.Items
{
    /// <summary>
    /// A Head armor
    /// </summary>
    public class HeadPiece : Apparel
    {
        #region Constructors
        public HeadPiece(string name, float weight, ushort value, Effect[] effects, byte DT, bool powerArmor) : base(name, weight, value, effects, DT, powerArmor) { }

        /// <inheritdoc/>
        public HeadPiece() : base() { }
        #endregion
    }
}
