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
        public List<Item> inventory = [
            new("10mm Pistol", "A common weapon in the wasteland", 5.5, 55, Item.Types.WEAPON),
            new("Sniper Rifle", "A high damage, low fire rate marksman rifle", 12.75, 250, Item.Types.WEAPON),
            new("Vault 13 Jumpsuit", "The standard jumpsuit for all Vault 13 residents", 5, 25, Item.Types.APPAREL),
            new("10mm Ammo", "A common ammo for many weapons", 0, 1, Item.Types.AMMO),
            new("Stimpack", "A healing device", 1, 30, Item.Types.AID),
            new("Journal Entry", "Exposition thingy", 1, 15, Item.Types.MISC)];
        public SoundPlayer soundEffects = new();
        #endregion

        public ConsoleColor color = ConsoleColor.Green;

        #region Page Info
        public Pages currentPage = Pages.STATS;
        public StatsPages statPage = StatsPages.Status;
        public ItemsPages itemPage = ItemsPages.Weapons;
        public DataPages dataPage = DataPages.LocalMap;
        #endregion

        #region Sounds
        public string[] sounds = Directory.GetFiles("C:\\Users\\jrsco\\source\\repos\\Pip-Boy\\Pip-Boy\\Sounds\\", "*.wav");
        public string[]
            staticSounds = Directory.GetFiles("C:\\Users\\jrsco\\source\\repos\\Pip-Boy\\Pip-Boy\\Sounds\\static\\", "*wav");
        public string[] radiationSounds = Directory.GetFiles("C:\\Users\\jrsco\\source\\repos\\Pip-Boy\\Pip-Boy\\Sounds\\radiation\\", "*wav");
        #endregion

        #region Menu
        public void ChangeMenu(bool right)
        {
            soundEffects.SoundLocation = sounds[^3];
            soundEffects.PlaySync();
            if (right && currentPage < Pages.DATA)
                currentPage++;
            if (!right && currentPage > Pages.STATS)
                currentPage--;
        }

        public void ChangeSubMenu(bool right)
        {
            soundEffects.SoundLocation = sounds[^3];
            soundEffects.PlaySync();

            switch (currentPage)
            {
                case Pages.STATS:
                    if (right && statPage < StatsPages.General)
                        statPage++;
                    if (!right && statPage > StatsPages.Status)
                        statPage--;
                    break;
                case Pages.ITEMS:
                    if (right && itemPage < ItemsPages.Misc)
                        itemPage++;
                    if (!right && itemPage > ItemsPages.Weapons)
                        itemPage--;
                    break;
                case Pages.DATA:
                    if (right && dataPage < DataPages.Radio)
                        dataPage++;
                    if (!right && dataPage > DataPages.LocalMap)
                        dataPage--;
                    break;
            }
        }

        public void Scroll(bool up)
        {
            soundEffects.SoundLocation = sounds[^2];
            soundEffects.PlaySync();
        }

        public string ShowMenu() => currentPage switch
        {
            Pages.STATS => player.ToString(),
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

        public Player CreatePlayer()
        {
            Player tempPlayer = new();
            byte[] tempAttributes = [0, 0, 0, 0, 0, 0, 0];
            Console.Write("Enter Player Name: ");
            string name = Console.ReadLine();

            for (byte x = 0; x < tempPlayer.attributeNames.Length; x++)
            {
                Console.Write($"Enter {tempPlayer.attributeNames[x]} value (1 - 10): ");
                if (byte.TryParse(Console.ReadLine(), out byte y) && y > 0 && y < 11)
                    tempAttributes[x] = y;
            }
            return new Player(name, tempAttributes);
        }

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

        public string ShowData() => dataPage switch
        {
            DataPages.LocalMap => "LOCAL MAP GOES HERE!!!",
            DataPages.WorldMap => map.ToString(),
            DataPages.Quests => "QUESTS GOES HERE!!!",
            DataPages.Misc => "MISC NOTES GOES HERE!!!",
            DataPages.Radio => radio.ToString()
        };

        #region Console Functions
        /// <summary>
        /// Error message and sound
        /// </summary>
        /// <param name="errorMessage">The error message to display</param>
        public void Error(string errorMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(errorMessage);
            Console.Beep(1000, 750);
            Console.Beep(1000, 750);
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
            Misc,
            Ammo
        }

        public enum DataPages
        {
            LocalMap,
            WorldMap,
            Quests,
            Misc,
            Radio
        }
        #endregion
        #endregion
    }
}
