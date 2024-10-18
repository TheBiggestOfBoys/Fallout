using Pip_Boy.Data_Types;
using System;
using System.IO;
using System.Xml.Serialization;

namespace Pip_Boy.Items
{
    /// <summary>
    /// A torso armor
    /// </summary>
    public class TorsoPiece : Apparel
    {
        public ArmorType TypeOfArmor;

        private static ArmorType GetArmorType(float weight) => weight switch
        {
            <= (byte)ArmorType.Clothing => ArmorType.Clothing,
            <= (byte)ArmorType.Light => ArmorType.Light,
            <= (byte)ArmorType.Medium => ArmorType.Medium,
            _ => ArmorType.Heavy,
        };

        #region Constructors
        public TorsoPiece(string name, float weight, ushort value, Effect[] effects, byte DT, bool powerArmor) : base(name, weight, value, effects, DT, powerArmor)
        {
            Icon = IconDeterminer.Determine(TypeOfArmor);
            TypeOfArmor = GetArmorType(Weight);
        }

        /// <inheritdoc/>
        public TorsoPiece() : base() { }
        #endregion

        /// <summary>
        /// Deserializes the <see cref="TorsoPiece"/> object from an <c>*.xml</c> file.
        /// </summary>
        /// <typeparam name="T">The <see cref="TorsoPiece"/> sub-class type to serialize to</typeparam>
        /// <param name="filePath">The path to the <c>*.xml</c> file.</param>
        /// <returns>The deserialized <see cref="TorsoPiece"/> object.</returns>
        /// <exception cref="NullReferenceException">If the <c>*.xml</c> file returns a null object.</exception>
        public static TorsoPiece FromFile(string filePath)
        {
            if (Path.GetExtension(filePath) == ".xml")
            {
                XmlSerializer x = new(typeof(TorsoPiece));
                StringReader reader = new(filePath);
                TorsoPiece? tempItem = (TorsoPiece?)x.Deserialize(reader) ?? throw new NullReferenceException("XMl file object is null!");
                reader.Close();
                return tempItem;
            }
            else
            {
                throw new FormatException("File is not '*.xml'!");
            }
        }

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
