using Pip_Boy.Items;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Pip_Boy.Objects
{
    public class Inventory
    {
        #region Lists
        /// <summary>
        /// All weapons in the inventory
        /// </summary>
        public List<Weapon> Weapons { get; private set; } = [];
        /// <summary>
        /// All apparels in the inventory
        /// </summary>
        public List<Apparrel> Apparels { get; private set; } = [];
        /// <summary>
        /// All aid items in the inventory
        /// </summary>
        public List<Aid> Aids { get; private set; } = [];
        /// <summary>
        /// All misc items in the inventory
        /// </summary>
        public List<Misc> Miscs { get; private set; } = [];
        /// <summary>
        /// All ammo items in the inventory
        /// </summary>
        public List<Ammo> Ammos { get; private set; } = [];
        #endregion

        /// <summary>
        /// The current ITEM sub-page
        /// </summary>
        public ItemsPages itemPage = ItemsPages.Weapons;

        #region Folders
        readonly string[] expectedSubDirectories = ["Weapons", "Apparel", "Aid", "Misc", "Ammo"];
        public string InventoryFolderPath { get; private set; }
        public string WeaponFolderPath { get; private set; }
        public string ApparrelFolderPath { get; private set; }
        public string AidFolderPath { get; private set; }
        public string MiscFolderPath { get; private set; }
        public string AmmoFolderPath { get; private set; }
        #endregion

        public ushort MaxCarryWeight { get; private set; }
        public double CurrentCarryWeight { get; private set; }
        public bool IsOverEncumbered { get; private set; }

        public Inventory(string folderPath)
        {
            // Set the folder paths
            if (!folderPath.EndsWith('\\'))
            {
                folderPath += '\\';
            }
            InventoryFolderPath = folderPath;
            WeaponFolderPath = InventoryFolderPath + "Weapons" + '\\';
            ApparrelFolderPath = InventoryFolderPath + "Apparrel" + '\\';
            AidFolderPath = InventoryFolderPath + "Aid" + '\\';
            MiscFolderPath = InventoryFolderPath + "Misc" + '\\';
            AmmoFolderPath = InventoryFolderPath + "Ammo" + '\\';

            MaxCarryWeight = 100;
            string[] subDirectories = Directory.GetDirectories(folderPath);

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
                                Weapons.Add(Weapon.FromFile(filePath)); break;
                            case "Apparel":
                                Apparels.Add(Apparrel.FromFile(filePath)); break;
                            case "Aid":
                                Aids.Add(Aid.FromFile(filePath)); break;
                            case "Misc":
                                Miscs.Add(Misc.FromFile(filePath)); break;
                            case "Ammo":
                                Ammos.Add(Ammo.FromFile(filePath)); break;
                        }
                    }
                }
            }
            CurrentCarryWeight = CalculateWeight();
        }

        /// <summary>
        /// Sums up the weight of all items in the inventory
        /// </summary>
        /// <returns>The total weight</returns>
        private double CalculateWeight() => Weapons.Sum(x => x.Weight)+ Apparels.Sum(x => x.Weight)+ Aids.Sum(x => x.Weight) + Miscs.Sum(x => x.Weight) + Ammos.Sum(x => x.Weight);

        public void Save()
        {
            foreach (Weapon weapon in Weapons)
            {
                weapon.ToFile(WeaponFolderPath);
            }
            foreach (Apparrel apparrel in Apparels)
            {
                apparrel.ToFile(ApparrelFolderPath);
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
        /// <param name="item">The item to add</param>
        public void Add(Item item)
        {
            if (item is Weapon weapon)
            {
                Weapons.Add(weapon);
            }
            else if (item is Apparrel apparrel)
            {
                Apparels.Add(apparrel);
            }
            else if (item is Aid aid)
            {
                Aids.Add(aid);
            }
            else if (item is Misc misc)
            {
                Miscs.Add(misc);
            }
            else if (item is Ammo ammo)
            {
                Ammos.Add(ammo);
            }
        }

        /// <summary>
        /// Removes a item from its list
        /// </summary>
        /// <param name="item">The item to drop</param>
        public void Drop(Item item)
        {
            if (item is Weapon weapon)
            {
                Weapons.Remove(weapon);
            }
            else if (item is Apparrel apparrel)
            {
                Apparels.Remove(apparrel);
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

        /// <summary>
        /// Shows the items with the current submenu type
        /// </summary>
        /// <returns>A table of every `Type` item's name, description, value and weight</returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new();
            switch (itemPage)
            {
                case ItemsPages.Weapons:
                    foreach (Weapon weapon in Weapons)
                    {
                        stringBuilder.AppendLine(weapon.ToString());
                    }
                    return stringBuilder.ToString();
                case ItemsPages.Apparel:
                    foreach (Apparrel apparrel in Apparels)
                    {
                        stringBuilder.AppendLine(apparrel.ToString());
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
        /// ITEM sub-menu pages
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
