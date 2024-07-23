using Pip_Boy.Entities;
using Pip_Boy.Items;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Pip_Boy.Objects
{
    /// <summary>
    /// Controls behavior of the sub-lists, and the items contained within.
    /// </summary>
    public class Inventory
    {
        #region Lists
        /// <summary>
        /// All weapons in the <c>Inventory</c>
        /// </summary>
        public List<Weapon> Weapons { get; private set; } = [];
        /// <summary>
        /// All apparels in the <see cref="Inventory"/>
        /// </summary>
        public List<Apparel> Apparels { get; private set; } = [];
        /// <summary>
        /// All aid items in the <see cref="Inventory"/>
        /// </summary>
        public List<Aid> Aids { get; private set; } = [];
        /// <summary>
        /// All misc items in the <see cref="Inventory"/>
        /// </summary>
        public List<Misc> Miscs { get; private set; } = [];
        /// <summary>
        /// All ammo items in the <see cref="Inventory"/>
        /// </summary>
        public List<Ammo> Ammos { get; private set; } = [];
        #endregion

        /// <summary>
        /// The current <c>Item</c> sub-page, which determines which sub-list to display.
        /// </summary>
        public ItemsPages itemPage = ItemsPages.Weapons;

        #region Folders
        /// <summary>
        /// The sub-directories needed in the <c>InventoryFolderPath</c> directory
        /// </summary>
        readonly string[] expectedSubDirectories = ["Weapon", "Apparel", "Aid", "Misc", "Ammo"];

        /// <summary>
        /// Directory which holds all sub-directories for <c>Inventory</c> items
        /// </summary>
        public string InventoryFolderPath { get; private set; }

        /// <summary>
        /// Directory which holds all Serialized <c>Weapon</c> objects for the <c>Weapons</c> list
        /// </summary>
        public string WeaponFolderPath { get; private set; }

        /// <summary>
        /// Directory which holds all Serialized <c>Apparel</c> objects for the <c>Apparels</c> list
        /// </summary>
        public string ApparelFolderPath { get; private set; }

        /// <summary>
        /// Directory which holds all Serialized <c>Aid</c> objects for the <c>Aids</c> list
        /// </summary>
        public string AidFolderPath { get; private set; }

        /// <summary>
        /// Directory which holds all Serialized <c>Misc</c> objects for the <c><see cref="Miscs"/></c> list
        /// </summary>
        public string MiscFolderPath { get; private set; }

        /// <summary>
        /// Directory which holds all Serialized <c>Ammo</c> objects for the <c>Ammos</c> list
        /// </summary>
        public string AmmoFolderPath { get; private set; }
        #endregion

        /// <summary>
        /// The maximum weight the player can carry
        /// </summary>
        public ushort MaxCarryWeight { get; private set; }

        /// <summary>
        /// The current weight of all items in <c>Inventory</c>
        /// </summary>
        public double CurrentCarryWeight { get; private set; }

        /// <summary>
        /// If <c>CurrentCarryWeight</c> is greater than or equal to the <c>MaxCarryWeight</c>
        /// </summary>
        public bool IsOverEncumbered { get; private set; }

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

            MaxCarryWeight = 0;
        }

        public Inventory(string folderPath, Player player)
        {
            // Set the folder paths
            if (!folderPath.EndsWith('\\'))
            {
                folderPath += '\\';
            }
            InventoryFolderPath = folderPath;
            WeaponFolderPath = InventoryFolderPath + "Weapon" + '\\';
            ApparelFolderPath = InventoryFolderPath + "Apparel" + '\\';
            AidFolderPath = InventoryFolderPath + "Aid" + '\\';
            MiscFolderPath = InventoryFolderPath + "Misc" + '\\';
            AmmoFolderPath = InventoryFolderPath + "Ammo" + '\\';

            MaxCarryWeight = (ushort)(150 + (player.SPECIAL["Strength"] * 10));

            // Get the sub-directories of the <c>Inventory</c> directory
            string[] subDirectories = Directory.GetDirectories(folderPath);
            // Strip the containing directory info
            for (int index = 0; index < subDirectories.Length; index++)
            {
                subDirectories[index] = subDirectories[index].Split('\\')[^1];
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
                                Weapons.Add(Weapon.FromFile(filePath));
                                break;
                            case "Apparel":
                                Apparels.Add(Apparel.FromFile(filePath));
                                break;
                            case "Aid":
                                Aids.Add(Aid.FromFile(filePath));
                                break;
                            case "Misc":
                                Miscs.Add(Misc.FromFile(filePath));
                                break;
                            case "Ammo":
                                Ammos.Add(Ammo.FromFile(filePath));
                                break;
                        }
                    }
                }
            }
            CurrentCarryWeight = CalculateWeight();
        }
        #endregion

        /// <summary>
        /// Sums up the weight of all items in the inventory
        /// </summary>
        /// <returns>The total weight</returns>
        private double CalculateWeight() => Weapons.Sum(x => x.Weight) + Apparels.Sum(x => x.Weight) + Aids.Sum(x => x.Weight) + Miscs.Sum(x => x.Weight) + Ammos.Sum(x => x.Weight);

        /// <summary>
        /// Writes all items in the <c>Inventory</c> sub-lists to files in the correct directories.
        /// </summary>
        public void Save()
        {
            foreach (Weapon weapon in Weapons)
            {
                weapon.ToFile(WeaponFolderPath);
            }
            foreach (Apparel apparel in Apparels)
            {
                apparel.ToFile(ApparelFolderPath);
            }
            foreach (Aid aid in Aids)
            {
                aid.ToFile(AidFolderPath);
            }
            foreach (Misc misc in Miscs)
            {
                misc.ToFile(MiscFolderPath);
            }
            foreach (Ammo ammo in Ammos)
            {
                ammo.ToFile(AmmoFolderPath);
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
                    break;
                case Apparel apparel:
                    Apparels.Add(apparel);
                    break;
                case Aid aid:
                    Aids.Add(aid);
                    break;
                case Misc misc:
                    Miscs.Add(misc);
                    break;
                case Ammo ammo:
                    Ammos.Add(ammo);
                    break;
            }
        }

        /// <summary>
        /// Removes a item from its list
        /// </summary>
        /// <param name="item">The <c>Item</c> to drop</param>
        public void Drop(Item item)
        {
            if (item is Weapon weapon)
            {
                Weapons.Remove(weapon);
            }
            else if (item is Apparel apparel)
            {
                Apparels.Remove(apparel);
            }
            else if (item is Aid aid)
            {
                Aids.Remove(aid);
            }
            else if (item is Misc misc)
            {
                Miscs.Remove(misc);
            }
            else if (item is Ammo ammo)
            {
                Ammos.Remove(ammo);
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
