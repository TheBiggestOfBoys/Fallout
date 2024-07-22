using Pip_Boy.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Text;
using System.Threading;

namespace Pip_Boy.Objects
{
    /// <summary>
    /// The <c>PIPBoy</c> object, which displays player info, <c>Inventory</c>, and other data.
    /// It controls output and error handling
    /// </summary>
    /// <param name="workingDirectory">The directory to load items, sounds, songs, and player info from</param>
    /// <param name="color">The display color</param>
    public class PipBoy(string workingDirectory, ConsoleColor color)
    {
        #region Objects
        /// <summary>
        /// The <c>Player</c> object tied to the PIP-Boy.
        /// </summary>
        public Player player = new("Player", [5, 5, 5, 5, 5, 5, 5], workingDirectory);

        /// <summary>
        /// Controls music.
        /// </summary>
        public Radio radio = new(workingDirectory + "Songs\\");

        /// <summary>
        /// Displays points of interest.
        /// </summary>
        public Map map = new(25, 50, 20);

        /// <summary>
        /// Controls <c>PIPBoy</c> sound effects.
        /// </summary>
        public SoundPlayer soundEffects = new();
        #endregion

        #region Lists
        /// <summary>
        /// A list of all unfinished quests, it can be added to, and quests will be removed and added to the `finishedQuests`, once finished.
        /// </summary>
        public List<Quest> quests = [new("Finish the game", [new("Do stuff", new(16, 16), false), new("Do things", new(32, 32), false)])];
        /// <summary>
        /// A list of all finished quests, which will grow.
        /// </summary>
        public List<Quest> finishedQuests = [];

        /// <summary>
        /// Data items collected
        /// </summary>
        public List<Data> miscData = [];

        /// <summary>
        /// An array of all factions.  Descriptions are taken from the Fallout Wiki (phrasing breaks immersion).  I might change these later.
        /// </summary>
        public Faction[] factions = [
            new("New California Republic", "The New California Republic (NCR) is a post-War federal republic founded in New California in 2189. It is comprised of five contiguous states located in southern California, with additional territorial holdings in northern California, Oregon and Nevada."),
            new("Caesar's Legion", "Also referred to simply as The Legion, is an imperialistic slaver society and totalitarian dictatorship founded in 2247 by Edward 'Caesar' Sallow and Joshua Graham, built on the conquest and enslavement of tribal societies in the American southwest. To enforce unity in the absence of any civilian institutions, the Legion loosely models itself after the military of the Roman Empire, repurposing its language and aesthetics for the post-apocalypse."),
            new("Brotherhood of Steel", "The Brotherhood of Steel (commonly abbreviated to BoS) is a post-War technology-focused paramilitary order with chapters operating across the territory of the former United States. Founded by rogue U.S. Army officer Captain Roger Maxson shortly after the Great War, the Brotherhood's core purpose is to preserve advanced technology and regulate its usage. Despite often being relatively isolationist, the Brotherhood has proved to be one of the most important organizations in the history of the wasteland, though their exact levels of power and influence have varied over time and by chapter."),
            new("Boomers", "The Boomers are a tribe formed out of a group of vault dwellers who originally inhabited Vault 34. The overstocked, unprotected armory led to the emergence of a particularly gun-centric culture among the dwellers. The Boomers were a group particularly obsessed with weapons and the right to keep and bear them freely. When the population of the Vault ballooned in the early 23rd century, the Vault's overseer attempted to salvage the situation by introducing population control measures and sealing the armory. The efforts backfired, as rioting began, quickly turning into full-out rebellion. The future Boomers attacked the armory, taking most of the heavy weapons and equipment, then fought their way out of the vault. The reactor was damaged in the attack, dooming the vault."),
            new("Great Khans", "The Khans were a raider tribe descended from one of the three raider clans that originated from Vault 15. They have a long and turbulent history and have been nearly wiped out on three occasions. After the original Khans faced near-extinction following the Vault Dweller's actions, they were succeeded by the New Khans and then the Great Khans."),
            new("Followers of the Apocalypse", "The Followers of the Apocalypse, or simply the Followers, are a humanitarian organization originating in New California. Followers focus on providing education and medical services to those in need, as well as furthering research in non-military areas. Once allies of the New California Republic, they have since parted ways due to disagreements over NCR foreign policy."),
            new("Powder Gangers", "The Powder Gangers (also referred to as Powder Gangsters by Johnson Nash) are a gang of escaped prisoners operating in the Mojave Wasteland in 2281."),
            new("The Strip", "The New Vegas Strip is a part of New Vegas in the Mojave Wasteland in 2281."),
            new("Free Side", "Free Side is a district of New Vegas in Fallout: New Vegas.")
        ];

        /// <summary>
        /// The current index of the selected <c>Faction</c> in the <c>General</c> sub page of the <c>STAT</c> page
        /// </summary>
        public byte factionIndex = 0;

        #region Sounds
        /// <summary>
        /// Sound
        /// </summary>
        public string[] sounds = Directory.GetFiles(workingDirectory + "Sounds\\", "*.wav");

        /// <summary>
        /// Sounds for static between songs and menu navigation
        /// </summary>
        public string[] staticSounds = Directory.GetFiles(workingDirectory + "Sounds\\static\\", "*wav");

        /// <summary>
        /// Geiger click sounds, for when in the RAD menu
        /// </summary>
        public string[] radiationSounds = Directory.GetFiles(workingDirectory + "Sounds\\radiation\\", "*wav");
        #endregion
        #endregion

        /// <summary>
        /// The directory from which files will be loaded and saved
        /// </summary>
        public readonly string activeDirectory = workingDirectory;

        /// <summary>
        /// The color of the <c>PIPBoy</c>'s text
        /// </summary>
        public readonly ConsoleColor Color = color;

        /// <summary>
        /// Displays Fake OS Boot screen info
        /// </summary>
        public void Boot()
        {
            PlaySound(sounds[2]);

            SlowType("PIP-Boy 3000 MKIV");
            SlowType("Copyright 2075 RobCo Industries");
            SlowType("64kb Memory");
            SlowType(new string('-', Console.WindowWidth));

            PlaySound(sounds[6]);

            Console.Clear();
            Console.WriteLine();
            SlowType("VAULT-TEC");
            Thread.Sleep(1250);

            Console.Clear();
            SlowType("LOADING...");
            Thread.Sleep(2500);
            Console.Clear();

            PlaySound(sounds[8]);
        }

        #region Page Info
        /// <summary>
        /// The current main page
        /// </summary>
        public Pages currentPage = Pages.STATS;

        /// <summary>
        /// The current STAT sub-page
        /// </summary>
        public StatsPages statPage = StatsPages.Status;

        /// <summary>
        /// The current DATA sub-page
        /// </summary>
        public DataPages dataPage = DataPages.Map;
        #endregion

        #region Menu Navigation
        /// <summary>
        /// Changes the current menu page
        /// </summary>
        /// <param name="right">Move right?</param>
        public void ChangeMenu(bool right)
        {
            PlaySound(sounds[^1]);
            if (right && currentPage < Pages.DATA)
            {
                currentPage++;
            }
            if (!right && currentPage > Pages.STATS)
            {
                currentPage--;
            }
        }

        /// <summary>
        /// Changes the current sub menu page
        /// </summary>
        /// <param name="right">Move right?</param>
        public void ChangeSubMenu(bool right)
        {
            PlaySound(sounds[^1]);

            switch (currentPage)
            {
                case Pages.STATS when right && statPage < StatsPages.General:
                    statPage++;
                    break;
                case Pages.STATS when !right && statPage > StatsPages.Status:
                    statPage--;
                    break;

                case Pages.ITEMS when right && player.Inventory.itemPage < Inventory.ItemsPages.Misc:
                    player.Inventory.itemPage++;
                    break;
                case Pages.ITEMS when !right && player.Inventory.itemPage > Inventory.ItemsPages.Weapons:
                    player.Inventory.itemPage--;
                    break;

                case Pages.DATA when right && dataPage < DataPages.Radio:
                    dataPage++;
                    break;
                case Pages.DATA when !right && dataPage > DataPages.Map:
                    dataPage--;
                    break;
            }
        }

        /// <summary>
        /// Changes the selected faction, in order to show their description
        /// </summary>
        /// <param name="up">Move up the list</param>
        public void ChangeSelectedFaction(bool up)
        {
            if (!up && factionIndex > 0)
            {
                factionIndex--;
                PlaySound(sounds[^12]);
            }

            if (up && factionIndex < factions.Length - 1)
            {
                factionIndex++;
                PlaySound(sounds[^13]);
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
            Pages.ITEMS => player.Inventory.ToString(),
            Pages.DATA => ShowData(),
            _ => throw new NotImplementedException()
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
                Pages.ITEMS => typeof(Inventory.ItemsPages),
                Pages.DATA => typeof(DataPages),
                _ => throw new NotImplementedException()
            };
            foreach (Enum subPage in Enum.GetValues(enumType))
            {
                footer.Add(subPage.ToString());
            }

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
                if (item == statPage.ToString() || item == player.Inventory.itemPage.ToString() || item == dataPage.ToString())
                {
                    Highlight(item, false);
                }
                else
                {
                    Console.Write(item);
                }
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
            StatsPages.General => ShowFactions(),
            _ => throw new NotImplementedException()
        };

        /// <summary>
        /// Show all the factions and there statuses
        /// </summary>
        /// <returns>A table of the factions</returns>
        public string ShowFactions()
        {
            StringBuilder stringBuilder = new();
            foreach (Faction faction in factions)
            {
                stringBuilder.AppendLine(faction.ToString());
            }
            return stringBuilder.ToString() + Environment.NewLine + factions[factionIndex].Description;
        }

        /// <summary>
        /// Logic behind showing the Data Page's submenus
        /// </summary>
        /// <returns>The corresponding string</returns>
        public string ShowData() => dataPage switch
        {
            DataPages.Map => map.ToString() + Environment.NewLine + "Key: " + Map.GenerateLegend(),
            DataPages.Quests => ShowQuests(),
            DataPages.Misc => ShowDataNotes(),
            DataPages.Radio => radio.ToString(),
            _ => throw new NotImplementedException()
        };

        /// <summary>
        /// Shows all active quests and their steps
        /// </summary>
        /// <returns>A table of all quests and their steps</returns>
        public string ShowQuests()
        {
            StringBuilder stringBuilder = new();
            foreach (Quest quest in quests)
            {
                stringBuilder.AppendLine(quest.ToString());
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Shows all Data Notes
        /// </summary>
        /// <returns>A table of all data notes and recordings</returns>
        public string ShowDataNotes()
        {
            StringBuilder stringBuilder = new();
            foreach (Data data in miscData)
            {
                stringBuilder.AppendLine(data.ToString());
            }
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
            Console.ForegroundColor = Color;
        }

        /// <summary>
        /// Highlights a message in the <c>Console</c>.
        /// </summary>
        /// <param name="message">The message to highlight</param>
        /// /// <param name="newLine">Whether to start a new line or not.</param>
        public void Highlight(string message, bool newLine)
        {
            Console.BackgroundColor = Color;
            Console.ForegroundColor = ConsoleColor.Black;
            if (newLine)
            {
                Console.WriteLine(message);
            }
            else
            {
                Console.Write(message);
            }
            Console.ResetColor();
            Console.ForegroundColor = Color;
        }

        /// <summary>
        /// Slowly Type out a message to the console
        /// </summary>
        /// <param name="message">The message</param>
        public static void SlowType(string message)
        {
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(10);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Play a <c>PIPBoy</c> sound-effect
        /// </summary>
        /// <param name="path">The path to the <c>*.wav</c> file.</param>
        public void PlaySound(string path)
        {
            soundEffects.SoundLocation = path;
            soundEffects.Load();
            soundEffects.Play();
        }
        #endregion

        #region Enums
        /// <summary>
        /// Main Menu pages which display different things
        /// </summary>
        public enum Pages
        {
            /// <summary>
            /// The <c>Player</c>'s stats.
            /// </summary>
            STATS,
            /// <summary>
            /// The <c>Inventory</c>.
            /// </summary>
            ITEMS,
            /// <summary>
            /// The <c>Data</c> sub-page.
            /// </summary>
            DATA
        }

        #region SubPages
        /// <summary>
        /// STAT sub-menu pages which displays different things
        /// </summary>
        public enum StatsPages
        {
            /// <summary>
            /// The <c>Player</c>'s actives <c>Effect</c>s
            /// </summary>
            Status,
            /// <summary>
            /// The <c>Player</c>'s SPECIAL Stats
            /// </summary>
            SPECIAL,
            /// <summary>
            /// The <c>Player</c>'s <c>Skill</c>s
            /// </summary>
            Skills,
            /// <summary>
            /// The <c>Player</c>'s <c>Perk</c>s
            /// </summary>
            Perks,
            /// <summary>
            /// The <c>Player</c>'s Reputations with other <c>Faction</c>s
            /// </summary>
            General
        }

        /// <summary>
        /// DATA sub-menu pages which display different things
        /// </summary>
        public enum DataPages
        {
            /// <summary>
            /// The <c>Map</c> object with it's markers and legends.
            /// </summary>
            Map,
            /// <summary>
            /// The <c>List</c> of active <c>Quest</c>s.
            /// </summary>
            Quests,
            /// <summary>
            /// The <c>Data</c> entries in the form of <c>*.txt</c> and <c>*.wav</c> files.
            /// </summary>
            Misc,
            /// <summary>
            /// The <c>Radio</c> object and it's <c>List</c> of <c>Songs</c>.
            /// </summary>
            Radio
        }
        #endregion
        #endregion
    }
}
