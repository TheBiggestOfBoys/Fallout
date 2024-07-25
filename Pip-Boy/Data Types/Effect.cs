using System.Text.RegularExpressions;

namespace Pip_Boy.Data_Types
{
    /// <summary>
    /// An effect which changes the <see cref="Entities.Player"/>'s attributes.
    /// </summary>
    public partial class Effect
    {
        /// <summary>
        /// The attribute the <see cref="Effect"/> will change.
        /// </summary>
        public readonly EffectTypes Effector;

        /// <summary>
        /// The amount the attribute will be changed by.
        /// </summary>
        public readonly sbyte Value;

        #region Constructors
        /// <summary>
        /// Empty constructor for serialization
        /// </summary>
        public Effect()
        {
            Effector = EffectTypes.None;
            Value = 0;
        }

        /// <summary>
        /// Builds the effect
        /// </summary>
        /// <param name="effector">The effect to change</param>
        /// <param name="value">The amount to change.</param>
        public Effect(EffectTypes effector, sbyte value)
        {
            Effector = effector;
            Value = value;
        }
        #endregion

        /// <summary>
        /// Changes the <see cref="Effector"/> from "TheEffect" to "The Effect".
        /// </summary>
        /// <returns>The modified name.</returns>
        public string ToTitleCase()
        {
            // Use a regular expression to split the string at capital letters
            Regex regex = new("(?<=[a-z])(?=[A-Z])");
            string[] words = regex.Split(Effector.ToString());

            // Join the words with spaces
            return string.Join(' ', words);
        }

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
