using System.Collections.Generic;

namespace Pip_Boy
{
    internal class Weapon(string name, string description, double weight, ushort value, Effect[] effects, Weapon.WeaponType weaponType, byte strengthRequirement, byte damage, ushort rateOfFire) : Equippable(name, description, weight, value, effects)
    {
        private readonly byte originalDamage = damage;
        public byte Damage { get; private set; } = damage;
        public ushort RateOfFire { get; private set; } = rateOfFire;
        public byte DPS { get; private set; } = (byte)((rateOfFire / 60) * damage);

        public readonly byte StrengthRequirement = strengthRequirement;
        public readonly WeaponType TypeOfWeapon = weaponType;
        public static readonly List<string> Modifications = [];

        public void UpdateDamage()
        {
            Damage = (byte)(originalDamage * Condition);
        }

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
