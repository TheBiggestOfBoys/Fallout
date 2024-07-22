using System;
using System.Xml;
using System.Xml.Serialization;

namespace Pip_Boy.Items
{
    public class Ammo : Equippable
    {
        public readonly AmmoType TypeOfAmmo;
        public readonly AmmoModification Modification;

        #region Constructors
        public Ammo(string name, ushort value, Effect[] effects, AmmoType ammoType, AmmoModification ammoModification) : base(name, 0, value, effects)
        {
            TypeOfAmmo = ammoType;
            Modification = ammoModification;
        }

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

        public enum AmmoType
        {
            Bullet,
            Bomb,
            EnergyCell,
            Other
        }

        public enum AmmoModification
        {
            Standard,
            HollowPoint,
            ArmorPiercing,
            HandLoad,
            Special,
            Surplus,
            Explosive,
            Incendiary,
        }

        public override string ToString() => base.ToString() + $"{Environment.NewLine}\t\tAmmo Type: {TypeOfAmmo}{Environment.NewLine}\t\tAmmo Modification: {Modification}";
    }
}
