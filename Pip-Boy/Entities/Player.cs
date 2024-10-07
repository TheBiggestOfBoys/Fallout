using Pip_Boy.Data_Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static Pip_Boy.Data_Types.Effect;

namespace Pip_Boy.Entities
{
    /// <summary>
    /// This contains all player behavior.
    /// </summary>
    [Serializable]
    public class Player : Human
    {
        #region Radiation Stuff
        /// <summary>
        /// How much radiation the <see cref="Player"/> has.
        /// </summary>
        public uint Rads { get; private set; } = 0u;

        /// <summary>
        /// How sick the <see cref="Player"/> is, based on <see cref="Rads"/>. 
        /// </summary>
        public RadiationSicknessLevels RadiationSicknessLevel => (RadiationSicknessLevels)Rads;

        /// <summary>
        /// The emoji icon that represents the current <see cref="RadiationSicknessLevel"/>.
        /// </summary>
        public string RadiationIcon => IconDeterminer.Determine(RadiationSicknessLevel);

        /// <summary>
        /// The <see cref="Effect"/>s to apply for each <see cref="RadiationSicknessLevel"/>.
        /// </summary>
        public static readonly Effect[][] RadiationSicknessEffects = [
            [],
            [new(EffectTypes.Endurance, -1, 0)],
            [new(EffectTypes.Endurance, -2, 0), new(EffectTypes.Agility, -1, 0)],
            [new(EffectTypes.Endurance, -3, 0), new(EffectTypes.Agility, -2, 0), new(EffectTypes.Strength, -1, 0)],
            [new(EffectTypes.Endurance, -3, 0), new(EffectTypes.Agility, -2, 0), new(EffectTypes.Strength, -2, 0)],
            [new(EffectTypes.HitPoints, -128, 0)], // Instant Death
        ];

        /// <summary>
        /// The percentage resistance to radiation.
        /// </summary>
        public float RadiationResistance { get; private set; }
        #endregion

        #region Arrays
        /// <summary>
        /// <see cref="List{Perk}"/> of all active <see cref="Perk"/> on the <see cref="Player"/>.
        /// </summary>
        [NonSerialized]
        public List<Perk> Perks = [];
        #endregion

        #region Directories
        /// <summary>
        /// The directory from which files will be loaded and saved
        /// </summary>
        public readonly string activeDirectory;

        /// <summary>
        /// The directory from which <c>Perk</c>s will be loaded and saved
        /// </summary>
        public readonly string perksDirectory;

        /// <summary>
        /// The directory from which <see cref="Objects.Inventory"/> <c>Item</c>s will be loaded and saved.
        /// </summary>
        public readonly string inventoryDirectory;
        #endregion

        #region Constructors
        /// <inheritdoc/>
        public Player() : base()
        {
            activeDirectory = string.Empty;
            perksDirectory = string.Empty;
            inventoryDirectory = string.Empty;
        }

        /// <summary>
        /// Player Creation using code
        /// </summary>
        /// <param name="name">The player's name</param>
        /// <param name="attributeValues">The special values</param>
        /// <param name="directory">The directory to load files from</param>
        public Player(string name, byte[] attributeValues, string directory)
        {
            activeDirectory = directory;
            perksDirectory = activeDirectory + "Perks\\";
            inventoryDirectory = activeDirectory + "Inventory\\";
            Inventory = new(inventoryDirectory, this);
            Name = name;
            Icon = "🕹️";

            byte index = 0;
            foreach (string key in SPECIAL.Keys)
            {
                SPECIAL[key] = attributeValues[index];
                index++;
            }
        }

        /// <summary>
        /// Player creation using console input
        /// </summary>
        public Player(string directory)
        {
            activeDirectory = directory;
            perksDirectory = activeDirectory + "Perks\\";
            inventoryDirectory = activeDirectory + "Inventory\\";
            Inventory = new(inventoryDirectory, this);
            string? tempName = null;
            while (tempName == null)
            {
                Console.Write("Enter Player Name: ");
                tempName = Console.ReadLine();
                Console.Clear();
            }
            Name = tempName.ToString();

            // You have 21 points to disperse across all the SPPECIAL attributes, and each one starts at 1, so 28 total
            byte totalPoints = 28;
            foreach (string attribute in SPECIAL.Keys)
            {
                byte value = 1;

                ConsoleKey key = ConsoleKey.Escape;
                while (key != ConsoleKey.Enter)
                {
                    Console.WriteLine($"Total Points: {totalPoints - value}");
                    Console.WriteLine($"Enter {attribute} value (1 - 10): {value}");
                    key = Console.ReadKey().Key;
                    switch (key)
                    {
                        case ConsoleKey.LeftArrow when value > 1 && value < totalPoints:
                            value--;
                            break;
                        case ConsoleKey.RightArrow when value < 10 && value < totalPoints:
                            value++;
                            break;
                    }
                    Console.Clear();
                }

                totalPoints -= value;
                SPECIAL[attribute] = value;
            }
        }
        #endregion

        /// <summary>
        /// Loads the <see cref="Player"/> and <see cref="Perks"/> from files.
        /// </summary>
        public void LoadPlayerFull()
        {
            // Add logic/function to load player from file
            LoadPlayerPerks();
            // Add logic/function to load quests from file
            // Add logic/function to data entries from file
        }

        #region Perk Stuff
        /// <summary>
        /// Serializes every <c>Perk</c> object in the <c>Perks</c> list to a file in the "Perks" folder.
        /// </summary>
        public void SavePlayerPerks()
        {
            foreach (Perk perk in Perks)
            {
                perk.ToFile(perksDirectory);
            }
        }

        /// <summary>
        /// Deserializes every <c>Perk</c> file in the "Perks" folder list to an object in the <c>Perks</c>.
        /// </summary>
        public void LoadPlayerPerks()
        {
            foreach (string filePath in Directory.GetFiles(activeDirectory + "Perks\\"))
            {
                Perks.Add(Perk.FromFile(filePath));
            }
        }
        #endregion

        /// <summary>
        /// Handles leveling up, and picking a <c>Perk</c>
        /// </summary>
        public void LevelUp()
        {
            Level++;
            if (Level % 2 == 0)
            {
                Perks.Add(new());
            }
        }

        /// <summary>
        /// Shows all the player's perks
        /// </summary>
        /// <returns>A table of every perk's name, level and description</returns>
        public string ShowPerks()
        {
            StringBuilder stringBuilder = new("Perks:");
            stringBuilder.AppendLine();
            foreach (Perk perk in Perks)
            {
                stringBuilder.AppendLine('\t' + perk.ToString());
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Displays the Radiation statuses.
        /// </summary>
        /// <returns>The status screen</returns>
        public string RADScreen()
        {
            StringBuilder stringBuilder = new("RAD");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("RADs: " + Rads);
            stringBuilder.AppendLine($"Radiation Resistance: {RadiationResistance:P}");
            stringBuilder.AppendLine("Radiation Sickness Level: " + RadiationSicknessLevel + '-' + RadiationIcon);
            stringBuilder.AppendLine("Radiation Effects:");
            int radiationEffectIndex = (int)(Rads / RadiationSicknessEffects.Length);
            foreach (Effect effect in RadiationSicknessEffects[radiationEffectIndex])
            {
                stringBuilder.AppendLine('\t' + effect.ToString());
            }
            return stringBuilder.ToString();
        }

        public enum RadiationSicknessLevels : uint
        {
            None = 0u,
            Minor = 200u,
            Advanced = 400u,
            Critical = 600u,
            Deadly = 800u,
            /// <summary>
            /// Instant Death
            /// </summary>
            Fatal = 1000u,
        }
    }
}
