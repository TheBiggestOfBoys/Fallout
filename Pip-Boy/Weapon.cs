using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Pip_Boy
{
    public class Weapon : Equippable
    {
        private readonly byte originalDamage;
        public byte Damage { get; private set; }
        public ushort RateOfFire { get; private set; }
        public byte DPS { get; private set; }

        public readonly byte StrengthRequirement;
        public readonly WeaponType TypeOfWeapon;
        public static readonly List<string> Modifications = [];

        #region Constructors
        public Weapon(string name, double weight, ushort value, Effect[] effects, WeaponType weaponType, byte strengthRequirement, byte damage, ushort rateOfFire) : base(name, weight, value, effects)
        {
            originalDamage = damage;
            Damage = originalDamage;
            RateOfFire = rateOfFire;
            DPS = (byte)(rateOfFire / 60 * damage);
            StrengthRequirement = strengthRequirement;
            TypeOfWeapon = weaponType;
        }

        public Weapon() : base() { }
        #endregion

        public static Weapon FromFile(string filePath)
        {
            XmlSerializer x = new(typeof(Weapon));
            TextReader reader = new StreamReader(filePath);
            Weapon? tempItem = (Weapon?)x.Deserialize(reader) ?? throw new NullReferenceException("XMl file object is null!");
            reader.Close();
            return tempItem;
        }

        public void UpdateDamage()
        {
            Damage = (byte)(originalDamage * Condition);
        }

        public enum WeaponType
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
                    tempString += $"{Environment.NewLine}\t\t{modificaiton}";
                }
                return tempString;
            }
        }
    }
}
