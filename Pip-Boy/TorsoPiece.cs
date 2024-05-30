namespace Pip_Boy
{
    internal class TorsoPiece(string name, string description, double weight, ushort value, string[] effects) : Apparrel(name, description, weight, value, effects)
    {
        public ArmorType TypeOfArmor = DetermineType(weight);
        public bool RequiresPowerArmorTraining = false;

        public static ArmorType DetermineType(double Weight)
        {
            if (Weight <= 10)
            {
                return ArmorType.Clothing;
            }
            else if (Weight <= 25)
            {
                return ArmorType.Light;
            }
            else if (Weight <= 30)
            {
                return ArmorType.Medium;
            }
            else
            {
                return ArmorType.Heavy;
            }
        }

        internal enum ArmorType
        {
            Clothing,
            Light,
            Medium,
            Heavy
        }
    }
}
