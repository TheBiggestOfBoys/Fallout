﻿using Pip_Boy.Data_Types;
using Pip_Boy.Entities;
using Pip_Boy.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Pip_Boy.Items
{
	/// <summary>
	/// Defines behaviors for <see cref="Equipable"/> sub-classes, such as <see cref="Weapon"/>, <see cref="HeadPiece"/>, <see cref="TorsoPiece"/> and <see cref="Ammo"/>.
	/// </summary>
	[DataContract]
	public abstract class Equipable : Item
	{
		/// <summary>
		/// The original value of the <see cref="Equipable"/>, unaffected by <see cref="Condition"/>.
		/// </summary>
		[DataMember]
		private readonly ushort originalValue;

		/// <summary>
		/// The percentage of the <see cref="Equipable"/> used to determine <see cref="Weapon.Damage"/>, <see cref="Apparel.DamageThreshold"/> and <see cref="Item.Value"/>.
		/// </summary>
		[DataMember]
		public float Condition = 1f;

		/// <summary>
		/// If the <see cref="Equipable"/> is equipped to an <see cref="Entity"/>
		/// </summary>
		[DataMember]
		public bool IsEquipped = false;

		/// <summary>
		/// The effects that the <see cref="Equipable"/> will apply when <see cref="Equip(Entity)"/>'d to an <see cref="Entity"/>.
		/// </summary>
		[DataMember]
		public readonly List<Effect> Effects;

		#region Constructors
		/// <inheritdoc/>
		public Equipable(string name, float weight, ushort value, Effect[] effects) : base(name, weight, value)
		{
			originalValue = value;
			Effects = [.. effects];
		}

		/// <inheritdoc/>
		public Equipable() : base()
		{
			originalValue = 0;
			Effects = [];
		}
		#endregion

		/// <summary>
		/// Equips this <see cref="Equipable"/> to an <see cref="Entity"/>.
		/// </summary>
		/// <param name="entity">The <see cref="Entity"/> to equip the <see cref="Equipable"/> to.</param>
		public void Equip(Entity entity)
		{
			IsEquipped = true;
			entity.Effects.AddRange(Effects);
			entity.ApplyEffects();
		}

		/// <summary>
		/// Unequips this <see cref="Equipable"/> from an <see cref="Entity"/>.
		/// </summary>
		/// <param name="entity">The <see cref="Entity"/> to unequip the <see cref="Equipable"/> from.</param>
		public void Unequip(Entity entity)
		{
			IsEquipped = false;
			foreach (Effect effect in entity.Effects)
			{
				Effects.Remove(effect);
			}
		}

		/// <summary>
		/// Update the value of the <see cref="Equipable"/> based on the <see cref="Condition"/>.
		/// </summary>
		public void UpdateValue()
		{
			Value = (ushort)(originalValue * Condition);
		}

		/// <returns>The (un)equipped character, <see cref="Item.ToString()"/>, <see cref="Effects"/> and <see cref="Condition"/>.</returns>
		public override string ToString() =>
			(IsEquipped ? '*' : 'O')
			+ base.ToString()
			+ ((Effects is null || Effects.Count == 0) ? string.Empty : Environment.NewLine + PipBoy.DisplayCollection(nameof(Effects), Effects) + Environment.NewLine)
			+ "\t\tCND: " + string.Format(Condition.ToString(), "0.00");

		/// <inheritdoc/>
		public override bool Equals(object? obj)
		{
			if (!base.Equals(obj)) return false;
			if (obj is not Equipable other) return false;

			// Compare originalValue, Condition, IsEquipped, and Effects
			bool effectsEqual = (Effects == null && other.Effects == null) ||
								(Effects != null && other.Effects != null && Effects.SequenceEqual(other.Effects));

			return originalValue == other.originalValue
				&& Condition == other.Condition
				&& IsEquipped == other.IsEquipped
				&& effectsEqual;
		}

		/// <inheritdoc/>
		public override int GetHashCode()
		{
			int effectsHash = Effects != null
				? Effects.Aggregate(0, (hash, effect) => HashCode.Combine(hash, effect?.GetHashCode() ?? 0))
				: 0;

			return HashCode.Combine(
				base.GetHashCode(),
				originalValue,
				Condition,
				IsEquipped,
				effectsHash
			);
		}
	}
}
