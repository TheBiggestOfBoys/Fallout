using System.Collections.Generic;

namespace Pip_Boy
{
    internal abstract class Equippable(string name, string description, double weight, ushort value, Effect[] effects) : Item(name, description, weight, value)
    {
        public decimal Condition { get; private set; } = 1;
        public bool IsEquipped { get; private set; } = false;
        public List<Effect> Effects = [.. effects];

        public void Equip(Player player)
        {
            IsEquipped = true;
            player.Effects.AddRange(effects);   
        }

        public void Unequip(Player player)
        {
            IsEquipped = false;
            foreach (Effect effect in player.Effects)
            {
                Effects.Remove(effect);
            }
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
