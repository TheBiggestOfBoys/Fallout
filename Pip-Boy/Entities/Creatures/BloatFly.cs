namespace Pip_Boy.Entities.Creatures
{
    internal class BloatFly : Creature
    {
        /// <inheritdoc/>
        public BloatFly() : base() { }

        /// <inheritdoc/>
        public BloatFly(string name, byte level) : base(name, level)
        {
            Icon = "🦟";
        }
    }
}
