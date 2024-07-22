namespace Pip_Boy.Entities
{
    public abstract class Robot : Entity
    {
        public Robot()
        {
            headPiece = null;
            torsoPiece = null;
            weapon = null;
            RadiationRessistance = 1f;
        }
    }
}
