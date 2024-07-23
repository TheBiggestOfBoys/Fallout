using System;
using System.Xml;
using System.Xml.Serialization;

namespace Pip_Boy.Items
{
    /// <summary>
    /// A wearable item, which can reduce damage taken.
    /// </summary>
    public abstract class Apparel : Equipable
    {
        private readonly byte originalDamageThreshold;

        /// <summary>
        /// How much damage the <see cref="Apparel"/> will negate
        /// </summary>
        public byte DamageThreshold;

        /// <summary>
        /// If the <see cref="Apparel"/> object need the "Power Armor Training" <see cref="Perk"/> to be worn.
        /// </summary>
        public bool RequiresPowerArmorTraining;

        #region Constructors
        public Apparel(string name, float weight, ushort value, Effect[] effects, byte DT, bool powerArmor) : base(name, weight, value, effects)
        {
            originalDamageThreshold = DT;
            DamageThreshold = originalDamageThreshold;
            RequiresPowerArmorTraining = powerArmor;
        }

        public Apparel() : base() { }
        #endregion

        public static Apparel FromFile(string filePath)
        {
            XmlSerializer x = new(typeof(Apparel));
            XmlReader reader = XmlReader.Create(filePath);
            Apparel? tempItem = (Apparel?)x.Deserialize(reader) ?? throw new NullReferenceException("XMl file object is null!");
            reader.Close();
            return tempItem;
        }

        /// <summary>
        /// Updates the <see cref="DamageThreshold"/> based on the <see cref="Apparel"/>'s <see cref="Equipable.Condition"/>
        /// </summary>
        public void UpdateDamageThreshold()
        {
            DamageThreshold = (byte)(originalDamageThreshold * Condition);
        }

        public override string GetIcon() => "🛡️";
    }
}
