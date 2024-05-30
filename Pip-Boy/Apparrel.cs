namespace Pip_Boy
{
    internal class Apparrel(string name, string description, double weight, ushort value, Effect[] effects, byte DT) : Equippable(name, description, weight, value, effects)
    {
        private readonly byte originalDamageThreshold = DT;
        public byte DamageThreshold { get; private set; } = DT;

        public void UpdateDamageThreshold()
        {
            DamageThreshold = (byte)(originalDamageThreshold * Condition);
        }
    }
}
