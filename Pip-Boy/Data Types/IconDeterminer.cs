using static Pip_Boy.Data_Types.Attribute;
using static Pip_Boy.Data_Types.Effect;

namespace Pip_Boy.Data_Types
{
    public static class IconDeterminer
    {
        #region Variables
        #region Attributes
        const string StrengthLogo = "💪";
        const string PerceptionLogo = "👁️";
        const string EnduranceLogo = "";
        const string CharismaLogo = "🗣️";
        const string IntelligenceLogo = "🧠";
        const string AgilityLogo = "🤸";
        const string LuckLogo = "🍀";
        #endregion
        #region Skills
        const string BarterLogo = "🤑";
        const string EnergyWeaponsLogo = "⚡";
        const string ExplosiveLogo = "💥";
        const string GunLogo = "🔫";
        const string LockpickLogo = "🔒";
        const string BMedicineLogo = "⚕️";
        const string MeleeWeaponsLogo = "🔪";
        const string RepairLogo = "🔧";
        const string ScienceLogo = "🧪";
        const string SneakLogo = "🥷";
        const string SpeechLogo = "💬";
        const string SurvivalLogo = "";
        const string UnarmedLogo = "👊";
        #endregion
        #region Effects
        const string HitPointsLogo = "HP";
        const string ActionPointsLogo = "AP";
        const string DamageResistanceLogo = "🛡️";
        const string FireLogo = "🔥";
        const string PoisonLogo = "🤢";
        const string StunLogo = "😵‍💫";

        #endregion
        #endregion

        /// <summary>
        /// Determine the emoji logo for the given <see cref="Attribute"/>.
        /// </summary>
        /// <param name="name"><see cref="Attribute"/> name.</param>
        /// <returns>The emoji logo</returns>
        public static string Determine(AttributeName name) => name switch
        {
            // S.P.E.C.I.A.L.
            AttributeName.Strength => "💪",
            AttributeName.Perception => "👁️",
            AttributeName.Endurance => "",
            AttributeName.Charisma => "🗣️",
            AttributeName.Intelligence => "🧠",
            AttributeName.Agility => "🤸",
            AttributeName.Luck => "🍀",

            // Skills
            AttributeName.Barter => "🤑",
            AttributeName.EnergyWeapons => "⚡",
            AttributeName.Explosives => "💥",
            AttributeName.Gun => "🔫",
            AttributeName.Lockpick => "🔒",
            AttributeName.Medicine => "⚕️",
            AttributeName.MeleeWeapons => "🔪",
            AttributeName.Repair => "🔧",
            AttributeName.Science => "🧪",
            AttributeName.Sneak => "🥷",
            AttributeName.Speech => "💬",
            AttributeName.Survival => "",
            AttributeName.Unarmed => "👊",

            _ => "?"
        };

        /// <summary>
        /// Determine the emoji logo for the given <see cref="EffectTypes"/>.
        /// </summary>
        /// <param name="effectType">The effect type</param>
        /// <returns>The emoji logo</returns>
        public static string Determine(EffectTypes effectType) => effectType switch
        {
            EffectTypes.Strength => "💪",
            EffectTypes.Perception => "👁️",
            EffectTypes.Endurance => "😠",
            EffectTypes.Charisma => "🗣️",
            EffectTypes.Intelligence => "🧠",
            EffectTypes.Agility => "🤸",
            EffectTypes.Luck => "🍀",
            EffectTypes.Barter => "🤑",
            EffectTypes.EnergyWeapons => "⚡",
            EffectTypes.Explosives => "💥",
            EffectTypes.Gun => "🔫",
            EffectTypes.Lockpick => "🔒",
            EffectTypes.Medicine => "⚕️",
            EffectTypes.MeleeWeapons => "🔪",
            EffectTypes.Repair => "🔧",
            EffectTypes.Science => "🧪",
            EffectTypes.Sneak => "🥷",
            EffectTypes.Speech => "💬",
            EffectTypes.Survival => "⛺",
            EffectTypes.Unarmed => "👊",
            EffectTypes.HitPoints => "HP",
            EffectTypes.ActionPoints => "AP",
            EffectTypes.DamageResistance => "🛡️",
            _ => "?"
        };
    }
}
