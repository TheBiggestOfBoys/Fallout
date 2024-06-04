using System.Text.RegularExpressions;

namespace Pip_Boy
{
    internal readonly struct Effect(Effect.EffectTypes effector, sbyte value)
    {
        public readonly EffectTypes Effector = effector;
        public readonly sbyte Value = value;

        public string ToTitleCase()
        {
            // Use a regular expression to split the string at capital letters
            string pattern = @"(?<=[a-z])(?=[A-Z])";
            string[] words = Regex.Split(Effector.ToString(), pattern);

            // Join the words with spaces
            string result = string.Join(" ", words);
            return result;
        }

        internal enum EffectTypes
        {
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
