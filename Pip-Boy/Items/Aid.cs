using Pip_Boy.Data_Types;
using System;
using System.IO;
using System.Xml.Serialization;

namespace Pip_Boy.Items
{
    /// <summary>
    /// Aid item which has corresponding effect
    /// </summary>
    public class Aid : Equipable
    {
        public readonly AidType TypeOfAid;

        #region Constructors
        public Aid(string name, float weight, ushort value, Effect[] effects, AidType type) : base(name, weight, value, effects)
        {
            TypeOfAid = type;
            Icon = TypeOfAid switch
            {
                AidType.Food => "🍎",
                AidType.Drink => "🥤",
                AidType.Syringe => "💉",
                AidType.Pill => "💊",
                AidType.Inhale => "🌬",
                AidType.Smoke => "🚬",
                _ => "?"
            };
        }

        /// <inheritdoc/>
        public Aid() : base() { }
        #endregion

        /// <summary>
        /// Deserializes the <see cref="Aid"/> object from an <c>*.xml</c> file.
        /// </summary>
        /// <typeparam name="T">The <see cref="Aid"/> sub-class type to serialize to</typeparam>
        /// <param name="filePath">The path to the <c>*.xml</c> file.</param>
        /// <returns>The deserialized <see cref="Aid"/> object.</returns>
        /// <exception cref="NullReferenceException">If the <c>*.xml</c> file returns a null object.</exception>
        public static Aid FromFile(string filePath)
        {
            if (Path.GetExtension(filePath) == ".xml")
            {
                XmlSerializer x = new(typeof(Aid));
                StringReader reader = new(filePath);
                Aid? tempItem = (Aid?)x.Deserialize(reader) ?? throw new NullReferenceException("XMl file object is null!");
                reader.Close();
                return tempItem;
            }
            else
            {
                throw new FormatException("File is not '*.xml'!");
            }
        }

        /// <summary>
        /// The type of aid the <see cref="Aid"/> object is.
        /// </summary>
        public enum AidType
        {
            Food,
            Drink,
            Syringe,
            Pill,
            Inhale,
            Smoke
        }
    }
}
