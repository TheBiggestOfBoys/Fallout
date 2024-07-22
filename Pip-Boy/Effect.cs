using System.Text.RegularExpressions;

namespace Pip_Boy
{
    public partial class Effect
    {
        public readonly EffectTypes Effector;
        public readonly sbyte Value;

        [GeneratedRegex("(?<=[a-z])(?=[A-Z])")]
        private static partial Regex MyRegex();

        #region Constructors
        /// <summary>
        /// Empty constructor for serialization
        /// </summary>
        public Effect()
        {
            Effector = EffectTypes.None;
            Value = 0;
        }

        public Effect(EffectTypes effector, sbyte value)
        {
            Effector = effector;
            Value = value;
        }
        #endregion

        public string ToTitleCase()
        {
            // Use a regular expression to split the string at capital letters
            string[] words = MyRegex().Split(Effector.ToString());

            // Join the words with spaces
            return string.Join(' ', words);
        }

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
