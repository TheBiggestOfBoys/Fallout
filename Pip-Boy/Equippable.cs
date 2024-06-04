using System.Collections.Generic;

namespace Pip_Boy
{
    internal abstract class Equippable(string name, double weight, ushort value, Effect[] effects) : Item(name, weight, value)
    {
        private readonly ushort originalValue = value;
        public decimal Condition { get; private set; } = 1;
        public bool IsEquipped { get; private set; } = false;
        public readonly List<Effect> Effects = [.. effects];

        public void Equip(Player player)
        {
            IsEquipped = true;
            player.Effects.AddRange(effects);
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
                effectsString += $"\n\t\t{effect}";
            }
            return isEquippedChar + base.ToString() + effectsString + $"\n\t\tCND: {Condition:0.00}";
        }
    }
}
