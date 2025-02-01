using System;
using System.Runtime.Serialization;
using System.Text;

namespace Pip_Boy.Items
{
	/// <summary>
	/// Generic super-class for all other <see cref="Objects.Inventory"/> items.
	/// </summary>
	[DataContract]
	public abstract class Item
	{
		#region Variables
		/// <summary>
		/// What the <see cref="Item"/> is called.
		/// </summary>
		[DataMember]
		public readonly string Name;

		/// <summary>
		/// How much the <see cref="Item"/> weighs.
		/// </summary>
		[DataMember]
		public readonly float Weight;

		/// <summary>
		/// How much the <see cref="Item"/> costs.
		/// </summary>
		[DataMember]
		public ushort Value;

		/// <summary>
		/// An emoji representation of the <see cref="Item"/>.
		/// </summary>
		[DataMember]
		public string Icon;
		#endregion

		#region Constructors
		/// <summary>
		/// Constructs an new <see cref="Item"/> from the given values
		/// </summary>
		public Item(string name, float weight, ushort value)
		{
			Name = name;
			Weight = weight;
			Value = value;
		}

		/// <summary>
		/// Empty constructor for serialization.
		/// </summary>
		public Item()
		{
			Name = string.Empty;
			Weight = 0;
			Value = 0;
		}
		#endregion

		/// <returns>The <see cref="Item"/>'s <see cref="Name"/>, <see cref="Icon"/>, <see cref="Value"/> and <see cref="Weight"/>, with logic handling if <see cref="Value"/> or <see cref="Weight"/> are 0</returns>
		public override string ToString()
		{
			StringBuilder defaultHeading = new('\t' + Name + ':' + Icon + Environment.NewLine);
			defaultHeading.AppendLine("\t\tValue: " + (Value == 0 ? "--" : Value.ToString()));
			defaultHeading.AppendLine("\t\tWeight: " + (Weight == 0 ? "--" : Weight.ToString()));
			return defaultHeading.ToString();
		}
	}
}
