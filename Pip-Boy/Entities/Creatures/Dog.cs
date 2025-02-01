using System.Runtime.Serialization;

namespace Pip_Boy.Entities.Creatures
{
	[DataContract]
	public class Dog : Canine
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
