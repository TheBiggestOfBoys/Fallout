namespace Pip_Boy.Entities
{
    public abstract class Humanoid : Entity
    {
        ushort Caps;

        /// <inheritdoc/>
        public Humanoid() : base() { }

        /// <inheritdoc/>
        public Humanoid(string name, byte level) : base(name, level)
        {

        }
    }
}
