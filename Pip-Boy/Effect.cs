using System.Text.RegularExpressions;

namespace Pip_Boy
{
    public readonly partial struct Effect(Effect.EffectTypes effector, sbyte value)
    {
        public readonly EffectTypes Effector = effector;
        public readonly sbyte Value = value;

        [GeneratedRegex("(?<=[a-z])(?=[A-Z])")]
        private static partial Regex MyRegex();

        public string ToTitleCase()
        {
            // Use a regular expression to split the string at capital letters
            string[] words = MyRegex().Split(Effector.ToString());

            // Join the words with spaces
            return string.Join(' ', words);
        }

        public enum EffectTypes
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
