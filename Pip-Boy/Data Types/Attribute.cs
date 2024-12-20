using System.Runtime.Serialization;

namespace Pip_Boy.Data_Types
{
    [DataContract]
    public class Attribute(Attribute.AttributeName name, byte value)
    {
        /// <summary>
        /// The name of the <see cref="Attribute"/>
        /// </summary>
        [DataMember]
        public readonly AttributeName Name = name;

        /// <summary>
        /// The value of the <see cref="Attribute"/>
        /// </summary>
        [DataMember]
        public byte Value = value;

        /// <summary>
        /// The emoji Icon of the <see cref="Attribute"/>
        /// </summary>
        [DataMember]
        public readonly string Icon = IconDeterminer.Determine(name);

        /// <inheritdoc/>
        /// <returns>The <see cref="Attribute"/>'s info.</returns>
        public override string ToString() => $"{Name}{Icon}:\t{Value}";

        /// <summary>
        /// The possible <see cref="Attribute"/> types.
        /// </summary>
        public enum AttributeName
        {
            Strength,
            Perception,
            Endurance,
            Charisma,
            Intelligence,
            Agility,
            Luck,
            Barter,
            EnergyWeapons,
            Explosives,
            Gun,
            Lockpick,
            Medicine,
            MeleeWeapons,
            Repair,
            Science,
            Sneak,
            Speech,
            Survival,
            Unarmed,
        }
    }
}
