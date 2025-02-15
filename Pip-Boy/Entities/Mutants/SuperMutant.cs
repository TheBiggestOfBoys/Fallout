﻿using System.Runtime.Serialization;

namespace Pip_Boy.Entities.Mutants
{
	[DataContract]
	public class SuperMutant : Mutant
	{
		/// <inheritdoc/>
		public SuperMutant() : base() { }

		/// <inheritdoc/>
		public SuperMutant(string name, byte level) : base(name, level)
		{
			Icon = "👹";
		}
	}
}
