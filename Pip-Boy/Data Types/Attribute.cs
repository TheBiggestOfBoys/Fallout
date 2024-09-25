namespace Pip_Boy.Data_Types
{
    public class Attribute(Attribute.AttributeName name, byte value)
    {
        public readonly AttributeName Name = name;
        public byte Value = value;
        public readonly string Icon = IconDeterminer.Determine(name);

        public override string ToString() => $"{Icon} {Name}:\t{Value}";

        public enum AttributeName
        {
            Strength,
            Perception,
            Endurance,
            Charisma,
            Intelligence,
            Agility,
            Luck,
            Barter,
            EnergyWeapons,
            Explosives,
            Gun,
            Lockpick,
            Medicine,
            MeleeWeapons,
            Repair,
            Science,
            Sneak,
            Speech,
            Survival,
            Unarmed,
        }
    }
}
