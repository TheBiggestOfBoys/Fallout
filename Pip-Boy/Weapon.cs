using System.Collections.Generic;

namespace Pip_Boy
{
    internal class Weapon(string name, string description, double weight, ushort value, Effect[] effects, Weapon.WeaponType weaponType, byte strengthRequirement) : Equippable(name, description, weight, value, effects)
    {
        public readonly byte StrengthRequirement = strengthRequirement;
        public readonly WeaponType TypeOfWeapon = weaponType;
        public List<string> Modifications = [];

        internal enum WeaponType
        {
            Melee,
            Unarmed,
            Gun,
            Explosive,
            Energy
        }

        public override string ToString()
        {
            if (Modifications.Count == 0)
            {
                return base.ToString();
            }
            else
            {
                string tempString = base.ToString();
                foreach (string modificaiton in Modifications)
                {
                    tempString += $"\n\t\t{modificaiton}";
                }
                return tempString;
            }
        }
    }
}
