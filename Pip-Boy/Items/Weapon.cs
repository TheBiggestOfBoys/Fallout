using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Pip_Boy.Items
{
    /// <summary>
    /// Can be used to attack <see cref="Entities.Entity"/>s.
    /// </summary>
    public class Weapon : Equipable
    {
        /// <summary>
        /// The original damage, unaffected by the <see cref="Weapon"/>'s <see cref="Equipable.Condition"/>.
        /// </summary>
        private readonly byte originalDamage;

        /// <summary>
        /// The varying damage which is just: <code><see cref="originalDamage"/> * <see cref="Equipable.Condition"/></code>
        /// </summary>
        public byte Damage;

        /// <summary>
        /// How many times the weapon can be used per minute.
        /// </summary>
        public ushort RateOfFire;

        /// <summary>
        /// Just: <code><see cref="Damage"/> / 60 * <see cref="Damage"/></code>
        /// </summary>
        public byte DPS;

        /// <summary>
        /// The required 'Strength' level in the SPECIAL attributes to effectively use the <see cref="Weapon"/>.
        /// </summary>
        public readonly byte StrengthRequirement;

        /// <summary>
        /// The required skill level in the skill attributes to effectively use the <see cref="Weapon"/>.
        /// </summary>
        public readonly byte SkillRequirement;
        public readonly WeaponType TypeOfWeapon;

        /// <summary>
        /// All equipped modifications on the <see cref="Weapon"/>.
        /// </summary>
        [XmlArray]
        public static readonly List<string> Modifications = [];

        #region Constructors
        public Weapon(string name, float weight, ushort value, Effect[] effects, WeaponType weaponType, byte strengthRequirement, byte damage, ushort rateOfFire) : base(name, weight, value, effects)
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

        /// <summary>
        /// Updates the <see cref="Damage"/> based on <see cref="Equipable.Condition"/>.
        /// </summary>
        public void UpdateDamage()
        {
            Damage = (byte)(originalDamage * Condition);
        }

        /// <summary>
        /// The type of weapon, which determines what <see cref="Ammo"/> can be used.
        /// </summary>
        public enum WeaponType
        {
            /// <summary>
            /// Most Melee Weapons are completely silent, making them excellent stealth weapons.
            /// </summary>
            Melee,

            /// <summary>
            /// Fisticuffs.
            /// </summary>
            Unarmed,

            /// <summary>
            /// Guns are the most common ranged weapons of the Mojave Wasteland. They are numerous and varied and ammunition is relatively easy to acquire.
            /// </summary>
            Gun,

            /// <summary>
            /// Explosive weapons are best used when dealing with crowds or in situations where precision is not a high priority.
            /// </summary>
            Explosive,

            /// <summary>
            /// Energy weapons are less common and varied than guns, but have a small number of ammunition types and can be quite potent.
            /// </summary>
            Energy
        }

        public override string GetIcon() => TypeOfWeapon switch
        {
            WeaponType.Melee => "⚔️",
            WeaponType.Unarmed => "👊",
            WeaponType.Gun => "🔫",
            WeaponType.Explosive => "💣",
            WeaponType.Energy => "⚡",
            _ => "?",
        };

        /// <returns><see cref="Equipable.ToString()"/> is there are no <see cref="Modifications"/>.  If there are, then add them to the string.</returns>
        public override string ToString()
        {
            if (Modifications.Count == 0)
            {
                return base.ToString();
            }
            else
            {
                string tempString = base.ToString();
                foreach (string modification in Modifications)
                {
                    tempString += $"{Environment.NewLine}\t\t{modification}";
                }
                return tempString;
            }
        }
    }
}
