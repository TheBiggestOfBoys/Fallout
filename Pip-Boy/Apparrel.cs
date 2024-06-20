namespace Pip_Boy
{
    internal class Apparrel(string name, double weight, ushort value, Effect[] effects, byte DT, bool powerArmor) : Equippable(name, weight, value, effects)
    {
        private readonly byte originalDamageThreshold = DT;
        public byte DamageThreshold { get; private set; } = DT;
        public bool RequiresPowerArmorTraining = powerArmor;

        public void UpdateDamageThreshold()
        {
            DamageThreshold = (byte)(originalDamageThreshold * Condition);
        }
    }
}
