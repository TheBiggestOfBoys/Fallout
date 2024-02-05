using System;
using System.Collections.Generic;
using System.Text;

namespace Pip_Boy
{
    internal class Player
    {
        #region Arrays
        public Attribute[] SPECIAL = [
            new("Strength", 5),
            new("Perception", 5),
            new("Endurance", 5),
            new("Charisma", 5),
            new("Intelligence", 5),
            new("Agility", 5),
            new("Luck", 5)];

        public Attribute[] Skills = [
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
            new("Unarmed", 10)];

        public List<Perk> Perks = [new("No Perks", "You have no perks, you get one every 2 levels", 0)];
        #endregion

        public readonly string name;
        public byte level = 1;
        public ushort maxHealth = 100;
        public ushort currentHealth = 100;

        #region Constructors
        /// <summary>
        /// Player Creation using code
        /// </summary>
        /// <param name="name">The player's name</param>
        /// <param name="attributeValues">The special values</param>
        public Player(string name, byte[] attributeValues)
        {
            this.name = name;
            for (byte index = 0; index < 7; index++)
                SPECIAL[index].Value = attributeValues[index];
        }

        /// <summary>
        /// Player creation using console input
        /// </summary>
        public Player()
        {
            while (name == null)
            {
                Console.Write("Enter Player Name: ");
                name = Console.ReadLine();
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

        #region Show Player Info
        /// <summary>
        /// Shows the player's current status
        /// </summary>
        /// <returns>A table of the player's name, level and current health</returns>
        public string ShowStatus()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.AppendLine("Name:\t" + name);
            stringBuilder.AppendLine("Level:\t" + level);
            stringBuilder.AppendLine("Health:\t" + currentHealth + '/' + maxHealth);

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Shows the player's SPECIAL attributes
        /// </summary>
        /// <returns>A table of all SPECIAL attributes and their values</returns>
        public string ShowSPECIAL()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.AppendLine("S.P.E.C.I.A.L.:");
            foreach (Attribute attribute in SPECIAL)
                stringBuilder.AppendLine($"\t{attribute.Name}:\t{attribute.Value}");

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Shows the player's skill levels
        /// </summary>
        /// <returns>A table with every skill and its associated value</returns>
        public string ShowSkills()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.AppendLine("Skills:");
            foreach (Attribute skill in Skills)
                stringBuilder.AppendLine($"\t{skill.Name}:\t{skill.Value}");

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
                stringBuilder.AppendLine($"\t{perk.ToString()}");

            return stringBuilder.ToString();
        }
        #endregion
    }
}
