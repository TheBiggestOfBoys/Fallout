using Pip_Boy.Data_Types;
using Pip_Boy.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using static Pip_Boy.Data_Types.Effect;

namespace Pip_Boy.Entities
{
    /// <summary>
    /// This contains all player behavior.
    /// </summary>
    [DataContract]
    public class Player : Human
    {
        #region Radiation Stuff
        /// <summary>
        /// How much radiation the <see cref="Player"/> has.
        /// </summary>
        [DataMember]
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
        [DataMember]
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
        [DataMember]
        public float RadiationResistance { get; private set; }
        #endregion

        #region Arrays
        /// <summary>
        /// <see cref="List{Perk}"/> of all active <see cref="Perk"/> on the <see cref="Player"/>.
        /// </summary>
        [DataMember]
        public List<Perk> Perks = [];
        #endregion

        #region Directories
        /// <summary>
        /// The directory from which files will be loaded and saved
        /// </summary>
        [DataMember]
        public readonly string activeDirectory;

        /// <summary>
        /// The directory from which <c>Perk</c>s will be loaded and saved
        /// </summary>
        [DataMember]
        public readonly string perksDirectory;

        /// <summary>
        /// The directory from which <see cref="Inventory"/> <see cref="Items.Item"/>s will be loaded and saved.
        /// </summary>
        [DataMember]
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
        public Player(string name, byte[] attributeValues, string directory) : base(name, 1)
        {
            activeDirectory = directory;
            perksDirectory = activeDirectory + "Perks\\";
            inventoryDirectory = activeDirectory + "Inventory\\";
            Inventory = new(inventoryDirectory, this);
            Icon = "🕹️";

            byte index = 0;
            for (int i = 0; i < SPECIAL.Length; i++)
            {
                SPECIAL[i].Value = attributeValues[index];
                index++;
            }

            LoadPlayerPerks();
        }
        #endregion

        #region Perk Stuff
        /// <summary>
        /// Serializes every <c>Perk</c> object in the <c>Perks</c> list to a file in the "Perks" folder.
        /// </summary>
        public void SavePlayerPerks()
        {
            foreach (Perk perk in Perks)
            {
                PipBoy.ToFile(perksDirectory, perk);
            }
        }

        /// <summary>
        /// Deserializes every <c>Perk</c> file in the "Perks" folder list to an object in the <c>Perks</c>.
        /// </summary>
        public void LoadPlayerPerks()
        {
            string[] filePaths = Directory.GetFiles(perksDirectory, "*.xml");
            foreach (string filePath in filePaths)
            {
                Perks.Add(PipBoy.FromFile<Perk>(filePath));
            }
        }

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
        #endregion

        /// <summary>
        /// Player creation using console input
        /// </summary>
        /// <param name="folder">The folder to load data from</param>
        /// <returns>The <see cref="Player"/>.</returns>
        public static Player CreatePlayer(string folder)
        {
            string? tempName = null;
            while (tempName is null)
            {
                Console.Write("Enter Player Name: ");
                tempName = Console.ReadLine();
                Console.Clear();
            }

            // You have 21 points to disperse across all the SPPECIAL attributes, and each one starts at 1, so 28 total
            byte[] attributeValues = new byte[7];
            byte totalPoints = 28;
            int index = 0;

            Data_Types.Attribute.AttributeName[] SPECIALAttributes = (Data_Types.Attribute.AttributeName[])Enum.GetValues(typeof(Data_Types.Attribute.AttributeName));
            SPECIALAttributes = SPECIALAttributes.Take(7).ToArray();
            foreach (Data_Types.Attribute.AttributeName attribute in SPECIALAttributes)
            {
                byte value = 1;

                ConsoleKey key = ConsoleKey.Escape;
                while (key != ConsoleKey.Enter)
                {
                    Console.WriteLine($"Total Points: {totalPoints - value}");
                    Console.WriteLine($"Enter {attribute} {IconDeterminer.Determine(attribute)} value (1 - 10): {value}");
                    key = Console.ReadKey(true).Key;
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
                attributeValues[index] = value;
                index++;
            }
            return new(tempName, attributeValues, folder);
        }

        /// <summary>
        /// Displays the Radiation statuses.
        /// </summary>
        /// <returns>The status screen</returns>
        public string RADScreen()
        {
            StringBuilder stringBuilder = new("RAD\t" + IconDeterminer.RadiationLogo);
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
