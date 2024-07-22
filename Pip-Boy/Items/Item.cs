using System;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Pip_Boy.Items
{
    [Serializable]
    public abstract class Item
    {
        private readonly Type type;

        public string Name;
        public float Weight;
        public ushort Value;

        #region Constructors
        public Item(string name, float weight, ushort value)
        {
            type = GetType();

            Name = name;
            Weight = weight;
            Value = value;
        }

        public Item()
        {
            type = GetType();

            Name = string.Empty;
            Weight = 0;
            Value = 0;
        }
        #endregion

        public void ToFile(string folderPath)
        {
            XmlSerializer x = new(type);
            XmlWriterSettings xmlWriterSettings = new() { NewLineOnAttributes = true, Indent = true, CloseOutput = true };
            XmlWriter writer = XmlWriter.Create(folderPath + Name + ".xml", xmlWriterSettings);
            writer.WriteProcessingInstruction("xml-stylesheet", "type=\"text/css\" href=\"../Inventory Styling.css\"");
            x.Serialize(writer, this);
            writer.Close();
        }

        public virtual string GetIcon() => "📦";

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
