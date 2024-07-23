using System;
using System.Xml;
using System.Xml.Serialization;

namespace Pip_Boy.Items
{
    /// <summary>
    /// What the non <see cref="Weapon.WeaponType.Melee"/> and <see cref="Weapon.WeaponType.Unarmed"/> <see cref="Weapon"/>s need.
    /// </summary>
    public class Ammo : Equipable
    {
        public readonly AmmoType TypeOfAmmo;
        public readonly AmmoModification Modification;

        #region Constructors
        public Ammo(string name, ushort value, Effect[] effects, AmmoType ammoType, AmmoModification ammoModification) : base(name, 0, value, effects)
        {
            TypeOfAmmo = ammoType;
            Modification = ammoModification;
        }

        /// <summary>
        /// Empty constructor for serialization.
        /// </summary>
        public Ammo() : base() { }
        #endregion

        public static Ammo FromFile(string filePath)
        {
            XmlSerializer x = new(typeof(Ammo));
            XmlReader reader = XmlReader.Create(filePath);
            Ammo? tempItem = (Ammo?)x.Deserialize(reader) ?? throw new NullReferenceException("XMl file object is null!");
            reader.Close();
            return tempItem;
        }

        #region Enums
        /// <summary>
        /// The type of ammo, which determines which <see cref="Weapon"/>s can use it.
        /// </summary>
        public enum AmmoType
        {
            Bullet,
            Bomb,
            EnergyCell,
            Other
        }

        /// <summary>
        /// The modifications on the <see cref="Ammo"/> object, which change damage and <see cref="Effect"/>s.
        /// </summary>
        public enum AmmoModification
        {
            Standard,
            HollowPoint,
            ArmorPiercing,
            HandLoad,
            Special,
            Surplus,
            Explosive,
            Incendiary
        }
        #endregion

        public override string GetIcon() => TypeOfAmmo switch
        {
            AmmoType.Bullet => "🧷",
            AmmoType.Bomb => "🧨",
            AmmoType.EnergyCell => "🔋",
            AmmoType.Other => "?",
            _ => "?"
        };

        /// <returns>An emoji icon for the <see cref="Modification"/></returns>
        public string GetModification() => Modification switch
        {
            AmmoModification.Standard => "",
            AmmoModification.HollowPoint => "⭕",
            AmmoModification.ArmorPiercing => "🛡️",
            AmmoModification.HandLoad => "🤚",
            AmmoModification.Special => "*",
            AmmoModification.Surplus => "+",
            AmmoModification.Explosive => "💥",
            AmmoModification.Incendiary => "🔥",
            _ => "",
        };

        /// <returns>The <see cref="Item.ToString()"/>, with the <see cref="TypeOfAmmo"/> and <see cref="Modification"/></returns>
        public override string ToString() => base.ToString() + $"{Environment.NewLine}\t\tAmmo Type: {TypeOfAmmo}{Environment.NewLine}\t\tAmmo Modification: {Modification}{GetModification()}";
    }
}
