using Pip_Boy.Data_Types;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Pip_Boy.Items
{
    /// <summary>
    /// A torso armor
    /// </summary>
    public class TorsoPiece : Apparel, ISerializable, IXmlSerializable
    {
        public ArmorType TypeOfArmor => Weight switch
        {
            <= (byte)ArmorType.Clothing => ArmorType.Clothing,
            <= (byte)ArmorType.Light => ArmorType.Light,
            <= (byte)ArmorType.Medium => ArmorType.Medium,
            _ => ArmorType.Heavy,
        };

        #region Constructors
        public TorsoPiece(string name, float weight, ushort value, Effect[] effects, byte DT, bool powerArmor) : base(name, weight, value, effects, DT, powerArmor)
        {
            Icon = TypeOfArmor switch
            {
                ArmorType.Clothing => "👕",
                ArmorType.Light => "🎽",
                ArmorType.Medium => "🧱",
                ArmorType.Heavy => "🛡️",
                _ => "?"
            };
        }

        /// <inheritdoc/>
        public TorsoPiece() : base() { }
        #endregion

        /// <summary>
        /// The type of armor, determined by <see cref="Item.Weight"/>.
        /// </summary>
        public enum ArmorType : byte
        {
            Clothing = 10,
            Light = 25,
            Medium = 30,
            Heavy
        }
    }
}
