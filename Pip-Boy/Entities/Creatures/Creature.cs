namespace Pip_Boy.Entities.Creatures
{
    public abstract class Creature : Entity
    {
        /// <inheritdoc/>
        public Creature() : base() { }

        /// <inheritdoc/>
        public Creature(string name, byte level) : base(name, level)
        {
            headPiece = null;
            torsoPiece = null;
            weapon = null;
            ammo = null;
        }
    }
}
