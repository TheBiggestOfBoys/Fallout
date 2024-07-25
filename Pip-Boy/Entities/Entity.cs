using Pip_Boy.Data_Types;
using Pip_Boy.Items;
using Pip_Boy.Objects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Pip_Boy.Entities
{
    /// <summary>
    /// Generic type which other <see cref="Entity"/> sub-classes will inherit from
    /// </summary>
    [Serializable]
    public abstract class Entity
    {
        #region Arrays
        /// <summary>
        /// This holds all objects belonging to the <see cref="Entity"/>.
        /// </summary>
        [NonSerialized]
        public Inventory Inventory;

        /// <summary>
        /// The SPECIAL attributes, which effects player stats.
        /// </summary>
        public Dictionary<string, byte> SPECIAL = new()
        {
            {"Strength", 1},
            {"Perception", 1},
            {"Endurance", 1},
            {"Charisma", 1},
            {"Intelligence", 1},
            {"Agility", 1},
            {"Luck", 1}
        };

        /// <summary>
        /// The Skills, which effects player stats.
        /// </summary>
        public Dictionary<string, byte> Skills = new(){
            {"Barter", 10},
            {"Energy Weapons", 10},
            {"Explosives", 10},
            {"Gun", 10},
            {"Lockpick", 10},
            {"Medicine", 10},
            {"Melee Weapons", 10},
            {"Repair", 10},
            {"Science", 10},
            {"Sneak", 10},
            {"Speech", 10},
            {"Survival", 10},
            {"Unarmed", 10}
        };

        /// <summary>
        /// All <see cref="Effect"/>s that are active on the <see cref="Entity"/>.
        /// </summary>
        public List<Effect> Effects = [];
        #endregion

        #region Entity Info
        /// <summary>
        /// The name of the <see cref="Entity"/>.
        /// </summary>
        public string Name;

        /// <summary>
        /// The <see cref="Entity"/>'s level, which determines attributes.
        /// </summary>
        public byte Level;

        /// <summary>
        /// The maximum health the <see cref="Entity"/> can have.
        /// </summary>
        public ushort MaxHealth { get; private set; }

        /// <summary>
        /// THe current health the <see cref="Entity"/> has.
        /// </summary>
        public int CurrentHealth { get; private set; }

        /// <summary>
        /// What percent of health the <see cref="Entity"/> has.
        /// </summary>
        public float HealthPercentage { get => CurrentHealth / MaxHealth; }

        /// <summary>
        /// The resistance to physical damage.
        /// </summary>
        public byte DamageResistance { get; private set; }

        /// <summary>
        /// The maximum number of Action Points (AP) the <see cref="Entity"/> has.
        /// </summary>
        public byte MaxActionPoints { get; private set; }

        /// <summary>
        /// The current amount of Action Points (AP) the <see cref="Entity"/> has.
        /// </summary>
        public byte ActionPoints { get; private set; }

        /// <summary>
        /// An emoji representing the <see cref="Entity"/>.
        /// </summary>
        public string Icon;
        #endregion

        #region Constructor
        /// <summary>
        /// Empty constructor for Serialization.
        /// </summary>
        public Entity()
        {
            Inventory = new();
            Name = string.Empty;
        }

        /// <summary>
        /// Construction based on level.
        /// </summary>
        public Entity(string name, byte level)
        {
            Name = name;
            Level = level;
            Inventory = new();

            // Set attribute to random values, based on the level
            Random random = new();

            foreach (string key in SPECIAL.Keys)
            {
                SPECIAL[key] = (byte)random.Next(1, 10);
            }

            foreach (string key in Skills.Keys)
            {
                Skills[key] = (byte)random.Next(10, 100);
            }

            MaxHealth = (ushort)random.Next(100, 250);
            CurrentHealth = MaxHealth;

            MaxActionPoints = (byte)random.Next(5, 35);
            ActionPoints = MaxActionPoints;
        }
        #endregion

        #region File Stuff
        /// <summary>
        /// Serializes the <see cref="Item"/> to an <c>*.xml</c> file.
        /// </summary>
        /// <param name="folderPath">The folder to write the <c>*.xml</c> file to.</param>
        public string ToFile(string folderPath)
        {
            string filePath = folderPath + Name + ".xml";
            XmlSerializer x = new(GetType());
            XmlWriterSettings xmlWriterSettings = new() { NewLineOnAttributes = true, Indent = true, CloseOutput = true };
            XmlWriter writer = XmlWriter.Create(filePath, xmlWriterSettings);
            writer.WriteProcessingInstruction("xml-stylesheet", "type=\"text/css\" href=\"../Inventory Styling.css\"");
            x.Serialize(writer, this);
            writer.Close();
            return filePath;
        }

        /// <summary>
        /// Deserializes an <see cref="Entity"/> object from an <c>*.xml</c> file.
        /// </summary>
        /// <typeparam name="T">The <see cref="Entity"/> sub-class type to serialize to</typeparam>
        /// <param name="filePath">The path to the <c>*.xml</c> file.</param>
        /// <returns>The deserialized <see cref="Entity"/> object.</returns>
        /// <exception cref="NullReferenceException">If the <c>*.xml</c> file returns a null object.</exception>
        public static T FromFile<T>(string filePath)
        {
            XmlSerializer x = new(typeof(T));
            XmlReader reader = XmlReader.Create(filePath);
            T? tempItem = (T?)x.Deserialize(reader) ?? throw new NullReferenceException("XMl file object is null!");
            reader.Close();
            return tempItem;
        }
        #endregion

        #region EquippedItems
        /// <summary>
        /// The equipped <see cref="HeadPiece"/>, which can be null
        /// </summary>
        public HeadPiece? headPiece;

        /// <summary>
        /// The equipped <see cref="TorsoPiece"/>, which can be null
        /// </summary>
        public TorsoPiece? torsoPiece;

        /// <summary>
        /// The equipped <see cref="Weapon"/>, which can be null
        /// </summary>
        public Weapon? weapon;

        /// <summary>
        /// The equipped <see cref="Ammo"/>, for the <see cref="weapon"/> which can be null, if the <see cref="weapon"/> is <see cref="Weapon.WeaponType.Melee"/> or <see cref="Weapon.WeaponType.Unarmed"/>
        /// </summary>
        public Ammo? ammo;
        #endregion

        #region Items
        /// <summary>
        /// Equips the <see cref="Equipable"/> to the correct spot, depending on it's type
        /// </summary>
        /// <param name="item">The item to equip.</param>
        public void Equip(Equipable item)
        {
            switch (item)
            {
                case HeadPiece headPieceItem:
                    headPiece = headPieceItem;
                    break;
                case TorsoPiece torsoPieceItem:
                    torsoPiece = torsoPieceItem;
                    break;
                case Weapon weaponItem:
                    weapon = weaponItem;
                    break;
                case Ammo ammoItem:
                    ammo = ammoItem;
                    break;
            }
            item.Equip(this);
        }

        /// <summary>
        /// Unequips the <see cref="Equipable"/> from the correct spot, depending on it's type
        /// </summary>
        /// <param name="item">The item to unequip.</param>
        public void Unequip(Equipable item)
        {
            if (item is not null)
            {
                item.Unequip(this);
                switch (item)
                {
                    case HeadPiece:
                        headPiece = null;
                        break;
                    case TorsoPiece:
                        torsoPiece = null;
                        break;
                    case Weapon:
                        weapon = null;
                        break;
                    case Ammo:
                        ammo = null;
                        break;
                }
            }
        }
        #endregion

        #region Effects
        /// <summary>
        /// Applies all <see cref="Effect"/>s from <see cref="Effects"/> to the <see cref="Entity"/>.
        /// </summary>
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

        /// <summary>
        /// Clears all <see cref="Effect"/>s from <see cref="Effects"/>.
        /// </summary>
        public void ResetEffects()
        {
            Effects.Clear();
        }
        #endregion

        #region Show Entity Info
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
        /// Shows the <see cref="Entity"/>'s current status
        /// </summary>
        /// <returns>A table of the <see cref="Entity"/>'s name, level and current health</returns>
        public string ShowStatus()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.AppendLine("Name:\t" + Name + Icon);
            stringBuilder.AppendLine("Level:\t" + Level);
            stringBuilder.AppendLine("Health:\t" + CurrentHealth + '/' + MaxHealth);
            stringBuilder.AppendLine("Action Points:\t" + ActionPoints + '/' + MaxActionPoints);

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Shows the <see cref="Entity"/>'s skill levels
        /// </summary>
        /// <returns>A table with every skill and its associated value</returns>
        public string ShowSkills()
        {
            StringBuilder stringBuilder = new("Skills:");
            stringBuilder.AppendLine();
            foreach (string skill in Skills.Keys)
            {
                stringBuilder.AppendLine('\t' + skill + ':' + '\t' + Skills[skill]);
            }
            return stringBuilder.ToString();
        }
        #endregion
    }
}
