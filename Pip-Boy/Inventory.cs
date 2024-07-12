using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Pip_Boy
{
    public class Inventory
    {
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

        #region Lists
        /// <summary>
        /// All weapons in the inventory
        /// </summary>
        public List<Weapon> Weapons { get; private set; } = [new("10mm Pistol", 5.5, 55, [], Weapon.WeaponType.Gun, 3, 10, 100)];
        /// <summary>
        /// All apparels in the inventory
        /// </summary>
        public List<Apparrel> Apparels { get; private set; } = [new TorsoPiece("Vault 13 Jumpsuit", 5, 25, [], 3, false)];
        /// <summary>
        /// All aid items in the inventory
        /// </summary>
        public List<Aid> Aids { get; private set; } = [new("Stimpack", 1, 30, [])];
        /// <summary>
        /// All misc items in the inventory
        /// </summary>
        public List<Misc> Miscs { get; private set; } = [new("Journal Entry", 1, 15)];
        /// <summary>
        /// All ammo items in the inventory
        /// </summary>
        public List<Ammo> Ammos { get; private set; } = [new("10mm Ammo", 1, [], Ammo.AmmoType.Bullet, Ammo.AmmoModification.Standard)];
        #endregion

        private double CalculateWeight()
        {
            double tempWeight = 0;
            foreach (Weapon weapon in Weapons)
            {
                tempWeight += weapon.Weight;
            }
            foreach (Apparrel apparrel in Apparels)
            {
                tempWeight += apparrel.Weight;
            }
            foreach (Aid aid in Aids)
            {
                tempWeight += aid.Weight;
            }
            foreach (Misc misc in Miscs)
            {
                tempWeight += misc.Weight;
            }
            foreach (Ammo ammo in Ammos)
            {
                tempWeight += ammo.Weight;
            }
            return tempWeight;
        }

        public void Save()
        {
            foreach (Weapon weapon in Weapons)
            {
                weapon.ToFile(WeaponFolderPath);
            }
            foreach (Apparrel apparrel in Apparels)
            {
                apparrel.ToFile(WeaponFolderPath);
            }
            foreach (Aid aid in Aids)
            {
                aid.ToFile(WeaponFolderPath);
            }
            foreach (Misc misc in Miscs)
            {
                misc.ToFile(WeaponFolderPath);
            }
            foreach (Ammo ammo in Ammos)
            {
                ammo.ToFile(WeaponFolderPath);
            }
        }

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
