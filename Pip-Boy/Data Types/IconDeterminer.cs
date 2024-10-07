using static Pip_Boy.Data_Types.Attribute;
using static Pip_Boy.Data_Types.Effect;
using static Pip_Boy.Entities.Player;

namespace Pip_Boy.Data_Types
{
    /// <summary>
    /// Determine the emoji icon for various data types.
    /// </summary>
    public static class IconDeterminer
    {
        #region Variables
        #region Attributes
        const string StrengthLogo = "💪";
        const string PerceptionLogo = "👁️";
        const string EnduranceLogo = "🏋️";
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
        const string MedicineLogo = "⚕️";
        const string MeleeWeaponsLogo = "🔪";
        const string RepairLogo = "🔧";
        const string ScienceLogo = "🧪";
        const string SneakLogo = "🥷";
        const string SpeechLogo = "💬";
        const string SurvivalLogo = "⛺";
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
        #region Radiation & Injury
        static readonly string[] RadiationSicknessLogos = ["😐", "🤒", "🤢", "🤮", "🧟", "💀"];
        static readonly string[] InjuryLevelLogos = ["😐", "🤕", "💀"];
        #endregion
        #region Entities
        const string MaleHuman = "👨";
        const string FemaleHuman = "👩";
        #endregion
        const string UnknownLogo = "?";
        #endregion

        #region Determine
        /// <summary>
        /// Determine the emoji logo for the given <see cref="Attribute"/>.
        /// </summary>
        /// <param name="name"><see cref="Attribute"/> name.</param>
        /// <returns>The emoji logo</returns>
        public static string Determine(AttributeName name) => name switch
        {
            // S.P.E.C.I.A.L.
            AttributeName.Strength => StrengthLogo,
            AttributeName.Perception => PerceptionLogo,
            AttributeName.Endurance => EnduranceLogo,
            AttributeName.Charisma => CharismaLogo,
            AttributeName.Intelligence => IntelligenceLogo,
            AttributeName.Agility => AgilityLogo,
            AttributeName.Luck => LuckLogo,

            // Skills
            AttributeName.Barter => BarterLogo,
            AttributeName.EnergyWeapons => EnergyWeaponsLogo,
            AttributeName.Explosives => ExplosiveLogo,
            AttributeName.Gun => GunLogo,
            AttributeName.Lockpick => LockpickLogo,
            AttributeName.Medicine => MedicineLogo,
            AttributeName.MeleeWeapons => MeleeWeaponsLogo,
            AttributeName.Repair => RepairLogo,
            AttributeName.Science => ScienceLogo,
            AttributeName.Sneak => SneakLogo,
            AttributeName.Speech => SpeechLogo,
            AttributeName.Survival => SurvivalLogo,
            AttributeName.Unarmed => UnarmedLogo,

            _ => UnknownLogo,
        };

        /// <summary>
        /// Determine the emoji logo for the given <see cref="EffectTypes"/>.
        /// </summary>
        /// <param name="effectType">The <see cref="Effect.Effector"/> type</param>
        /// <returns>The emoji logo</returns>
        public static string Determine(EffectTypes effectType) => effectType switch
        {
            EffectTypes.Strength => StrengthLogo,
            EffectTypes.Perception => PerceptionLogo,
            EffectTypes.Endurance => EnduranceLogo,
            EffectTypes.Charisma => CharismaLogo,
            EffectTypes.Intelligence => IntelligenceLogo,
            EffectTypes.Agility => AgilityLogo,
            EffectTypes.Luck => LuckLogo,
            EffectTypes.Barter => BarterLogo,
            EffectTypes.EnergyWeapons => EnergyWeaponsLogo,
            EffectTypes.Explosives => ExplosiveLogo,
            EffectTypes.Gun => GunLogo,
            EffectTypes.Lockpick => LockpickLogo,
            EffectTypes.Medicine => MedicineLogo,
            EffectTypes.MeleeWeapons => MeleeWeaponsLogo,
            EffectTypes.Repair => RepairLogo,
            EffectTypes.Science => ScienceLogo,
            EffectTypes.Sneak => SneakLogo,
            EffectTypes.Speech => SpeechLogo,
            EffectTypes.Survival => SurvivalLogo,
            EffectTypes.Unarmed => UnarmedLogo,
            EffectTypes.HitPoints => HitPointsLogo,
            EffectTypes.ActionPoints => ActionPointsLogo,
            EffectTypes.DamageResistance => DamageResistanceLogo,
            _ or EffectTypes.None => UnknownLogo,
        };

        /// <summary>
        /// Determines the emoji logo for the given <see cref="RadiationSicknessLevels"/>.
        /// </summary>
        /// <param name="radiationSicknessLevel">The level of radiation sickness.</param>
        /// <returns>The emoji logo</returns>
        public static string Determine(RadiationSicknessLevels radiationSicknessLevel) => radiationSicknessLevel switch
        {
            RadiationSicknessLevels.Minor => RadiationSicknessLogos[0],
            RadiationSicknessLevels.Advanced => RadiationSicknessLogos[1],
            RadiationSicknessLevels.Critical => RadiationSicknessLogos[2],
            RadiationSicknessLevels.Deadly => RadiationSicknessLogos[3],
            RadiationSicknessLevels.Fatal => RadiationSicknessLogos[4],
            RadiationSicknessLevels.None => RadiationSicknessLogos[5],
            _ => UnknownLogo,
        };

        /// <summary>
        /// Determines the emoji logo for the given gender.
        /// </summary>
        /// <param name="gender"><c>false</c> is male, <c>true</c> is female</param>
        /// <returns>The emoji logo</returns>
        public static char Determine(bool gender) => gender ? '♂' : '♀';
        #endregion
    }
}
