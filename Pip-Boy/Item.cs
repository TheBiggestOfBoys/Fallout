namespace Pip_Boy
{
    internal readonly struct Item(string name, string description, double weight, int value, Item.Types type)
    {
        public string Name { get; } = name;
        public string Description { get; } = description;

        public Types Type { get; } = type;

        public double Weight { get; } = weight;
        public int Value { get; } = value;

        public enum Types
        {
            WEAPON,
            APPAREL,
            AID,
            AMMO,
            MISC
        }
    }
}
