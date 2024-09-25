using System;
using System.Numerics;
using System.Xml;
using System.Xml.Serialization;

namespace Pip_Boy.Data_Types
{
    /// <summary>
    /// A point of interest on the <see cref="Objects.Map"/>
    /// </summary>
    [Serializable]
    public class Location
    {
        /// <summary>
        /// The <see cref="Location"/>'s name.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// A description of the <see cref="Location"/>.
        /// </summary>
        public readonly string Description;

        /// <summary>
        /// The <see cref="Location"/>'s icon.
        /// </summary>
        public readonly string Icon;

        /// <summary>
        /// How many rads per second the location has.
        /// </summary>
        public readonly byte Rads;

        /// <summary>
        /// Where the <see cref="Location"/> is on the <see cref="Objects.Map"/>.
        /// </summary>
        public readonly Vector2 Position;

        #region Constructors
        /// <summary>
        /// Construct a <see cref="Location"/> manually.
        /// </summary>
        /// <param name="name">The <see cref="Name"/> of the <see cref="Location"/></param>
        /// <param name="description">The <see cref="Description"/> that describe the <see cref="Location"/></param>.
        /// <param name="icon">The <see cref="Icon"/> to represent the <see cref="Location"/> with.</param>
        /// <param name="rads">How radiated the <see cref="Location"/> is.</param>
        /// <param name="position">Where the <see cref="Location"/> is on the <see cref="Objects.Map"/>.</param>
        public Location(string name, string description, string icon, byte rads, Vector2 position)
        {
            Name = name;
            Description = description;
            Icon = icon;
            Rads = rads;
            Position = position;
        }

        /// <summary>
        /// Empty Constructer for serialization.
        /// </summary>
        public Location()
        {
            Name = string.Empty;
            Description = string.Empty;
            Icon = string.Empty;
        }
        #endregion

        #region File Stuff
        /// <summary>
        /// Serializes the <see cref="Location"/> to an <c>*.xml</c> file.
        /// </summary>
        /// <param name="folderPath">The folder to write the file to.</param>
        public void ToFile(string folderPath)
        {
            XmlSerializer x = new(GetType());
            XmlWriter writer = XmlWriter.Create(folderPath + Name + ".xml");
            x.Serialize(writer, this);
            writer.Close();
        }

        /// <summary>
        /// Deserializes an <c>*.xml</c> file to an <see cref="Location"/> object.
        /// </summary>
        /// <param name="filePath">The path to the <c>*.xml</c> file.</param>
        public static Location FromFile(string filePath)
        {
            XmlSerializer x = new(typeof(Location));
            XmlReader reader = XmlReader.Create(filePath);
            Location? tempItem = (Location?)x.Deserialize(reader) ?? throw new NullReferenceException("XMl file object is null!");
            reader.Close();
            return tempItem;
        }
        #endregion

        /// <summary>
        /// A string representation of the <see cref="Location"/>.
        /// </summary>
        /// <returns>A string representation of the <see cref="Location"/>.</returns>
        public override string ToString() => $"{Icon} {Name}:{Environment.NewLine}\t{Description}";
    }
}
