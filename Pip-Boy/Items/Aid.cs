using System;
using System.Xml;
using System.Xml.Serialization;

namespace Pip_Boy.Items
{
    public class Aid : Equippable
    {
        public readonly AidType TypeOfAid;

        #region Constructors
        public Aid(string name, float weight, ushort value, Effect[] effects) : base(name, weight, value, effects) { }

        public Aid() : base() { }
        #endregion

        public static Aid FromFile(string filePath)
        {
            XmlSerializer x = new(typeof(Aid));
            XmlReader reader = XmlReader.Create(filePath);
            Aid? tempItem = (Aid?)x.Deserialize(reader) ?? throw new NullReferenceException("XMl file object is null!");
            reader.Close();
            return tempItem;
        }

        public override string GetIcon() => TypeOfAid switch
        {
            AidType.Food => "🍎",
            AidType.Drink => "🥤",
            AidType.Syringe => "💉",
            AidType.Pill => "💊",
            AidType.Inhale => "🌬💨",
            AidType.Smoke => "🚬",
            _ => "?",
        };

        public enum AidType
        {
            Food,
            Drink,
            Syringe,
            Pill,
            Inhale,
            Smoke,
        }
    }
}
