namespace Pip_Boy.Entities.Mutants
{
    public class Ghoul : Mutant
    {
        /// <inheritdoc/>
        public Ghoul() : base() { }

        /// <inheritdoc/>
        public Ghoul(string name, byte level) : base(name, level)
        {
            Icon = "💀";
        }
    }
}
