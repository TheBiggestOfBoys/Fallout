using System;
using System.Collections.Generic;

namespace Pip_Boy
{
    public abstract class Equippable : Item
    {
        private readonly ushort originalValue;
        public decimal Condition = 1;
        public bool IsEquipped = false;
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

        public void Equip(Player player)
        {
            IsEquipped = true;
            player.Effects.AddRange(Effects);
            player.ApplyEffects();
        }

        public void Unequip(Player player)
        {
            IsEquipped = false;
            foreach (Effect effect in player.Effects)
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
