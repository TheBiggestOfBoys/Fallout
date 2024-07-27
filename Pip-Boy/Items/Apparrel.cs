using Pip_Boy.Data_Types;
using System.Runtime.Serialization;
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
        public byte DamageThreshold { get => (byte)(originalDamageThreshold * Condition); }

        /// <summary>
        /// If the <see cref="Apparel"/> object need the "Power Armor Training" <see cref="Perk"/> to be worn.
        /// </summary>
        public readonly bool RequiresPowerArmorTraining;

        #region Constructors
        public Apparel(string name, float weight, ushort value, Effect[] effects, byte DT, bool powerArmor) : base(name, weight, value, effects)
        {
            originalDamageThreshold = DT;
            RequiresPowerArmorTraining = powerArmor;
        }

        /// <inheritdoc/>
        public Apparel() : base() { }
        #endregion
    }
}
