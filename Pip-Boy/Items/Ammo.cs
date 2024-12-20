using Pip_Boy.Data_Types;
using System;
using System.Runtime.Serialization;

namespace Pip_Boy.Items
{
    /// <summary>
    /// What the non <see cref="Weapon.WeaponType.Melee"/> and <see cref="Weapon.WeaponType.Unarmed"/> <see cref="Weapon"/>s need.
    /// </summary>
    [DataContract]
    public class Ammo : Equipable
    {

        /// <summary>
        /// The type of ammo needed for the <see cref="Weapon"/>.
        /// </summary>
        [DataMember]
        public readonly AmmoType TypeOfAmmo;

        /// <summary>
        /// The modification applied to the <see cref="Ammo"/>.
        /// Will change damage and/or damage type.
        /// </summary>
        [DataMember]
        public readonly AmmoModification Modification;

        #region Constructors
        /// <inheritdoc/>
        public Ammo(string name, ushort value, Effect[] effects, AmmoType ammoType, AmmoModification ammoModification) : base(name, 0, value, effects)
        {
            TypeOfAmmo = ammoType;
            Modification = ammoModification;

            Icon = IconDeterminer.Determine(ammoType);
        }

        /// <inheritdoc/>
        public Ammo() : base() { }
        #endregion

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
