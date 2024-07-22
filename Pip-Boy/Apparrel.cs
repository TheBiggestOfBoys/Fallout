using System;
using System.IO;
using System.Xml.Serialization;

namespace Pip_Boy
{
    public class Apparrel : Equippable
    {
        private readonly byte originalDamageThreshold;
        public byte DamageThreshold { get; private set; }
        public bool RequiresPowerArmorTraining;

        #region Constructors
        public Apparrel(string name, double weight, ushort value, Effect[] effects, byte DT, bool powerArmor) : base(name, weight, value, effects)
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
            TextReader reader = new StreamReader(filePath);
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
