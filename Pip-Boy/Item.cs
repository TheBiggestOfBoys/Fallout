namespace Pip_Boy
{
    internal readonly struct Item(string name, string description, double weight, int value, Item.ItemTypes type)
    {
        public readonly string Name = name;
        public readonly string Description = description;

        public readonly ItemTypes Type = type;

        public readonly double Weight = weight;
        public readonly int Value = value;

        public enum ItemTypes
        {
            WEAPON,
            APPAREL,
            AID,
            AMMO,
            MISC
        }
    }
}
