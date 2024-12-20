﻿using System.Runtime.Serialization;

namespace Pip_Boy.Entities.Creatures
{
    [DataContract]
    public class NightStalker : Canine
    {
        /// <inheritdoc/>
        public NightStalker() : base() { }

        /// <inheritdoc/>
        public NightStalker(string name, byte level) : base(name, level)
        {
            Icon = "🐺";
        }
    }
}
