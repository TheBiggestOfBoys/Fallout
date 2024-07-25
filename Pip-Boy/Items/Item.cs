using System;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Pip_Boy.Items
{
    /// <summary>
    /// Generic super-class for all other <see cref="Objects.Inventory"/> items.
    /// </summary>
    [Serializable]
    public abstract class Item
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
        #endregion

        /// <summary>
        /// Serializes the <see cref="Item"/> to an <c>*.xml</c> file.
        /// </summary>
        /// <param name="folderPath">The folder to write the file to.</param>
        public void ToFile(string folderPath)
        {
            XmlSerializer x = new(type);
            XmlWriterSettings xmlWriterSettings = new() { NewLineOnAttributes = true, Indent = true, CloseOutput = true };
            XmlWriter writer = XmlWriter.Create(folderPath + Name + ".xml", xmlWriterSettings);
            writer.WriteProcessingInstruction("xml-stylesheet", "type=\"text/css\" href=\"../Inventory Styling.css\"");
            x.Serialize(writer, this);
            writer.Close();
        }

        /// <returns>The emoji which represents the <see cref="Item"/>.</returns>
        public virtual string GetIcon() => "📦";

        /// <returns>The <see cref="Item"/>'s <see cref="Name"/>, <see cref="GetIcon()"/>, <see cref="Value"/> and <see cref="Weight"/>, with logic handling if <see cref="Value"/> or <see cref="Weight"/> are 0</returns>
        public override string ToString()
        {
            StringBuilder defaultHeading = new('\t' + Name + ':' + GetIcon());
            defaultHeading.AppendLine();
            defaultHeading.Append("\t\tValue: ");
            defaultHeading.Append(Value == 0 ? "--" : Value.ToString());
            defaultHeading.AppendLine();
            defaultHeading.Append("\t\tWeight: ");
            defaultHeading.Append(Weight == 0 ? "--" : Weight.ToString());
            return defaultHeading.ToString();
        }
    }
}
