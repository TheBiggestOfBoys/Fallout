namespace Pip_Boy.Entities.Creatures
{
    internal class DeathClaw : Creature
    {
        /// <inheritdoc/>
        public DeathClaw() : base() { }

        /// <inheritdoc/>
        public DeathClaw(string name, byte level) : base(name, level)
        {
            Icon = "😈";
        }
    }
}
