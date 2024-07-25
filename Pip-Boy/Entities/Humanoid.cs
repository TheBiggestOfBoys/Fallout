namespace Pip_Boy.Entities
{
    public abstract class Humanoid : Entity
    {
        public ushort Caps;

        /// <inheritdoc/>
        public Humanoid() : base() { }

        /// <inheritdoc/>
        public Humanoid(string name, byte level) : base(name, level)
        {

        }
    }
}
