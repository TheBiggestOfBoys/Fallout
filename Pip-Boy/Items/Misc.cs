using Pip_Boy.Data_Types;
using System;
using System.IO;
using System.Xml.Serialization;

namespace Pip_Boy.Items
{
    /// <summary>
    /// Can be junk, sellable items, or crafting components.
    /// </summary>
    public class Misc : Item
    {
        public MiscType miscType;

        #region Constructors
        public Misc(string name, float weight, ushort value, MiscType type) : base(name, weight, value)
        {
            miscType = type;
            Icon = IconDeterminer.Determine(type);
        }

        /// <inheritdoc/>
        public Misc() : base() { }
        #endregion

        /// <summary>
        /// Deserializes the <see cref="Misc"/> object from an <c>*.xml</c> file.
        /// </summary>
        /// <typeparam name="T">The <see cref="Misc"/> sub-class type to serialize to</typeparam>
        /// <param name="filePath">The path to the <c>*.xml</c> file.</param>
        /// <returns>The deserialized <see cref="Misc"/> object.</returns>
        /// <exception cref="NullReferenceException">If the <c>*.xml</c> file returns a null object.</exception>
        public static Misc FromFile(string filePath)
        {
            if (Path.GetExtension(filePath) == ".xml")
            {
                XmlSerializer x = new(typeof(Misc));
                StringReader reader = new(filePath);
                Misc? tempItem = (Misc?)x.Deserialize(reader) ?? throw new NullReferenceException("XMl file object is null!");
                reader.Close();
                return tempItem;
            }
            else
            {
                throw new FormatException("File is not '*.xml'!");
            }
        }

        /// <summary>
        /// The possible types for the <see cref="Misc"/> object.
        /// </summary>
        public enum MiscType
        {
            /// <summary>
            /// Any other type
            /// </summary>
            Other,

            /// <summary>
            /// Any useless item.
            /// </summary>
            Junk,

            /// <summary>
            /// Any valuable item.
            /// </summary>
            Sellable,

            /// <summary>
            /// Item which can be used to craft another item.
            /// </summary>
            Crafting,

            /// <summary>
            /// A key which can unlock doors
            /// </summary>
            Key,

            /// <summary>
            /// A package which can be delivered
            /// </summary>
            Package
        }
    }
}
