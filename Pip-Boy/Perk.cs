namespace Pip_Boy
{
    internal readonly struct Perk(string name, string description, byte level)
    {
        public readonly string Name = name;
        public readonly string Description = description;
        public readonly byte Level = level;

        public override string ToString()
        {
            return $"{Name} -- Level:{Level}\n \t{Description}";
        }
    }
}
