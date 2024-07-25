﻿using Pip_Boy.Data_Types;
using System;
using System.Xml;
using System.Xml.Serialization;

namespace Pip_Boy.Items
{
    /// <summary>
    /// Aid item which has corresponding effect
    /// </summary>
    public class Aid : Equipable
    {
        public readonly AidType TypeOfAid;

        #region Constructors
        public Aid(string name, float weight, ushort value, Effect[] effects, AidType type) : base(name, weight, value, effects)
        {
            TypeOfAid = type;
            Icon = TypeOfAid switch
            {
                AidType.Food => "🍎",
                AidType.Drink => "🥤",
                AidType.Syringe => "💉",
                AidType.Pill => "💊",
                AidType.Inhale => "🌬",
                AidType.Smoke => "🚬",
                _ => "?"
            };
        }

        /// <inheritdoc/>
        public Aid() : base() { }
        #endregion

        /// <summary>
        /// Deserializes an <c>*.xml</c> file to an <see cref="Aid"/> object.
        /// </summary>
        /// <param name="filePath">The path to the <c>*.xml</c> file.</param>
        public static Aid FromFile(string filePath)
        {
            XmlSerializer x = new(typeof(Aid));
            XmlReader reader = XmlReader.Create(filePath);
            Aid? tempItem = (Aid?)x.Deserialize(reader) ?? throw new NullReferenceException("XMl file object is null!");
            reader.Close();
            return tempItem;
        }

        /// <summary>
        /// The type of aid the <see cref="Aid"/> object is.
        /// </summary>
        public enum AidType
        {
            Food,
            Drink,
            Syringe,
            Pill,
            Inhale,
            Smoke
        }
    }
}
