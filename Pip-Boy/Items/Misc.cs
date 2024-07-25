using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Pip_Boy.Items
{
    /// <summary>
    /// Can be junk, sellable items, or crafting components.
    /// </summary>
    public class Misc : Item, ISerializable, IXmlSerializable
    {
        public MiscType miscType;

        #region Constructors
        public Misc(string name, float weight, ushort value, MiscType type) : base(name, weight, value)
        {
            miscType = type;
            Icon = miscType switch
            {
                MiscType.Other => "?",
                MiscType.Junk => "🗑️",
                MiscType.Sellable => "💰",
                MiscType.Crafting => "🔨",
                MiscType.Key => "🔑",
                MiscType.Package => "📦",
                _ => "?"
            };
        }

        /// <inheritdoc/>
        public Misc() : base() { }
        #endregion

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
