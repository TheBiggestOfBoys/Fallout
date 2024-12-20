using System.Runtime.Serialization;

namespace Pip_Boy.Data_Types
{
    /// <summary>
    /// An effect which changes the <see cref="Entities.Player"/>'s attributes.
    /// </summary>
    [DataContract]
    public partial class Effect
    {
        /// <summary>
        /// The attribute the <see cref="Effect"/> will change.
        /// </summary>
        [DataMember]
        public readonly EffectTypes Effector;

        /// <summary>
        /// The amount the attribute will be changed by.
        /// </summary>
        [DataMember]
        public readonly sbyte Value;

        /// <summary>
        /// The logo for the effect.
        /// </summary>
        [DataMember]
        public readonly string Icon;

        /// <summary>
        /// How long the effect will last for. -1 represent an effect which lasts forever (as long as the item is equipped).
        /// </summary>
        [DataMember]
        public readonly short Duration;

        /// <summary>
        /// How much time the effect has left
        /// </summary>
        [DataMember]
        public short TimeLeft;

        #region Constructors
        /// <summary>
        /// Empty constructor for serialization
        /// </summary>
        public Effect()
        {
            Effector = EffectTypes.None;
            Icon = string.Empty;
        }

        /// <summary>
        /// Builds the effect
        /// </summary>
        /// <param name="effector">The effect to change</param>
        /// <param name="value">The amount to change.</param>
        /// <param name="duration">How long the effect will last.</param>
        public Effect(EffectTypes effector, sbyte value, short duration)
        {
            Effector = effector;
            Value = value;
            Icon = IconDeterminer.Determine(effector);
            Duration = duration;
            TimeLeft = Duration;
        }
        #endregion

        /// <summary>
        /// A string representation of the <see cref="Effect"/>.
        /// </summary>
        /// <returns>A string representation of the <see cref="Effect"/>.</returns>
        public override string ToString() => $"{Icon} {Effector}: {TimeLeft}";

        /// <summary>
        /// The possible attributes to change.
        /// </summary>
        public enum EffectTypes
        {
            None,
            #region SPECIAL
            Strength,
            Perception,
            Endurance,
            Charisma,
            Intelligence,
            Agility,
            Luck,
            #endregion
            #region Skills
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
            #endregion
            #region Player
            HitPoints,
            ActionPoints,
            DamageResistance,
            #endregion
        }
    }
}
