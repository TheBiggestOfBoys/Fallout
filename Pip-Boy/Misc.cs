using System;
using System.IO;
using System.Xml.Serialization;

namespace Pip_Boy
{
    public class Misc : Item
    {
        #region Constructors
        public Misc(string name, double weight, ushort value) : base(name, weight, value)
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
            TextReader reader = new StreamReader(filePath);
            Misc? tempItem = (Misc?)x.Deserialize(reader) ?? throw new NullReferenceException("XMl file object is null!");
            reader.Close();
            return tempItem;
        }
    }
}
