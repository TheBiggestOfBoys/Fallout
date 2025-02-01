﻿using System.Runtime.Serialization;

namespace Pip_Boy.Entities.Mutants
{
	[DataContract]
	public abstract class Mutant : Humanoid
	{
		/// <inheritdoc/>
		public Mutant() : base() { }

		/// <inheritdoc/>
		public Mutant(string name, byte level) : base(name, level) { }
	}
}
