using Pip_Boy.Data_Types;
using System;
using System.IO;
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

            Icon = IconDeterminer.Determine(ammoType);
        }

        /// <inheritdoc/>
        public Ammo() : base() { }
        #endregion

        /// <summary>
        /// Deserializes the <see cref="Ammo"/> object from an <c>*.xml</c> file.
        /// </summary>
        /// <typeparam name="T">The <see cref="Ammo"/> sub-class type to serialize to</typeparam>
        /// <param name="filePath">The path to the <c>*.xml</c> file.</param>
        /// <returns>The deserialized <see cref="Ammo"/> object.</returns>
        /// <exception cref="NullReferenceException">If the <c>*.xml</c> file returns a null object.</exception>
        public static Ammo FromFile(string filePath)
        {
            if (Path.GetExtension(filePath) == ".xml")
            {
                XmlSerializer x = new(typeof(Ammo));
                StringReader reader = new(filePath);
                Ammo? tempItem = (Ammo?)x.Deserialize(reader) ?? throw new NullReferenceException("XMl file object is null!");
                reader.Close();
                return tempItem;
            }
            else
            {
                throw new FormatException("File is not '*.xml'!");
            }
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

        /// <returns>The <see cref="Item.ToString()"/>, with the <see cref="TypeOfAmmo"/> and <see cref="Modification"/></returns>
        public override string ToString() => base.ToString() + $"{Environment.NewLine}\t\tAmmo Type: {TypeOfAmmo}{Environment.NewLine}\t\tAmmo Modification: {Modification}{IconDeterminer.Determine(Modification)}";
    }
}
