using Pip_Boy.Data_Types;
using System.Runtime.Serialization;
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
