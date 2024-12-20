using System.Runtime.Serialization;

namespace Pip_Boy.Entities.Mutants
{
    [DataContract]
    public class Feral : Ghoul
    {
        /// <inheritdoc/>
        public Feral() : base() { }

        /// <inheritdoc/>
        public Feral(string name, byte level) : base(name, level)
        {
            Icon = "🧟";
        }
    }
}
