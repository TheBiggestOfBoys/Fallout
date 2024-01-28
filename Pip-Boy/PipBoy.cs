using System.Media;
using System.Text;

namespace Pip_Boy
{
    internal class PipBoy
    {
        #region Objects
        public Random random = new();
        public Player player = new("Player", [5, 5, 5, 5, 5, 5, 5]);
        public Radio radio = new("C:\\Users\\jrsco\\source\\repos\\Pip-Boy\\Pip-Boy\\Songs\\");
        public Map map = new(25, 75, 15);
        public SoundPlayer soundEffects = new();
        #endregion

        #region Lists
        public List<Item> inventory = [
            new("10mm Pistol", "A common weapon in the wasteland", 5.5, 55, Item.Types.WEAPON),
            new("Sniper Rifle", "A high damage, low fire rate marksman rifle", 12.75, 250, Item.Types.WEAPON),
            new("Vault 13 Jumpsuit", "The standard jumpsuit for all Vault 13 residents", 5, 25, Item.Types.APPAREL),
            new("10mm Ammo", "A common ammo for many weapons", 0, 1, Item.Types.AMMO),
            new("Stimpack", "A healing device", 1, 30, Item.Types.AID),
            new("Journal Entry", "Exposition thingy", 1, 15, Item.Types.MISC)];
        public List<Quest> Quests = [new("Finish the game", [new("Do stuff", 16, 16), new("Do things", 32, 32)])];
        #endregion

        public ConsoleColor color = ConsoleColor.Green;

        #region Page Info
        public Pages currentPage = Pages.STATS;
        public StatsPages statPage = StatsPages.Status;
        public ItemsPages itemPage = ItemsPages.Weapons;
        public DataPages dataPage = DataPages.Map;
        #endregion

        #region Sounds
        public string[] sounds = Directory.GetFiles("C:\\Users\\jrsco\\source\\repos\\Pip-Boy\\Pip-Boy\\Sounds\\", "*.wav");
        public string[]
            staticSounds = Directory.GetFiles("C:\\Users\\jrsco\\source\\repos\\Pip-Boy\\Pip-Boy\\Sounds\\static\\", "*wav");
        public string[] radiationSounds = Directory.GetFiles("C:\\Users\\jrsco\\source\\repos\\Pip-Boy\\Pip-Boy\\Sounds\\radiation\\", "*wav");
        #endregion

        #region Menu Navigation
        /// <summary>
        /// Changes the current menu page
        /// </summary>
        /// <param name="right">Move right?</param>
        public void ChangeMenu(bool right)
        {
            soundEffects.SoundLocation = sounds[^3];
            soundEffects.PlaySync();
            if (right && currentPage < Pages.DATA)
                currentPage++;
            if (!right && currentPage > Pages.STATS)
                currentPage--;
        }

        /// <summary>
        /// Changes the current sub menu page
        /// </summary>
        /// <param name="right">Move right?</param>
        public void ChangeSubMenu(bool right)
        {
            soundEffects.SoundLocation = sounds[^3];
            soundEffects.PlaySync();

            switch (currentPage)
            {
                case Pages.STATS when right && statPage < StatsPages.General:
                    statPage++;
                    break;
                case Pages.STATS when !right && statPage > StatsPages.Status:
                    statPage--;
                    break;

                case Pages.ITEMS when right && itemPage < ItemsPages.Misc:
                    itemPage++;
                    break;
                case Pages.ITEMS when !right && itemPage > ItemsPages.Weapons:
                    itemPage--;
                    break;

                case Pages.DATA when right && dataPage < DataPages.Radio:
                    dataPage++;
                    break;
                case Pages.DATA when !right && dataPage > DataPages.Map:
                    dataPage--;
                    break;
            }
        }
        #endregion

        #region Menu
        /// <summary>
        /// Shows strings based in the selected page
        /// </summary>
        /// <returns>The corresponding string</returns>
        public string ShowMenu() => currentPage switch
        {
            Pages.STATS => ShowStats(),
            Pages.ITEMS => ShowInventory(),
            Pages.DATA => ShowData()
        };

        /// <summary>
        /// Display the the SubMenus for each main page
        /// </summary>
        /// <returns>The footer of the SubMenus</returns>
        public string[] GetSubMenu()
        {
            List<string> footer = [];
            Type enumType = currentPage switch
            {
                Pages.STATS => typeof(StatsPages),
                Pages.ITEMS => typeof(ItemsPages),
                Pages.DATA => typeof(DataPages)
            };
            foreach (var subPage in Enum.GetValues(enumType))
                footer.Add(subPage.ToString());

            return [.. footer];
        }

        /// <summary>
        /// Shows and highlights the selected submenu for the page
        /// </summary>
        /// <param name="subMenuItems">The submenu items of the current page</param>
        public void ShowSubMenu(string[] subMenuItems)
        {
            foreach (string item in subMenuItems)
            {
                Console.Write('\t');
                if (item == statPage.ToString() || item == itemPage.ToString() || item == dataPage.ToString())
                    Highlight(item, false);
                else
                    Console.Write(item);
                Console.Write('\t');
            }
            Console.WriteLine();
        }
        #endregion

        #region Page Logic
        /// <summary>
        /// Logic behind showing the Stats Page's submenus
        /// </summary>
        /// <returns>The corresponding string</returns>
        public string ShowStats() => statPage switch
        {
            StatsPages.Status => player.ShowStatus(),
            StatsPages.SPECIAL => player.ShowSPECIAL(),
            StatsPages.Skills => player.ShowSkills(),
            StatsPages.Perks => player.ShowPeks(),
            StatsPages.General => "GENERAL STUFF GOES HERE!"
        };

        /// <summary>
        /// Shows the items with the current submenu type
        /// </summary>
        /// <returns>A table of every `Type` item's name, description, value and weight</returns>
        public string ShowInventory()
        {
            StringBuilder stringBuilder = new();
            Item.Types sortType = itemPage switch
            {
                ItemsPages.Weapons => Item.Types.WEAPON,
                ItemsPages.Apparel => Item.Types.APPAREL,
                ItemsPages.Aid => Item.Types.AID,
                ItemsPages.Ammo => Item.Types.AMMO,
                ItemsPages.Misc => Item.Types.MISC
            };

            foreach (Item item in inventory)
                if (item.Type == sortType)
                    stringBuilder.AppendLine($"\t{item.Name}: {item.Description}\n\t\tValue: {item.Value}\n\t\tWeight: {item.Weight}");

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Logic behind showing the Data Page's submenus
        /// </summary>
        /// <returns>The corresponding string</returns>
        public string ShowData() => dataPage switch
        {
            DataPages.Map => map.ToString(),
            DataPages.Quests => ShowQuests(),
            DataPages.Misc => "MISC NOTES GOES HERE!!!",
            DataPages.Radio => radio.ToString()
        };

        /// <summary>
        /// Shows all active quests and their steps
        /// </summary>
        /// <returns>A table of all quests and their steps</returns>
        public string ShowQuests()
        {
            StringBuilder stringBuilder = new();
            foreach (Quest quest in Quests)
                stringBuilder.AppendLine(quest.ToString());

            return stringBuilder.ToString();
        }
        #endregion


        #region Console Functions
        /// <summary>
        /// Error message and sound
        /// </summary>
        /// <param name="message">The error message to display</param>
        public void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(message);
            Console.Beep(500, 500);
            Console.Beep(500, 500);
            Console.ForegroundColor = color;
        }

        /// <summary>
        /// Highlights a message in the console
        /// </summary>
        /// <param name="message">The message to highlight</param>
        public void Highlight(string message, bool newLine)
        {
            Console.BackgroundColor = color;
            Console.ForegroundColor = ConsoleColor.Black;
            if (newLine)
                Console.WriteLine(message);
            else
                Console.Write(message);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = color;
        }
        #endregion

        #region Enums
        public enum Pages
        {
            STATS,
            ITEMS,
            DATA
        }

        #region SubPages
        public enum StatsPages
        {
            Status,
            SPECIAL,
            Skills,
            Perks,
            General
        }

        public enum ItemsPages
        {
            Weapons,
            Apparel,
            Aid,
            Ammo,
            Misc
        }

        public enum DataPages
        {
            Map,
            Quests,
            Misc,
            Radio
        }
        #endregion
        #endregion
    }
}
