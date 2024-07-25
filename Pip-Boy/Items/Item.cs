using System;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Pip_Boy.Items
{
    /// <summary>
    /// Generic super-class for all other <see cref="Objects.Inventory"/> items.
    /// </summary>
    [Serializable]
    public abstract class Item : ISerializable, IXmlSerializable
    {
        private readonly Type type;

        /// <summary>
        /// What the <see cref="Item"/> is called.
        /// </summary>
        public string Name;

        /// <summary>
        /// How much the <see cref="Item"/> weighs.
        /// </summary>
        public float Weight;

        /// <summary>
        /// How much the <see cref="Item"/> costs.
        /// </summary>
        public ushort Value;

        /// <summary>
        /// An emoji representation of the <see cref="Item"/>.
        /// </summary>
        public string Icon;

        #region Constructors
        public Item(string name, float weight, ushort value)
        {
            type = GetType();

            Name = name;
            Weight = weight;
            Value = value;
        }

        /// <summary>
        /// Empty constructor for serialization.
        /// </summary>
        public Item()
        {
            type = GetType();

            Name = string.Empty;
            Weight = 0;
            Value = 0;
        }

        /// <summary>
        /// Custom deserialization constructor
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected Item(SerializationInfo info, StreamingContext context) { }
        #endregion

        #region File Stuff
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context) { }

        /// <summary>
        /// Serializes the <see cref="Item"/> to an <c>*.xml</c> file.
        /// </summary>
        /// <param name="folderPath">The folder to write the <c>*.xml</c> file to.</param>
        public string ToFile(string folderPath)
        {
            string filePath = folderPath + Name + ".xml";
            XmlSerializer x = new(type);
            XmlWriterSettings xmlWriterSettings = new() { NewLineOnAttributes = true, Indent = true, CloseOutput = true };
            XmlWriter writer = XmlWriter.Create(filePath, xmlWriterSettings);
            writer.WriteProcessingInstruction("xml-stylesheet", "type=\"text/css\" href=\"../Inventory Styling.css\"");
            x.Serialize(writer, this);
            writer.Close();
            return filePath;
        }

        /// <summary>
        /// Deserializes the <see cref="Item"/> object from an <c>*.xml</c> file.
        /// </summary>
        /// <typeparam name="T">The <see cref="Item"/> sub-class type to serialize to</typeparam>
        /// <param name="filePath">The path to the <c>*.xml</c> file.</param>
        /// <returns>The deserialized <see cref="Item"/> object.</returns>
        /// <exception cref="NullReferenceException">If the <c>*.xml</c> file returns a null object.</exception>
        public static T FromFile<T>(string filePath)
        {
            XmlSerializer x = new(typeof(T));
            XmlReader reader = XmlReader.Create(filePath);
            T? tempItem = (T?)x.Deserialize(reader) ?? throw new NullReferenceException("XMl file object is null!");
            reader.Close();
            return tempItem;
        }
        #endregion

        /// <returns>The <see cref="Item"/>'s <see cref="Name"/>, <see cref="Icon"/>, <see cref="Value"/> and <see cref="Weight"/>, with logic handling if <see cref="Value"/> or <see cref="Weight"/> are 0</returns>
        public override string ToString()
        {
            StringBuilder defaultHeading = new('\t' + Name + ':' + Icon);
            defaultHeading.AppendLine();
            defaultHeading.Append("\t\tValue: ");
            defaultHeading.Append(Value == 0 ? "--" : Value.ToString());
            defaultHeading.AppendLine();
            defaultHeading.Append("\t\tWeight: ");
            defaultHeading.Append(Weight == 0 ? "--" : Weight.ToString());
            return defaultHeading.ToString();
        }

        public XmlSchema? GetSchema() => null;

        public virtual void ReadXml(XmlReader reader)
        {
            Name = reader.ReadElementString("Name");
            Weight = float.Parse(reader.ReadElementString("Weight"));
            Value = ushort.Parse(reader.ReadElementString("Value"));
            Icon = reader.ReadElementString("Icon");
        }

        public virtual void WriteXml(XmlWriter writer)
        {
            writer.WriteElementString("Name", Name);
            writer.WriteElementString("Weight", Weight.ToString());
            writer.WriteElementString("Value", Value.ToString());
            writer.WriteElementString("Icon", Icon);
        }
    }
}
