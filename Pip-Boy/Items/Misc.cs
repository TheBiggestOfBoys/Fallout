using System;
using System.Xml;
using System.Xml.Serialization;

namespace Pip_Boy.Items
{
    public class Misc : Item
    {
        #region Constructors
        public Misc(string name, float weight, ushort value) : base(name, weight, value)
        {
            Name = name;
            Weight = weight;
            Value = value;
        }

        public Misc() : base() { }
        #endregion

        public static Misc FromFile(string filePath)
        {
            XmlSerializer x = new(typeof(Misc));
            XmlReader reader = XmlReader.Create(filePath);
            Misc? tempItem = (Misc?)x.Deserialize(reader) ?? throw new NullReferenceException("XMl file object is null!");
            reader.Close();
            return tempItem;
        }
    }
}
