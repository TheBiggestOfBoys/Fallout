namespace Pip_Boy.Entities.Creatures
{
    internal class Dog : Canine
    {
        /// <inheritdoc/>
        public Dog() : base() { }

        /// <inheritdoc/>
        public Dog(string name, byte level) : base(name, level)
        {
            Icon = "🐶";
        }
    }
}
