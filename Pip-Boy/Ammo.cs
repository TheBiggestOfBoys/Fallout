using System;
using System.IO;
using System.Xml.Serialization;

namespace Pip_Boy
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
            TextReader reader = new StreamReader(filePath);
            Ammo? tempItem = (Ammo?)x.Deserialize(reader) ?? throw new NullReferenceException("XMl file object is null!");
            reader.Close();
            return tempItem;
        }

        #region Enums
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

        public override string ToString() => base.ToString() + $"{Environment.NewLine}\t\tAmmo Type: {TypeOfAmmo}{Environment.NewLine}\t\tAmmo Modification: {Modification}{GetModification()}";
    }
}
