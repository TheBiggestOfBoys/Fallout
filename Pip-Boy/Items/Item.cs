﻿using System;
using System.Runtime.Serialization;
using System.Text;

namespace Pip_Boy.Items
{
	/// <summary>
	/// Generic super-class for all other <see cref="Objects.Inventory"/> items.
	/// Provides common properties such as name, weight, value, and icon.
	/// </summary>
	[DataContract]
	public abstract class Item
	{
		#region Variable(s)
		/// <summary>
		/// The name of the <see cref="Item"/>.
		/// </summary>
		[DataMember]
		public readonly string Name;

		/// <summary>
		/// The weight of the <see cref="Item"/>.
		/// </summary>
		[DataMember]
		public readonly float Weight;

		/// <summary>
		/// The value (in caps) of the <see cref="Item"/>.
		/// </summary>
		[DataMember]
		public ushort Value;

		/// <summary>
		/// An emoji or icon representation of the <see cref="Item"/>.
		/// </summary>
		[DataMember]
		public string Icon;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Item"/> class with the specified name, weight, and value.
		/// </summary>
		/// <param name="name">The name of the item.</param>
		/// <param name="weight">The weight of the item.</param>
		/// <param name="value">The value (in caps) of the item.</param>
		public Item(string name, float weight, ushort value)
		{
			Name = name;
			Weight = weight;
			Value = value;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Item"/> class for serialization.
		/// Sets default values for all properties.
		/// </summary>
		public Item()
		{
			Name = string.Empty;
			Weight = 0;
			Value = 0;
		}
		#endregion

		#region Method(s)
		#endregion

		#region Enum(s)
		#endregion

		#region Override Functions
		/// <summary>
		/// Returns a string representation of the item, including its name, icon, value, and weight.
		/// If value or weight is zero, displays "--" instead.
		/// </summary>
		/// <returns>
		/// A formatted string with the item's name, icon, value, and weight.
		/// </returns>
		public override string ToString()
		{
			StringBuilder defaultHeading = new('\t' + Name + ':' + Icon + Environment.NewLine);
			defaultHeading.AppendLine("\t\tValue: " + (Value == 0 ? "--" : Value.ToString()));
			defaultHeading.AppendLine("\t\tWeight: " + (Weight == 0 ? "--" : Weight.ToString()));
			return defaultHeading.ToString();
		}

		/// <summary>
		/// Checks if the contents of the 2 Items are the same
		/// </summary>
		/// <returns>If the Items are the same.</returns>
		public override bool Equals(object? obj)
		{
			if (obj is not Item other) return false;
			return Name == other.Name
				&& Weight == other.Weight
				&& Value == other.Value
				&& Icon == other.Icon;
		}

		/// <summary>
		/// Gets a hash code for the item based on its properties.
		/// </summary>
		/// <returns>The int Hash Code</returns>
		public override int GetHashCode()
		{
			return HashCode.Combine(Name, Weight, Value, Icon);
		}
		#endregion
	}
}
