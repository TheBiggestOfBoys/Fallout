﻿using System.Runtime.Serialization;

namespace Pip_Boy.Entities
{
	[DataContract]
	public class Human : Humanoid
	{
		/// <inheritdoc/>
		public Human() : base() { }

		/// <inheritdoc/>
		public Human(string name, byte level) : base(name, level)
		{
			Icon = "🧑";
		}
	}
}
