namespace Pip_Boy
{
    internal class TorsoPiece(string name, double weight, ushort value, Effect[] effects, byte DT, bool powerArmor) : Apparrel(name, weight, value, effects, DT, powerArmor)
    {
        public readonly ArmorType TypeOfArmor = DetermineType(weight);

        public static ArmorType DetermineType(double Weight) => Weight switch
        {
            <= (byte)ArmorType.Clothing => ArmorType.Clothing,
            <= (byte)ArmorType.Light => ArmorType.Light,
            <= (byte)ArmorType.Medium => ArmorType.Medium,
            _ => ArmorType.Heavy,
        };

        internal enum ArmorType : byte
        {
            Clothing = 10,
            Light = 25,
            Medium = 30,
            Heavy
        }
    }
}
