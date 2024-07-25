using Pip_Boy.Data_Types;
using Pip_Boy.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace Pip_Boy.Items
{
    /// <summary>
    /// Defines behaviors for <see cref="Equipable"/> sub-classes, such as <see cref="Weapon"/>, <see cref="HeadPiece"/>, <see cref="TorsoPiece"/> and <see cref="Ammo"/>.
    /// </summary>
    public abstract class Equipable : Item, ISerializable, IXmlSerializable
    {
        /// <summary>
        /// The original value of the <see cref="Equipable"/>, unaffected by <see cref="Condition"/>.
        /// </summary>
        private ushort originalValue;

        /// <summary>
        /// The percentage of the <see cref="Equipable"/> used to determine <see cref="Weapon.Damage"/>, <see cref="Apparel.DamageThreshold"/> and <see cref="Item.Value"/>.
        /// </summary>
        public float Condition = 1f;

        /// <summary>
        /// If the <see cref="Equipable"/> is equipped to an <see cref="Entity"/>
        /// </summary>
        public bool IsEquipped = false;

        /// <summary>
        /// The effects that the <see cref="Equipable"/> will apply when <see cref="Equip(Entity)"/>'d to an <see cref="Entity"/>.
        /// </summary>
        [XmlArray]
        public readonly List<Effect> Effects;

        #region Constructors
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

        protected Equipable(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            originalValue = info.GetUInt16("originalValue");
        }
        #endregion

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("originalValue", originalValue);
        }

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
        public override string ToString()
        {
            char isEquippedChar = IsEquipped ? '*' : 'O';
            string effectsString = string.Empty;
            foreach (Effect effect in Effects)
            {
                effectsString += $"{Environment.NewLine}\t\t{effect}";
            }
            return isEquippedChar + base.ToString() + effectsString + $"{Environment.NewLine}\t\tCND: {Condition:0.00}";
        }

        public override void ReadXml(XmlReader reader)
        {
            base.ReadXml(reader);
            originalValue = ushort.Parse(reader.ReadElementString("originalValue"));
            Condition = float.Parse(reader.ReadElementString("Condition"));
            IsEquipped = bool.Parse(reader.ReadElementString("IsEquipped"));

            reader.Read();
            if (reader.Name == "Effects")
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "Effect")
                    {
                        // Read the "Effector" and "Value" sub-nodes
                        string? effector = reader.GetAttribute("Effector");
                        string? value = reader.GetAttribute("Value");

                        if (effector is not null && value is not null)
                        {
                            Effect tempEffect = new((Effect.EffectTypes)Enum.Parse(typeof(Effect.EffectTypes), effector), sbyte.Parse(value));
                            Effects.Add(tempEffect);
                        }
                    }
                }
            }
        }

        public override void WriteXml(XmlWriter writer)
        {
            base.WriteXml(writer);
            writer.WriteElementString("originalValue", originalValue.ToString());
            writer.WriteElementString("Condition", Condition.ToString());
            writer.WriteElementString("IsEquipped", IsEquipped.ToString());

            writer.WriteStartElement("Effects");
            foreach (Effect effect in Effects)
            {
                writer.WriteStartElement("Effect");
                writer.WriteElementString("Effector", effect.Effector.ToString());
                writer.WriteElementString("Value", effect.Value.ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }
    }
}
