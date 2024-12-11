using Pip_Boy.Items;
using static Pip_Boy.Data_Types.Attribute;
using static Pip_Boy.Data_Types.Effect;
using static Pip_Boy.Entities.Player;
using static Pip_Boy.Items.Aid;
using static Pip_Boy.Items.Ammo;
using static Pip_Boy.Items.Misc;
using static Pip_Boy.Items.Weapon;

namespace Pip_Boy.Data_Types
{
    /// <summary>
    /// Determine the emoji icon for various data types.
    /// </summary>
    public static class IconDeterminer
    {
        #region Variables
        #region Attributes
        public const string StrengthLogo = "💪";
        public const string PerceptionLogo = "👁️";
        public const string EnduranceLogo = "🏋️";
        public const string CharismaLogo = "🗣️";
        public const string IntelligenceLogo = "🧠";
        public const string AgilityLogo = "🤸";
        public const string LuckLogo = "🍀";
        #endregion
        #region Skills
        public const string BarterLogo = "🤑";
        public const string EnergyWeaponsLogo = "⚡";
        public const string ExplosiveLogo = "💥";
        public const string GunLogo = "🔫";
        public const string LockpickLogo = "🔒";
        public const string MedicineLogo = "⚕️";
        public const string MeleeWeaponsLogo = "🔪";
        public const string RepairLogo = "🔧";
        public const string ScienceLogo = "🧪";
        public const string SneakLogo = "🥷";
        public const string SpeechLogo = "💬";
        public const string SurvivalLogo = "⛺";
        public const string UnarmedLogo = "👊";
        #endregion
        #region Effects
        public const string HitPointsLogo = "HP";
        public const string ActionPointsLogo = "AP";
        public const string DamageResistanceLogo = "🛡️";
        public const string FireLogo = "🔥";
        public const string PoisonLogo = "🤢";
        public const string StunLogo = "😵‍💫";
        #endregion
        #region Radiation & Injury
        public const string RadiationLogo = "☢️";
        public static readonly string[] RadiationSicknessLogos = ["😐", "🤒", "🤢", "🤮", "🧟", "💀"];
        public static readonly string[] InjuryLevelLogos = ["🙂", "😐", "🙁", "🤕", "😵", "💀"];
        #endregion
        #region Entities
        public const string MaleHuman = "👨";
        public const string FemaleHuman = "👩";
        #endregion
        #region Ammo
        #region Types
        public const string BulletLogo = "🧷";
        public const string BombLogo = "🧨";
        public const string EnergyCellLogo = "🔋";
        #endregion
        #region Modifications
        public const string HollowPointLogo = "⭕";
        public const string ArmorPiercingLogo = "🛡️";
        public const string HandLoadLogo = "🤚";
        public const string SpecialLoadLogo = "*";
        public const string SurplusLoadLogo = "+";
        #endregion
        #endregion
        public const string UnknownLogo = "?";
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


        /// <summary>
        /// Determines the emoji logo for the <see cref="AidType"/>.
        /// </summary>
        /// <param name="typeOfAid">The type of <see cref="Items.Aid"/> the item is.</param>
        /// <returns>The emoji logo</returns>
        public static string Determine(AidType typeOfAid) => typeOfAid switch
        {
            AidType.Food => "🍎",
            AidType.Drink => "🥤",
            AidType.Syringe => "💉",
            AidType.Pill => "💊",
            AidType.Inhale => "🌬",
            AidType.Smoke => "🚬",
            _ => UnknownLogo
        };

        public static string Determine(AmmoType ammoType) => ammoType switch
        {
            AmmoType.Bullet => BulletLogo,
            AmmoType.Bomb => BombLogo,
            AmmoType.EnergyCell => EnergyCellLogo,
            _ => UnknownLogo
        };

        public static string Determine(AmmoModification ammoModification) => ammoModification switch
        {
            AmmoModification.Standard => string.Empty,
            AmmoModification.HollowPoint => SpecialLoadLogo,
            AmmoModification.ArmorPiercing => ArmorPiercingLogo,
            AmmoModification.HandLoad => HandLoadLogo,
            AmmoModification.Special => SpecialLoadLogo,
            AmmoModification.Surplus => SurplusLoadLogo,
            AmmoModification.Explosive => ExplosiveLogo,
            AmmoModification.Incendiary => FireLogo,
            _ => UnknownLogo
        };

        public static string Determine(MiscType miscType) => miscType switch
        {
            MiscType.Junk => "🗑️",
            MiscType.Sellable => "💰",
            MiscType.Crafting => "🔨",
            MiscType.Key => "🔑",
            MiscType.Package => "📦",
            MiscType.Other or _ => UnknownLogo
        };

        public static string Determine(Apparel apparel) => apparel.pieceType switch
        {
            Apparel.PieceType.Nothing => apparel is HeadPiece ? "👓" : "👕",
            Apparel.PieceType.Light => apparel is HeadPiece ? "🧢" : "🎽",
            Apparel.PieceType.Heavy => apparel is HeadPiece ? "⛑️" : "🧱",
            _ => UnknownLogo
        };

        public static string Determine(WeaponType weaponType) => weaponType switch
        {
            WeaponType.Melee => MeleeWeaponsLogo,
            WeaponType.Unarmed => UnarmedLogo,
            WeaponType.Gun => GunLogo,
            WeaponType.Explosive => ExplosiveLogo,
            WeaponType.Energy => EnergyWeaponsLogo,
            _ => UnknownLogo
        };
        #endregion
    }
}
