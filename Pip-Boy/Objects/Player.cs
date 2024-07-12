using Pip_Boy.Items;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Pip_Boy.Objects
{
    [Serializable]
    public class Player
    {
        #region Arrays
        [NonSerialized]
        public Inventory Inventory = new(activeDirectory + "Inventory\\");

        public static Dictionary<string, byte> SPECIAL = new()
        {
            {"Strength", 5},
            {"Perception", 5},
            {"Endurance", 5},
            {"Charisma", 5},
            {"Intelligence", 5},
            {"Agility", 5},
            {"Luck", 5}
        };
        [NonSerialized]
        private readonly Dictionary<string, byte> baseSPECIAL = SPECIAL;

        public static Dictionary<string, byte> Skills = new(){
            {"Barter", 10},
            {"Energy Weapons", 10},
            { "Explosives", 10},
            { "Gun", 10},
            { "Lockpick", 10},
            { "Medicine", 10},
            { "Melee Weapons", 10},
            { "Repair", 10},
            { "Science", 10},
            { "Sneak", 10},
            { "Speech", 10},
            { "Survival", 10},
            { "Unarmed", 10}
        };
        [NonSerialized]
        private readonly Dictionary<string, byte> baseSkills = Skills;

        [NonSerialized]
        public List<Perk> Perks = [];

        public List<Effect> Effects = [];
        #endregion

        #region Player Info
        /// <summary>
        /// The directory from which files will be loaded and saved
        /// </summary>
        public static string activeDirectory;
        public readonly string Name;
        public byte Level { get; private set; } = 1;
        public static ushort MaxHealth { get; private set; } = 100;
        [NonSerialized]
        private readonly ushort baseMaxHealth = MaxHealth;
        public int CurrentHealth { get; private set; } = 100;

        public static byte MaxActionPoints { get; private set; } = 25;
        [NonSerialized]
        private readonly byte baseMaxActionPoints = MaxActionPoints;
        public byte ActionPoints { get; private set; } = 25;

        public static byte DamageRessistance { get; private set; } = 0;
        [NonSerialized]
        private readonly byte baseDamageRessistance = DamageRessistance;
        #endregion

        #region EquippedItems
        public HeadPiece? headPiece;
        public TorsoPiece? torsoPiece;
        public Weapon? weapon;
        public Ammo? ammo;
        #endregion

        #region Constructors
        /// <summary>
        /// Player Creation using code
        /// </summary>
        /// <param name="name">The player's name</param>
        /// <param name="attributeValues">The special values</param>
        public Player(string name, byte[] attributeValues, string directory)
        {
            activeDirectory = directory;
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
            while (Name == null)
            {
                Console.Write("Enter Player Name: ");
                Name = Console.ReadLine();
                Console.Clear();
            }

            // You have 21 points to disperse across all the SPPECIAL attributes, and each one starts at 1, so 28 total
            byte totalPoints = 28;
            foreach (string attribute in SPECIAL.Keys)
            {
                byte value = 1;

                ConsoleKey key = ConsoleKey.Escape;
                while (key != ConsoleKey.Enter)
                {
                    Console.WriteLine($"Total Points: {totalPoints - value}");
                    Console.WriteLine($"Enter {attribute} value (1 - 10): {SPECIAL[attribute]}");
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
        public void ToFile(string folderPath)
        {
            XmlSerializer x = new(GetType());
            TextWriter writer = new StreamWriter(folderPath + Name + '.' + GetType().Name);
            x.Serialize(writer, this);
            writer.Close();
            SavePlayerPerks();
        }

        public static Player FromFile(string filePath)
        {
            XmlSerializer x = new(typeof(Player));
            TextReader reader = new StreamReader(filePath);
            Player? tempItem = (Player?)x.Deserialize(reader) ?? throw new NullReferenceException("XMl file object is null!");
            reader.Close();
            return tempItem;
        }
        #endregion

        public void LoadPlayerFull()
        {
            // Add logic/function to load player from file
            FromFile(activeDirectory);
            LoadPlayerPerks();
            // Add logic/function to load quests from file
            // Add logic/function to data entries from file
        }

        #region Perk Stuff
        public void SavePlayerPerks()
        {
            foreach (Perk perk in Perks)
            {
                perk.ToFile(activeDirectory + "Perks\\");
            }
        }

        public void LoadPlayerPerks()
        {
            foreach (string filePath in Directory.GetFiles(activeDirectory + "Perks\\"))
            {
                Perks.Add(Perk.FromFile(filePath));
            }
        }
        #endregion

        public void LevelUp()
        {
            Level++;
            if (Level % 2 == 0)
            {
                Perks.Add(new());
            }
        }

        #region Items
        public void Equip(Equippable item)
        {
            if (item is HeadPiece headPieceItem)
            {
                headPiece = headPieceItem;
            }
            else if (item is TorsoPiece torsoPieceItem)
            {
                torsoPiece = torsoPieceItem;
            }
            else if (item is Weapon weaponItem)
            {
                weapon = weaponItem;
            }
            else if (item is Ammo ammoItem)
            {
                ammo = ammoItem;
            }
            item.Equip(this);
        }

        public void Unequip(Equippable item)
        {
            if (item is not null)
            {
                item.Unequip(this);
                if (item is HeadPiece)
                {
                    headPiece = null;
                }
                else if (item is TorsoPiece)
                {
                    torsoPiece = null;
                }
                else if (item is Weapon)
                {
                    weapon = null;
                }
                else if (item is Ammo)
                {
                    ammo = null;
                }
            }
        }
        #endregion

        #region Effects
        public void ApplyEffects()
        {
            ResetEffects();
            foreach (Effect effect in Effects)
            {
                foreach (string attribute in SPECIAL.Keys)
                {
                    if (effect.ToTitleCase() == attribute)
                    {
                        if (SPECIAL[attribute] + effect.Value >= 1)
                        {
                            SPECIAL[attribute] = (byte)(SPECIAL[attribute] + effect.Value);
                        }
                        break;
                    }
                }
                foreach (string attribute in Skills.Keys)
                {
                    if (effect.ToTitleCase() == attribute)
                    {
                        if (Skills[attribute] + effect.Value >= 1)
                        {
                            Skills[attribute] = (byte)(Skills[attribute] + effect.Value);
                        }
                        break;
                    }
                }
            }
        }

        public void ResetEffects()
        {
            SPECIAL = baseSPECIAL;
            Skills = baseSkills;
            MaxActionPoints = baseMaxActionPoints;
            MaxHealth = baseMaxHealth;
            DamageRessistance = baseDamageRessistance;
            Effects.Clear();
        }
        #endregion

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
        public static string ShowSPECIAL()
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
        public static string ShowSkills()
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
        public string ShowPeks()
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
