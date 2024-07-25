namespace Pip_Boy.Entities.Mutants
{
    public class Nightkin : SuperMutant
    {
        /// <inheritdoc/>
        public Nightkin() : base() { }

        /// <inheritdoc/>
        public Nightkin(string name, byte level) : base(name, level)
        {
            Icon = "🥷";
        }
    }
}
