using System;
using System.Xml;
using System.Xml.Serialization;

namespace Pip_Boy
{
    [Serializable]
    public class Perk
    {
        public readonly string Name;
        public readonly string Description;
        public byte Level;
        public readonly bool IsMultiLevel;

        #region Constructors
        public Perk(string name, string description, byte level, bool multiLevel)
        {
            Name = name;
            Description = description;
            Level = level;
            IsMultiLevel = multiLevel;
        }

        public Perk()
        {
            Name = string.Empty;
            Description = string.Empty;
        }
        #endregion

        #region File Stuff
        public void ToFile(string folderPath)
        {
            XmlSerializer x = new(GetType());
            XmlWriter writer = XmlWriter.Create(folderPath + Name + ".xml");
            x.Serialize(writer, this);
            writer.Close();
        }

        public static Perk FromFile(string filePath)
        {
            XmlSerializer x = new(typeof(Perk));
            XmlReader reader = XmlReader.Create(filePath);
            Perk? tempItem = (Perk?)x.Deserialize(reader) ?? throw new NullReferenceException("XMl file object is null!");
            reader.Close();
            return tempItem;
        }
        #endregion

        public override string ToString() => $"{Name} -- Level:{Level}{Environment.NewLine}\t{Description}";
    }
}
