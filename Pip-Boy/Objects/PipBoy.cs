﻿using Pip_Boy.Data_Types;
using Pip_Boy.Entities;
using Pip_Boy.Items;
using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Xml;

namespace Pip_Boy.Objects
{
    /// <summary>
    /// The <c>PIPBoy</c> object, which displays player info, <c>Inventory</c>, and other data.
    /// It controls output and error handling
    /// </summary>
    public class PipBoy
    {
        #region System Info
        /// <summary>
        /// The Current <see cref="DateOnly"/> and <see cref="TimeOnly"/>.
        /// </summary>
        public DateTime dateTime;
        #endregion

        #region Objects
        /// <summary>
        /// The <c>Player</c> object tied to the PIP-Boy.
        /// </summary>
        public Player player;

        /// <summary>
        /// Controls music.
        /// </summary>
        public Radio radio;

        /// <summary>
        /// Displays points of interest.
        /// </summary>
        public Map map;

        /// <summary>
        /// Controls <c>PIPBoy</c> sound effects.
        /// </summary>
        public SoundPlayer soundEffects;
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
        public string[] miscData = [];

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
        /// The current index of the selected <see cref="Faction"/> in the <c>General</c> sub page of the <c>STAT</c> page
        /// </summary>
        public byte factionIndex = 0;

        #region Sounds
        /// <summary>
        /// Sound
        /// </summary>
        public string[] sounds;

        /// <summary>
        /// Sounds for static between songs and menu navigation
        /// </summary>
        public string[] staticSounds;

        /// <summary>
        /// Geiger click sounds, for when in the RAD menu
        /// </summary>
        public string[] radiationSounds;
        #endregion
        #endregion

        /// <summary>
        /// The directory from which files will be loaded and saved
        /// </summary>
        public readonly string activeDirectory;

        /// <summary>
        /// The path of the player's <c>*.xml</c> file.
        /// </summary>
        public readonly string playerFilePath;

        /// <summary>
        /// The color of the <c>PIPBoy</c>'s text
        /// </summary>
        public readonly ConsoleColor Color;

        #region Constructors
        /// <param name="workingDirectory">The directory to load items, sounds, songs, and player info from</param>
        /// <param name="color">The display color</param>
        /// <param name="boot">Whether to show the boot screen.</param>
        public PipBoy(string workingDirectory, ConsoleColor color, bool boot)
        {
            // Sounds
            sounds = Directory.GetFiles(workingDirectory + "Sounds\\", "*.wav");
            staticSounds = Directory.GetFiles(workingDirectory + "Sounds\\static\\", "*wav");
            radiationSounds = Directory.GetFiles(workingDirectory + "Sounds\\radiation\\", "*wav");

            radio = new(workingDirectory + "Songs\\");
            map = new(25, 50, "PIP-Boy\\Map Locations\\");
            soundEffects = new();

            activeDirectory = workingDirectory;
            dateTime = DateTime.Now;
            Color = color;
            playerFilePath = Directory.GetFiles(activeDirectory, "*.xml")[0];

            Console.ForegroundColor = Color;
            Console.Title = "PIP-Boy 3000 MKIV";
            Console.OutputEncoding = Encoding.UTF8;

            if (boot)
            {
                Boot();
            }

            if (Directory.GetFiles(activeDirectory, "*.xml").Length == 0)
            {
                player = Player.CreatePlayer(activeDirectory);
            }
            else
            {
                player = FromFile<Player>(playerFilePath);
                player.Inventory = new(activeDirectory + "Inventory\\", player);
            }
            map.MovePlayer(null, null, player);
        }
        #endregion

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
        /// The main loop that controls the PIP-Boy with keyboard input
        /// </summary>
        public void MainLoop()
        {
            ConsoleKey key = ConsoleKey.Escape;
            while (key != ConsoleKey.Q)
            {
                Console.Clear();

                Highlight(currentPage.ToString(), true);
                Console.WriteLine();

                Console.WriteLine(ShowMenu());
                Console.WriteLine();

                ShowSubMenu(GetSubMenu());

                key = Console.ReadKey().Key;

                switch (key)
                {
                    #region Menu
                    case ConsoleKey.A:
                        ChangeMenu(false);
                        break;
                    case ConsoleKey.D:
                        ChangeMenu(true);
                        break;
                    #endregion

                    #region Sub-Menu
                    case ConsoleKey.LeftArrow:
                        ChangeSubMenu(false);
                        break;
                    case ConsoleKey.RightArrow:
                        ChangeSubMenu(true);
                        break;

                    case ConsoleKey.UpArrow when currentPage == Pages.STATS && statPage == StatsPages.General:
                        ChangeSelectedFaction(false);
                        break;
                    case ConsoleKey.DownArrow when currentPage == Pages.STATS && statPage == StatsPages.General:
                        ChangeSelectedFaction(true);
                        break;
                    #endregion

                    #region Radio
                    case ConsoleKey.Enter when currentPage == Pages.DATA && dataPage == DataPages.Radio:
                        radio.Play();
                        break;

                    case ConsoleKey.Add when currentPage == Pages.DATA && dataPage == DataPages.Radio:
                        radio.AddSong(this);
                        break;

                    case ConsoleKey.UpArrow when radio.songIndex > 0:
                        radio.ChangeSong(false);
                        radio.Play();
                        break;
                    case ConsoleKey.DownArrow when radio.songIndex < radio.songs.Length:
                        radio.ChangeSong(true);
                        radio.Play();
                        break;
                    #endregion

                    #region Map
                    case ConsoleKey.NumPad8 when currentPage == Pages.DATA && dataPage == DataPages.Map:
                        map.MovePlayer(true, null, player);
                        break;
                    case ConsoleKey.NumPad2 when currentPage == Pages.DATA && dataPage == DataPages.Map:
                        map.MovePlayer(false, null, player);
                        break;
                    case ConsoleKey.NumPad4 when currentPage == Pages.DATA && dataPage == DataPages.Map:
                        map.MovePlayer(null, false, player);
                        break;
                    case ConsoleKey.NumPad6 when currentPage == Pages.DATA && dataPage == DataPages.Map:
                        map.MovePlayer(null, true, player);
                        break;
                    #endregion

                    case ConsoleKey.L:
                        Spawner.Prompt();
                        break;
                }
            }
            Shutdown();
        }

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
            StatsPages.Perks => player.ShowPerks(),
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
            DataPages.Map => map.ToString(),
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
            foreach (string data in miscData)
            {
                stringBuilder.AppendLine(Path.GetFileNameWithoutExtension(data));
                stringBuilder.AppendLine(new string('-', 10));
                stringBuilder.AppendLine(File.ReadAllText(data));
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
            Console.Beep();
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
                Thread.Sleep(5);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Play a <see cref="PipBoy"/> sound-effect
        /// </summary>
        /// <param name="path">The path to the <c>*.wav</c> file.</param>
        public void PlaySound(string path)
        {
            soundEffects.SoundLocation = path;
            soundEffects.Load();
            soundEffects.Play();
        }

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

        /// <summary>
        /// Write all data to files before deletion.
        /// </summary>
        public void Shutdown()
        {
            PlaySound(sounds[2]);

            SlowType("Shutting Down...");
            SlowType(new string('-', Console.WindowWidth));
            Thread.Sleep(1000);

            player.SavePlayerPerks();
            player.Inventory.Save();
            ToFile(activeDirectory, player);
        }
        #endregion

        #region File Stuff
        #region To File
        /// <summary>
        /// Serializes the <see cref="object"/> to an <c>*.xml</c> file.
        /// </summary>
        /// <param name="folderPath">The folder to write the <c>*.xml</c> file to.</param>
        /// <param name="obj">The <see cref="object"/> to serialize.</param>
        public static string ToFile(string folderPath, object obj)
        {
            if (Directory.Exists(folderPath))
            {
                Type type = obj.GetType();
                string name = obj switch
                {
                    Item item => item.Name,
                    Entity entity => entity.Name,
                    Perk perk => perk.Name,
                    _ => throw new Exception("Object is invalid type!")
                };

                if (name == string.Empty)
                {
                    name = type.Name;
                }

                string filePath = folderPath + name + ".xml";
                DataContractSerializer x = new(type);

                XmlWriterSettings writerSettings = new()
                {
                    Indent = true,              // Indent elements for readability
                    IndentChars = "\t",         // Use tabs for indentation
                    NewLineChars = "\n",        // Use newline for element endings
                    NewLineHandling = NewLineHandling.Replace, // Standardize newline handling
                    OmitXmlDeclaration = false, // Include XML declaration
                    NewLineOnAttributes = false, // Keep attributes on the same line
                    CloseOutput = true,
                };

                XmlWriter writer = XmlWriter.Create(filePath, writerSettings);
                x.WriteObject(writer, obj);
                writer.Close();
                return filePath;
            }
            throw new DirectoryNotFoundException("Folder not found. " + folderPath);
        }
        #endregion

        #region From File
        /// <summary>
        /// Deserializes an <see cref="Entity"/> object from an <c>*.xml</c> file.
        /// </summary>
        /// <typeparam name="T">The <see cref="Entity"/> sub-class type to serialize to</typeparam>
        /// <param name="filePath">The path to the <c>*.xml</c> file.</param>
        /// <returns>The deserialized <see cref="Entity"/> object.</returns>
        /// <exception cref="NullReferenceException">If the <c>*.xml</c> file returns a null object.</exception>
        public static T FromFile<T>(string filePath)
        {
            if (File.Exists(filePath))
            {
                if (Path.GetExtension(filePath) == ".xml")
                {
                    DataContractSerializer x = new(typeof(T));

                    XmlReaderSettings readerSettings = new()
                    {
                        IgnoreWhitespace = true,     // Ignore insignificant whitespace
                        IgnoreComments = true,       // Ignore comments in the XML
                        IgnoreProcessingInstructions = true, // Ignore processing instructions
                        CheckCharacters = true,      // Ensure valid XML characters
                        DtdProcessing = DtdProcessing.Ignore, // Disable DTD processing for security
                        ValidationType = ValidationType.None, // Change to Schema if XML schema validation is needed
                        CloseInput = true,
                    };

                    XmlReader reader = XmlReader.Create(filePath, readerSettings);
                    return (T)x.ReadObject(reader);
                }
                throw new FileLoadException("File is not '*.xml'. ", filePath);
            }
            throw new FileNotFoundException("File not found. ", filePath);
        }

        /// <summary>
        /// Reads the root tag of an <c>*.xml</c> file.
        /// </summary>
        /// <param name="filePath">The path to the file</param>
        /// <returns>The <see cref="Type"/> from the tag name.</returns>
        /// <exception cref="NullReferenceException">If no head object tag is found.</exception>
        /// <exception cref="FormatException">IF the file is no <c>*.xml</c>.</exception>
        public static Type GetTypeFromXML(string filePath)
        {
            if (File.Exists(filePath))
            {
                if (Path.GetExtension(filePath) == ".xml")
                {
                    XmlDocument doc = new();
                    doc.Load(filePath);
                    string typeName = doc.DocumentElement?.LocalName ?? throw new NullReferenceException("No head object tag found!");
                    return Type.GetType("Pip_Boy.Items." + typeName, true);
                }
                throw new FormatException("File is not '*.xml'!");
            }
            throw new FileNotFoundException("File not found.", filePath);
        }
        #endregion
        #endregion

        /// <summary>
        /// Enter the values to create an object from the console.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> of <see cref="object"/> to construct.</typeparam>
        /// <returns>The <see cref="object"/> of the given <see cref="Type"/>.</returns>
        public static T CreateObject<T>() where T : new()
        {
            T obj = new();
            foreach (PropertyInfo prop in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (prop.CanWrite)
                {
                    Console.WriteLine($"Enter the value for {prop.Name} ({prop.PropertyType}):");
                    string? input = Console.ReadLine();
                    object? value = Convert.ChangeType(input, prop.PropertyType);
                    prop.SetValue(obj, value);
                }
            }
            return obj;
        }

        #region Enums
        /// <summary>
        /// Main Menu pages which display different things
        /// </summary>
        public enum Pages
        {
            /// <summary>
            /// The <see cref="Player"/>'s stats.
            /// </summary>
            STATS,
            /// <summary>
            /// The <see cref="Inventory"/>.
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
            /// The <see cref="Player"/>'s actives <see cref="Effect"/>s.
            /// </summary>
            Status,
            /// <summary>
            /// The <see cref="Player"/>'s SPECIAL <see cref="Attribute"/>s.
            /// </summary>
            SPECIAL,
            /// <summary>
            /// The <see cref="Player"/>'s skill <see cref="Attribute"/>s.
            /// </summary>
            Skills,
            /// <summary>
            /// The <see cref="Player"/>'s <see cref="Perk"/>s.
            /// </summary>
            Perks,
            /// <summary>
            /// The <see cref="Player"/>'s Reputations with other <see cref="Faction"/>s.
            /// </summary>
            General
        }

        /// <summary>
        /// DATA sub-menu pages which display different things
        /// </summary>
        public enum DataPages
        {
            /// <summary>
            /// The <see cref="Objects.Map"/> object with it's markers and legends.
            /// </summary>
            Map,
            /// <summary>
            /// The <see cref="List{Quest}"/> of active <see cref="Quest"/>s.
            /// </summary>
            Quests,
            /// <summary>
            /// The <c>Data</c> entries in the form of <c>*.txt</c> and <c>*.wav</c> files.
            /// </summary>
            Misc,
            /// <summary>
            /// The <see cref="Radio"/> object and it's <see cref="List{T}"/> of <c>Songs</c> file paths.
            /// </summary>
            Radio
        }
        #endregion
        #endregion
    }
}
