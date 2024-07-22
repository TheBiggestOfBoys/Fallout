using System;
using System.Xml;
using System.Xml.Serialization;

namespace Pip_Boy.Items
{
    public class Apparrel : Equippable
    {
        private readonly byte originalDamageThreshold;
        public byte DamageThreshold;
        public bool RequiresPowerArmorTraining;

        #region Constructors
        public Apparrel(string name, float weight, ushort value, Effect[] effects, byte DT, bool powerArmor) : base(name, weight, value, effects)
        {
            originalDamageThreshold = DT;
            DamageThreshold = originalDamageThreshold;
            RequiresPowerArmorTraining = powerArmor;
        }

        public Apparrel() : base() { }
        #endregion

        public static Apparrel FromFile(string filePath)
        {
            XmlSerializer x = new(typeof(Apparrel));
            XmlReader reader = XmlReader.Create(filePath);
            Apparrel? tempItem = (Apparrel?)x.Deserialize(reader) ?? throw new NullReferenceException("XMl file object is null!");
            reader.Close();
            return tempItem;
        }

        public void UpdateDamageThreshold()
        {
            DamageThreshold = (byte)(originalDamageThreshold * Condition);
        }
    }
}
