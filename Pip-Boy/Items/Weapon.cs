using Pip_Boy.Data_Types;
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
        public byte Damage { get => (byte)(originalDamage * Condition); set { } }

        /// <summary>
        /// How many times the weapon can be used per minute.
        /// </summary>
        public ushort RateOfFire;

        /// <summary>
        /// Average Damage per Second based on <see cref="Damage"/> per shot and <see cref="RateOfFire"/>.
        /// </summary>
        public byte DPS { get { return (byte)(RateOfFire / 60 * Damage); } set { } }

        /// <summary>
        /// The required 'Strength' level in the SPECIAL attributes to effectively use the <see cref="Weapon"/>.
        /// </summary>
        public byte StrengthRequirement;

        /// <summary>
        /// The required skill level in the skill attributes to effectively use the <see cref="Weapon"/>.
        /// </summary>
        public byte SkillRequirement;
        public WeaponType TypeOfWeapon;

        /// <summary>
        /// All equipped modifications on the <see cref="Weapon"/>.
        /// </summary>
        [XmlArray]
        public List<string> Modifications = [];

        #region Constructors
        public Weapon(string name, float weight, ushort value, Effect[] effects, WeaponType weaponType, byte strengthRequirement, byte skillRequirement, byte damage, ushort rateOfFire) : base(name, weight, value, effects)
        {
            TypeOfWeapon = weaponType;
            StrengthRequirement = strengthRequirement;
            SkillRequirement = skillRequirement;
            originalDamage = damage;
            RateOfFire = rateOfFire;

            Icon = TypeOfWeapon switch
            {
                WeaponType.Melee => "⚔️",
                WeaponType.Unarmed => "👊",
                WeaponType.Gun => "🔫",
                WeaponType.Explosive => "💣",
                WeaponType.Energy => "⚡",
                _ => "?",
            };
        }

        /// <inheritdoc/>
        public Weapon() : base() { }
        #endregion

        /// <summary>
        /// Deserializes the <see cref="Weapon"/> object from an <c>*.xml</c> file.
        /// </summary>
        /// <typeparam name="T">The <see cref="Weapon"/> sub-class type to serialize to</typeparam>
        /// <param name="filePath">The path to the <c>*.xml</c> file.</param>
        /// <returns>The deserialized <see cref="Weapon"/> object.</returns>
        /// <exception cref="NullReferenceException">If the <c>*.xml</c> file returns a null object.</exception>
        public static Weapon FromFile(string filePath)
        {
            if (Path.GetExtension(filePath) == ".xml")
            {
                XmlSerializer x = new(typeof(Weapon));
                StringReader reader = new(filePath);
                Weapon? tempItem = (Weapon?)x.Deserialize(reader) ?? throw new NullReferenceException("XMl file object is null!");
                reader.Close();
                return tempItem;
            }
            else
            {
                throw new FormatException("File is not '*.xml'!");
            }
        }

        /// <summary>
        /// The type of weapon, which determines what <see cref="Ammo"/> can be used.
        /// </summary>
        public enum WeaponType
        {
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
            Energy,

            /// <summary>
            /// Most Melee Weapons are completely silent, making them excellent stealth weapons.
            /// </summary>
            Melee,

            /// <summary>
            /// Fisticuffs.
            /// </summary>
            Unarmed
        }

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
