using static Pip_Boy.Ammo;

namespace Pip_Boy
{
    internal class Ammo(string name, ushort value, Effect[] effects, AmmoType ammoType, AmmoModification ammoModification) : Equippable(name, 0, value, effects)
    {
        public readonly AmmoType TypeOfAmmo = ammoType;
        public readonly AmmoModification Modification = ammoModification;

        internal enum AmmoType
        {
            Bullet,
            Bomb,
            EnergyCell,
            Other
        }

        internal enum AmmoModification
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

        public override string ToString() => base.ToString() + $"\n\t\tAmmo Type: {TypeOfAmmo}\n\t\tAmmo Modification: {Modification}";
    }
}
