namespace Pip_Boy
{
    internal struct Perk(string name, string description, byte level)
    {
        public readonly string Name = name;
        public readonly string Description = description;
        public byte Level = level;
    }
}
