using Pip_Boy.Entities;
using Pip_Boy.Items;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Pip_Boy.Objects
{
    /// <summary>
    /// Controls behavior of the sub-lists, and the <see cref="Item"/>s contained within.
    /// </summary>
    public class Inventory
    {
        #region Lists
        /// <summary>
        /// All <see cref="Weapon"/>s in the<see cref="Inventory"/>.
        /// </summary>
        public List<Weapon> Weapons { get; private set; } = [];

        /// <summary>
        /// All <see cref="Apparel"/>s in the <see cref="Inventory"/>.
        /// </summary>
        public List<Apparel> Apparels { get; private set; } = [];

        /// <summary>
        /// All <see cref="Aid"/>s in the <see cref="Inventory"/>.
        /// </summary>
        public List<Aid> Aids { get; private set; } = [];

        /// <summary>
        /// All <see cref="Misc"/>s in the <see cref="Inventory"/>.
        /// </summary>
        public List<Misc> Miscs { get; private set; } = [];

        /// <summary>
        /// All <see cref="Ammo"/>s in the <see cref="Inventory"/>.
        /// </summary>
        public List<Ammo> Ammos { get; private set; } = [];
        #endregion

        /// <summary>
        /// The current <see cref="Item"/> sub-page, which determines which sub-list to display.
        /// </summary>
        public ItemsPages itemPage = ItemsPages.Weapons;

        #region Folders
        /// <summary>
        /// The sub-directories needed in the <see cref="InventoryFolderPath"/> directory
        /// </summary>
        readonly string[] expectedSubDirectories = ["Weapon", "Apparel", "Aid", "Misc", "Ammo"];

        /// <summary>
        /// Directory which holds all sub-directories for <see cref="Inventory"/> <see cref="Item"/>s
        /// </summary>
        public readonly string InventoryFolderPath;

        /// <summary>
        /// Directory which holds all Serialized <see cref="Weapon"/> objects for the <see cref="Weapons"/> list
        /// </summary>
        public readonly string WeaponFolderPath;

        /// <summary>
        /// Directory which holds all Serialized <see cref="Apparel"/> objects for the <see cref="Apparels"/> list
        /// </summary>
        public readonly string ApparelFolderPath;

        /// <summary>
        /// Directory which holds all Serialized <see cref="Aid"/> objects for the <see cref="Aids"/> list
        /// </summary>
        public readonly string AidFolderPath;

        /// <summary>
        /// Directory which holds all Serialized <see cref="Misc"/> objects for the <see cref="Miscs"/> list
        /// </summary>
        public readonly string MiscFolderPath;

        /// <summary>
        /// Directory which holds all Serialized <see cref="Ammo"/> objects for the <see cref="Ammos"/> list
        /// </summary>
        public readonly string AmmoFolderPath;
        #endregion

        /// <summary>
        /// The maximum weight the player can carry
        /// </summary>
        public ushort MaxCarryWeight { get; private set; }

        /// <summary>
        /// The current weight of all items in <see cref="Inventory"/>
        /// </summary>
        public float CurrentCarryWeight => CalculateWeight();

        /// <summary>
        /// If <see cref="CurrentCarryWeight"/> is greater than or equal to the <see cref="MaxCarryWeight"/>
        /// </summary>
        public bool IsOverEncumbered => CurrentCarryWeight >= MaxCarryWeight;

        #region Constructors
        /// <summary>
        /// Empty constructor for serialization.
        /// </summary>
        public Inventory()
        {
            InventoryFolderPath = string.Empty;
            WeaponFolderPath = string.Empty;
            ApparelFolderPath = string.Empty;
            AidFolderPath = string.Empty;
            MiscFolderPath = string.Empty;
            AmmoFolderPath = string.Empty;
        }

        /// <summary>
        /// Creates an inventory from folder and <see cref="Player"/>.
        /// </summary>
        /// <param name="folderPath">Where to load the items from.</param>
        /// <param name="player">Used to determine max carry weight.</param>
        public Inventory(string folderPath, Player player)
        {
            if (!Directory.Exists(folderPath))
            {
                throw new DirectoryNotFoundException("Folder not found. " + folderPath);
            }

            // Set the folder paths
            if (!folderPath.EndsWith('\\'))
            {
                folderPath += '\\';
            }
            InventoryFolderPath = folderPath;
            WeaponFolderPath = InventoryFolderPath + "Weapon\\";
            ApparelFolderPath = InventoryFolderPath + "Apparel\\";
            AidFolderPath = InventoryFolderPath + "Aid\\";
            MiscFolderPath = InventoryFolderPath + "Misc\\";
            AmmoFolderPath = InventoryFolderPath + "Ammo\\";

            MaxCarryWeight = (ushort)(150 + (player.SPECIAL[0].Value * 10));

            // Get the sub-directories of the Inventory directory
            string[] subDirectories = Directory.GetDirectories(folderPath);
            // Strip the containing directory info
            for (int index = 0; index < subDirectories.Length; index++)
            {
                subDirectories[index] = Path.GetFileName(subDirectories[index]);
            }

            foreach (string expectedSubDirectory in expectedSubDirectories)
            {
                if (subDirectories.Contains(expectedSubDirectory))
                {
                    string currentFolder = folderPath + expectedSubDirectory + '\\';
                    string[] filePaths = Directory.GetFiles(currentFolder);
                    foreach (string filePath in filePaths)
                    {
                        switch (expectedSubDirectory)
                        {
                            case "Weapon":
                                Weapons.Add(PipBoy.FromFile<Weapon>(filePath));
                                break;
                            case "Apparel":
                                if (PipBoy.GetTypeFromXML(filePath) == typeof(HeadPiece))
                                    Apparels.Add(PipBoy.FromFile<HeadPiece>(filePath));
                                else if (PipBoy.GetTypeFromXML(filePath) == typeof(TorsoPiece))
                                    Apparels.Add(PipBoy.FromFile<TorsoPiece>(filePath));
                                break;
                            case "Aid":
                                Aids.Add(PipBoy.FromFile<Aid>(filePath));
                                break;
                            case "Misc":
                                Miscs.Add(PipBoy.FromFile<Misc>(filePath));
                                break;
                            case "Ammo":
                                Ammos.Add(PipBoy.FromFile<Ammo>(filePath));
                                break;
                        }
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// Sums up the weight of all items in the <see cref="Inventory"/>
        /// </summary>
        /// <returns>The total weight</returns>
        private float CalculateWeight() => Weapons.Sum(x => x.Weight) + Apparels.Sum(x => x.Weight) + Aids.Sum(x => x.Weight) + Miscs.Sum(x => x.Weight) + Ammos.Sum(x => x.Weight);

        /// <summary>
        /// Writes all items in the <see cref="Inventory"/> sub-lists to files in the correct directories.
        /// </summary>
        public void Save()
        {
            foreach (Weapon weapon in Weapons)
            {
                PipBoy.ToFile(WeaponFolderPath, weapon);
            }
            foreach (Apparel apparel in Apparels)
            {
                PipBoy.ToFile(ApparelFolderPath, apparel);
            }
            foreach (Aid aid in Aids)
            {
                PipBoy.ToFile(AidFolderPath, aid);
            }
            foreach (Misc misc in Miscs)
            {
                PipBoy.ToFile(MiscFolderPath, misc);
            }
            foreach (Ammo ammo in Ammos)
            {
                PipBoy.ToFile(AmmoFolderPath, ammo);
            }
        }

        #region Inventory Management
        /// <summary>
        /// Clear all <c>Inventory</c> sub-lists.
        /// </summary>
        public void Clear()
        {
            Weapons.Clear();
            Apparels.Clear();
            Aids.Clear();
            Miscs.Clear();
            Ammos.Clear();
        }

        /// <summary>
        /// Adds a generic item to its correct list
        /// </summary>
        /// <param name="item">The <c>Item</c> to add</param>
        public void Add(Item item)
        {
            switch (item)
            {
                case Weapon weapon:
                    Weapons.Add(weapon);
                    Weapons.Sort((x, y) => string.Compare(x.Name, y.Name));
                    break;
                case Apparel apparel:
                    Apparels.Add(apparel);
                    Apparels.Sort((x, y) => string.Compare(x.Name, y.Name));
                    break;
                case Aid aid:
                    Aids.Add(aid);
                    Aids.Sort((x, y) => string.Compare(x.Name, y.Name));
                    break;
                case Misc misc:
                    Miscs.Add(misc);
                    Miscs.Sort((x, y) => string.Compare(x.Name, y.Name));
                    break;
                case Ammo ammo:
                    Ammos.Add(ammo);
                    Ammos.Sort((x, y) => string.Compare(x.Name, y.Name));
                    break;
            }
        }

        /// <summary>
        /// Removes a item from its list
        /// </summary>
        /// <param name="item">The <c>Item</c> to drop</param>
        public void Drop(Item item)
        {
            switch (item)
            {
                case Weapon weapon:
                    Weapons.Remove(weapon);
                    Weapons.Sort((x, y) => string.Compare(x.Name, y.Name));
                    break;
                case Apparel apparel:
                    Apparels.Remove(apparel);
                    Apparels.Sort((x, y) => string.Compare(x.Name, y.Name));
                    break;
                case Aid aid:
                    Aids.Remove(aid);
                    Weapons.Sort((x, y) => string.Compare(x.Name, y.Name));
                    break;
                case Misc misc:
                    Miscs.Remove(misc);
                    Miscs.Sort((x, y) => string.Compare(x.Name, y.Name));
                    break;
                case Ammo ammo:
                    Ammos.Remove(ammo);
                    Ammos.Sort((x, y) => string.Compare(x.Name, y.Name));
                    break;
            }
        }

        /// <summary>
        /// Counts how many occurrences of an <see cref="Item"/> are in a <see cref="List{Item}"/>
        /// </summary>
        /// <param name="list">The <see cref="List{Item}"/> to search through.</param>
        /// <param name="item">The <see cref="Item"/> to look for.</param>
        /// <returns></returns>
        public static int CountItem(List<Item> list, Item item)
        {
            if (list.Contains(item))
            {
                int count = 1;
                for (int index = list.FindIndex(f => f.Name == item.Name) + 1; index < list.Count; index++)
                {
                    if (list[index].Name == item.Name)
                    {
                        count++;
                    }
                    // Ideally the list should be sorted alphabetically by name, 
                    // so there is no need to keep checking once the name isn't the same.
                    else
                    {
                        return count;
                    }
                }
                return count;
            }
            else
            {
                return 0;
            }
        }
        #endregion

        /// <returns>A table of every <see cref="itemPage"/> <see cref="Item"/>'s <see cref="Item.ToString()"/></returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new($"{CurrentCarryWeight}/{MaxCarryWeight} -- Over Encumbered?: {IsOverEncumbered}");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(new string('-', 15));
            switch (itemPage)
            {
                case ItemsPages.Weapons:
                    foreach (Weapon weapon in Weapons)
                    {
                        stringBuilder.AppendLine(weapon.ToString());
                    }
                    return stringBuilder.ToString();
                case ItemsPages.Apparel:
                    foreach (Apparel apparel in Apparels)
                    {
                        stringBuilder.AppendLine(apparel.ToString());
                    }
                    return stringBuilder.ToString();
                case ItemsPages.Aid:
                    foreach (Aid aid in Aids)
                    {
                        stringBuilder.AppendLine(aid.ToString());
                    }
                    return stringBuilder.ToString();
                case ItemsPages.Ammo:
                    foreach (Ammo ammo in Ammos)
                    {
                        stringBuilder.AppendLine(ammo.ToString());
                    }
                    return stringBuilder.ToString();
                case ItemsPages.Misc:
                    foreach (Misc misc in Miscs)
                    {
                        stringBuilder.AppendLine(misc.ToString());
                    }
                    return stringBuilder.ToString();
                default:
                    throw new InvalidDataException("Invalid ItemsPage!");
            }
        }

        /// <summary>
        /// <see cref="Item"/> sub-menu pages
        /// </summary>
        public enum ItemsPages
        {
            Weapons,
            Apparel,
            Aid,
            Ammo,
            Misc
        }
    }
}
