using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Pip_Boy.Entities
{
    /// <summary>
    /// This contains all player behavior.
    /// </summary>
    [Serializable]
    public class Player : Human
    {
        #region Arrays
        /// <summary>
        /// <see cref="List{T}"/> of all active <see cref="Perk"/> on the <see cref="Player"/>.
        /// </summary>
        [NonSerialized]
        public List<Perk> Perks = [];
        #endregion

        #region Player Info
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

        /// <summary>
        /// The maximum number of Action Points (AP) the <see cref="Player"/> has.
        /// </summary>
        public static byte MaxActionPoints { get; private set; } = 25;

        /// <summary>
        /// The current amount of Action Points (AP) the <see cref="Player"/> has.
        /// </summary>
        public byte ActionPoints { get; private set; } = 25;
        #endregion

        #region Constructors
        /// <summary>
        /// Empty constructor for serialization
        /// </summary>
        public Player()
        {
            Inventory = new();
            activeDirectory = string.Empty;
            perksDirectory = string.Empty;
            inventoryDirectory = string.Empty;
            Name = string.Empty;
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

        #region File Stuff
        /// <summary>
        /// Serializes the <see cref="Player"/> to an <c>*.xml</c> file.
        /// </summary>
        /// <param name="folderPath">The folder to write the file to.</param>
        public void ToFile(string folderPath)
        {
            XmlSerializer x = new(GetType());
            XmlWriter writer = XmlWriter.Create(folderPath + Name + ".xml");
            x.Serialize(writer, this);
            writer.Close();
            SavePlayerPerks();
        }

        /// <summary>
        /// Deserializes an <c>*.xml</c> file to an <see cref="Player"/> object.
        /// </summary>
        /// <param name="filePath">The path to the <c>*.xml</c> file.</param>
        public static Player FromFile(string filePath)
        {
            XmlSerializer x = new(typeof(Player));
            XmlReader reader = XmlReader.Create(filePath);
            Player? tempItem = (Player?)x.Deserialize(reader) ?? throw new NullReferenceException("XMl file object is null!");
            reader.Close();
            return tempItem;
        }
        #endregion

        /// <summary>
        /// Loads the <see cref="Player"/> and <see cref="Perks"/> from files.
        /// </summary>
        public void LoadPlayerFull()
        {
            // Add logic/function to load player from file
            FromFile(activeDirectory);
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

        #region Show Player Info
        /// <summary>
        /// Shows the player's current status
        /// </summary>
        /// <returns>A table of the player's name, level and current health</returns>
        public string ShowStatus()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.AppendLine("Name:\t" + Name);
            stringBuilder.AppendLine("Level:\t" + Level);
            stringBuilder.AppendLine("Health:\t" + CurrentHealth + '/' + MaxHealth);
            stringBuilder.AppendLine("Action Points:\t" + ActionPoints + '/' + MaxActionPoints);

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Shows the player's SPECIAL attributes
        /// </summary>
        /// <returns>A table of all SPECIAL attributes and their values</returns>
        public string ShowSPECIAL()
        {
            StringBuilder stringBuilder = new("S.P.E.C.I.A.L.:");
            foreach (string attribute in SPECIAL.Keys)
            {
                stringBuilder.AppendLine(attribute + ':' + '\t' + SPECIAL[attribute]);
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Shows the player's skill levels
        /// </summary>
        /// <returns>A table with every skill and its associated value</returns>
        public string ShowSkills()
        {
            StringBuilder stringBuilder = new("Skills:");
            foreach (string skill in Skills.Keys)
            {
                stringBuilder.AppendLine('\t' + skill + ':' + '\t' + Skills[skill]);
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Shows all the player's perks
        /// </summary>
        /// <returns>A table of every perk's name, level and description</returns>
        public string ShowPerks()
        {
            StringBuilder stringBuilder = new("Perks:");
            foreach (Perk perk in Perks)
            {
                stringBuilder.AppendLine('\t' + perk.ToString());
            }
            return stringBuilder.ToString();
        }
        #endregion
    }
}
