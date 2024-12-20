﻿using System.Runtime.Serialization;

namespace Pip_Boy.Entities.Creatures
{
    [DataContract]
    public abstract class Canine : Creature
    {
        /// <inheritdoc/>
        public Canine() : base() { }

        /// <inheritdoc/>
        public Canine(string name, byte level) : base(name, level) { }
    }
}
