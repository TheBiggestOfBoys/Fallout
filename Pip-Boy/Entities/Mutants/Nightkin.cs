using System.Runtime.Serialization;

namespace Pip_Boy.Entities.Mutants
{
    [DataContract]
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
