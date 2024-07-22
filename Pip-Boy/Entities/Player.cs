using System;
using System.Collections.Generic;
using System.Text;

<<<<<<<< HEAD:Pip-Boy/Player.cs
namespace Pip_Boy
{
    public class Player
========
namespace Pip_Boy.Entities
{
    [Serializable]
    public class Player : Human
>>>>>>>> serialization-changes:Pip-Boy/Entities/Player.cs
    {
        #region Arrays
<<<<<<< HEAD:Pip-Boy/Player.cs
        public Inventory Inventory = new("C:\\Users\\jrsco\\source\\repos\\Pip-Boy\\Pip-Boy\\PIP-Boy\\Inventory\\");
=======
        [NonSerialized]
<<<<<<<< HEAD:Pip-Boy/Player.cs
        public Inventory Inventory = new(activeDirectory + "Inventory\\");
>>>>>>> parent of 0a5484b (replaced structs with classes, and fixed pathing errors):Pip-Boy/Objects/Player.cs

        public static Attribute[] SPECIAL = [
            new("Strength", 5),
            new("Perception", 5),
            new("Endurance", 5),
            new("Charisma", 5),
            new("Intelligence", 5),
            new("Agility", 5),
            new("Luck", 5)
        ];
        private readonly Attribute[] baseSPECIAL = SPECIAL;

<<<<<<< HEAD:Pip-Boy/Player.cs
        public static Attribute[] Skills = [
            new("Barter", 10),
            new("Energy Weapons", 10),
            new("Explosives", 10),
            new("Gun", 10),
            new("Lockpick", 10),
            new("Medicine", 10),
            new("Melee Weapons", 10),
            new("Repair", 10),
            new("Science", 10),
            new("Sneak", 10),
            new("Speech", 10),
            new("Survival", 10),
            new("Unarmed", 10)
        ];
        private readonly Attribute[] baseSkills = Skills;
=======
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
>>>>>>> parent of 0a5484b (replaced structs with classes, and fixed pathing errors):Pip-Boy/Objects/Player.cs

        public List<Perk> Perks = [new("No Perks", "You have no perks, you get one every 2 levels", 0)];

        public List<Effect> Effects = [];
========
        public List<Perk> Perks = [];
>>>>>>>> serialization-changes:Pip-Boy/Entities/Player.cs
        #endregion

        #region Player Info
<<<<<<< HEAD:Pip-Boy/Player.cs
=======
        /// <summary>
        /// The directory from which files will be loaded and saved
        /// </summary>
<<<<<<<< HEAD:Pip-Boy/Player.cs
        public static string activeDirectory;
>>>>>>> parent of 0a5484b (replaced structs with classes, and fixed pathing errors):Pip-Boy/Objects/Player.cs
        public readonly string Name;
        public byte Level { get; private set; } = 1;
        public static ushort MaxHealth { get; private set; } = 100;
        private readonly ushort baseMaxHealth = MaxHealth;
        public int CurrentHealth { get; private set; } = 100;
========
        public readonly string activeDirectory;

        /// <summary>
        /// The directory from which <c>Perk</c>s will be loaded and saved
        /// </summary>
        public readonly string perksDirectory;

        /// <summary>
        /// The directory from which <Inventory> <c>Item</c>s will be loaded and saved
        /// </summary>
        public readonly string inventoryDirectory;
        #endregion
>>>>>>>> serialization-changes:Pip-Boy/Entities/Player.cs

        public static byte MaxActionPoints { get; private set; } = 25;
        private readonly byte baseMaxActionPoints = MaxActionPoints;
        public byte ActionPoints { get; private set; } = 25;
<<<<<<<< HEAD:Pip-Boy/Player.cs

        public static byte DamageRessistance { get; private set; } = 0;
        private readonly byte baseDamageRessistance = DamageRessistance;
        #endregion

        #region EquippedItems
        public HeadPiece? headPiece;
        public TorsoPiece? torsoPiece;
        public Weapon? weapon;
        public Ammo? ammo;
========
>>>>>>>> serialization-changes:Pip-Boy/Entities/Player.cs
        #endregion

        #region Constructors
        /// <summary>
<<<<<<<< HEAD:Pip-Boy/Player.cs
========
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
>>>>>>>> serialization-changes:Pip-Boy/Entities/Player.cs
        /// Player Creation using code
        /// </summary>
        /// <param name="name">The player's name</param>
        /// <param name="attributeValues">The special values</param>
        public Player(string name, byte[] attributeValues)
        {
<<<<<<< HEAD:Pip-Boy/Player.cs
=======
            activeDirectory = directory;
>>>>>>> parent of 0a5484b (replaced structs with classes, and fixed pathing errors):Pip-Boy/Objects/Player.cs
            Name = name;
            for (byte index = 0; index < 7; index++)
            {
                SPECIAL[index].Value = attributeValues[index];
            }
        }

        /// <summary>
        /// Player creation using console input
        /// </summary>
        public Player()
        {
<<<<<<< HEAD:Pip-Boy/Player.cs
=======
            activeDirectory = directory;
>>>>>>> parent of 0a5484b (replaced structs with classes, and fixed pathing errors):Pip-Boy/Objects/Player.cs
            while (Name == null)
            {
                Console.Write("Enter Player Name: ");
                Name = Console.ReadLine();
                Console.Clear();
            }

            // You have 21 points to disperse across all the SPPECIAL attributes, and each one starts at 1, so 28 total
            byte totalPoints = 28;
            for (byte index = 0; index < SPECIAL.Length; index++)
            {
                byte value = 1;

                ConsoleKey key = ConsoleKey.Escape;
                while (key != ConsoleKey.Enter)
                {
                    Console.WriteLine($"Total Points: {totalPoints - value}");
                    Console.WriteLine($"Enter {SPECIAL[index].Name} value (1 - 10): {value}");
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
                SPECIAL[index].Value = value;
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

<<<<<<<< HEAD:Pip-Boy/Player.cs
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

        public void ApplyEffects()
        {
            ResetEffects();
            foreach (Effect effect in Effects)
            {
                for (byte i = 0; i < SPECIAL.Length; i++)
                {
                    if (effect.ToTitleCase() == SPECIAL[i].Name)
                    {
                        if (SPECIAL[i].Value + effect.Value >= 1)
                        {
                            SPECIAL[i].Value = (byte)(SPECIAL[i].Value + effect.Value);
                        }
                        break;
                    }
                }
                for (byte i = 0; i < Skills.Length; i++)
                {
                    if (effect.ToTitleCase() == Skills[i].Name)
                    {
                        if (Skills[i].Value + effect.Value >= 1)
                        {
                            Skills[i].Value = (byte)(Skills[i].Value + effect.Value);
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

========
>>>>>>>> serialization-changes:Pip-Boy/Entities/Player.cs
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
            StringBuilder stringBuilder = new();
            stringBuilder.AppendLine("S.P.E.C.I.A.L.:");
            foreach (Attribute attribute in SPECIAL)
            {
                stringBuilder.AppendLine(attribute.ToString());
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Shows the player's skill levels
        /// </summary>
        /// <returns>A table with every skill and its associated value</returns>
        public static string ShowSkills()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.AppendLine("Skills:");
            foreach (Attribute skill in Skills)
            {
                stringBuilder.AppendLine('\t' + skill.ToString());
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Shows all the player's perks
        /// </summary>
        /// <returns>A table of every perk's name, level and description</returns>
        public string ShowPeks()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.AppendLine("Perks:");
            foreach (Perk perk in Perks)
                stringBuilder.AppendLine('\t' + perk.ToString());

            return stringBuilder.ToString();
        }
        #endregion
    }
}
