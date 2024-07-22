using Pip_Boy.Entities;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Pip_Boy
{
    public abstract class Equippable : Item
    {
        private readonly ushort originalValue;
        public float Condition = 1f;
        public bool IsEquipped = false;
        [XmlArray]
        public readonly List<Effect> Effects;

        #region Constructors
        public Equippable(string name, double weight, ushort value, Effect[] effects) : base(name, weight, value)
        {
            originalValue = value;
            Effects = [.. effects];
        }

        public Equippable() : base()
        {
            originalValue = 0;
            Effects = [];
        }
        #endregion

        public void Equip(Entity entity)
        {
            IsEquipped = true;
            entity.Effects.AddRange(Effects);
            entity.ApplyEffects();
        }

        public void Unequip(Entity entity)
        {
            IsEquipped = false;
            foreach (Effect effect in entity.Effects)
            {
                Effects.Remove(effect);
            }
        }

        public void UpdateValue()
        {
            Value = (ushort)(originalValue * Condition);
        }

        public override string ToString()
        {
            char isEquippedChar = IsEquipped ? '*' : 'O';
            string effectsString = string.Empty;
            foreach (Effect effect in Effects)
            {
                effectsString += $"{Environment.NewLine}\t\t{effect}";
            }
            return isEquippedChar + base.ToString() + effectsString + $"{Environment.NewLine}\t\tCND: {Condition:0.00}";
        }
    }
}
