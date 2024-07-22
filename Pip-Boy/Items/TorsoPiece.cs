namespace Pip_Boy.Items
{
    public class TorsoPiece : Apparrel
    {
        #region Constructors
        public TorsoPiece(string name, float weight, ushort value, Effect[] effects, byte DT, bool powerArmor) : base(name, weight, value, effects, DT, powerArmor) { }
        public TorsoPiece() : base() { }
        #endregion

        public ArmorType TypeOfArmor => Weight switch
        {
            <= (byte)ArmorType.Clothing => ArmorType.Clothing,
            <= (byte)ArmorType.Light => ArmorType.Light,
            <= (byte)ArmorType.Medium => ArmorType.Medium,
            _ => ArmorType.Heavy,
        };

        public enum ArmorType : byte
        {
            Clothing = 10,
            Light = 25,
            Medium = 30,
            Heavy
        }

        public override string GetIcon() => TypeOfArmor switch
        {
            ArmorType.Clothing => "👕",
            ArmorType.Light => "🎽",
            ArmorType.Medium => "🧱",
            ArmorType.Heavy => "🛡️",
            _ => "?"
        };
    }
}
