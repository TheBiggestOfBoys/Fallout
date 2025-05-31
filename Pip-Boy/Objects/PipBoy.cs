using Pip_Boy.Data_Types;
using Pip_Boy.Entities;
using Pip_Boy.Entities.Creatures;
using Pip_Boy.Entities.Mutants;
using Pip_Boy.Entities.Robots;
using Pip_Boy.Items;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Xml;

namespace Pip_Boy.Objects
{
	/// <summary>
	/// Displays <see cref="Player"/> info, <see cref="Inventory"/>, and other data.
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
		public List<Quest> quests = [];

		/// <summary>
		/// A list of all finished quests, which will grow.
		/// </summary>
		public List<Quest> finishedQuests = [];

		/// <summary>
		/// An array of all factions.  Descriptions are taken from the Fallout Wiki (phrasing breaks immersion).  I might change these later.
		/// </summary>
		public Faction[] factions = [];

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

		#region Directories
		/// <summary>
		/// The directory from which files will be loaded and saved
		/// </summary>
		public readonly string activeDirectory;

		/// <summary>
		/// The directory from which data files will be loaded and saved
		/// </summary>
		public readonly string dataDirectory;

		/// <summary>
		/// The directory from which factions will be loaded and saved
		/// </summary>
		public readonly string factionDirectory;
		#endregion

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
			dataDirectory = workingDirectory + "Data\\";
			factionDirectory = workingDirectory + "Factions\\";

			string[] filePaths = Directory.GetFiles(factionDirectory, "*.txt");
			factions = new Faction[filePaths.Length];
			// Factions
			for (int i = 0; i < filePaths.Length; i++)
			{
				factions[i] = new(Path.GetFileNameWithoutExtension(filePaths[i]), File.ReadAllText(filePaths[i]));
			}

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

			Console.ForegroundColor = Color;
			Console.Title = "PIP-Boy 3000 MKIV";
			Console.OutputEncoding = Encoding.UTF8;

			if (boot)
			{
				Boot();
			}

			try
			{
				playerFilePath = Directory.GetFiles(activeDirectory, "*.xml")[0];
				player = FromFile<Player>(playerFilePath);
				player.Inventory = new(activeDirectory + "Inventory\\", player);
			}
			catch (IndexOutOfRangeException)
			{
				player = Player.CreatePlayer(activeDirectory);
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

				key = Console.ReadKey(true).Key;

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

					#region Object Creation (for testing)
					case ConsoleKey.L:
						Console.Write("Enter the type of object to create (e.g., Weapon, Player, Ghoul): ");
						string enteredType = Console.ReadLine();
						Type type = GetSerializableTypeByName(enteredType);
						object createdObject = CreateFromInput(type);
						string filePath = ToFile(Directory.GetCurrentDirectory(), createdObject);
						Console.WriteLine($"Created object of type {type.Name} and saved to file: {filePath}");
						Console.WriteLine($"Press {ConsoleKey.Enter} to continue: ");
						Console.ReadLine();
						break;
						#endregion
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
		/// <param name="collection">The array of objects</param>
		/// <param name="header">The title of the list</param>
		/// <returns>A string representation of all items in an array.</returns>
		public static string DisplayCollection<T>(string header, IEnumerable<T> collection) =>
			header + ':'
			+ Environment.NewLine
			+ string.Join(Environment.NewLine + '\t', collection);

		/// <summary>
		/// Logic behind showing the Stats Page's submenus
		/// </summary>
		/// <returns>The corresponding string</returns>
		public string ShowStats() => statPage switch
		{
			StatsPages.Status => player.ShowStatus(),
			StatsPages.SPECIAL => DisplayCollection(nameof(player.SPECIAL), player.SPECIAL),
			StatsPages.Skills => DisplayCollection(nameof(player.Skills), player.Skills),
			StatsPages.Perks => DisplayCollection(nameof(player.Perks), player.Perks),
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
			foreach (string data in Directory.GetFiles(dataDirectory))
			{
				string extension = Path.GetExtension(data);
				string icon = extension switch
				{
					".txt" => "📄",
					".wav" => "🔊",
					_ => "?",
				};

				stringBuilder.AppendLine(icon + ' ' + Path.GetFileNameWithoutExtension(data));
				stringBuilder.AppendLine(new string('-', 10));
				if (extension == ".txt")
				{
					stringBuilder.AppendLine(File.ReadAllText(data));
				}
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
		/// Prompts the user to enter a value of the specified type.
		/// </summary>
		/// <typeparam name="T">The type of the value to be entered.</typeparam>
		/// <param name="message">The message to display before the input prompt.</param>
		/// <returns>The entered value converted to the specified type.</returns>
		public static T EnterValue<T>(string message) where T : IConvertible
		{
			Type type = typeof(T);
			T variable = default;
			bool isValid = false;
			string promptMessage = $"{message} ({type.Name}";

			if (type != typeof(bool) && type != typeof(string))
			{
				T minValue = (T)Convert.ChangeType(type.GetField("MinValue").GetValue(null), typeof(T));
				T maxValue = (T)Convert.ChangeType(type.GetField("MaxValue").GetValue(null), typeof(T));
				promptMessage += $", Min: {minValue}, Max: {maxValue}";
			}
			promptMessage += "): ";

			do
			{
				Console.Write(promptMessage);
				string input = Console.ReadLine();
				try
				{
					if (type == typeof(bool))
					{
						if (bool.TryParse(input, out bool boolResult))
						{
							variable = (T)Convert.ChangeType(boolResult, typeof(T));
							isValid = true;
						}
						else
						{
							Console.Error.WriteLine($"Invalid input. Please enter a valid {type.Name} (true/false).");
						}
					}
					else
					{
						variable = (T)Convert.ChangeType(input, typeof(T));
						isValid = true;
					}
				}
				catch
				{
					Console.Error.WriteLine($"Invalid input. Please enter a valid {type.Name}.");
				}
			} while (!isValid);

			return variable;
		}

		/// <summary>
		/// Creates an instance of the specified <paramref name="type"/> by prompting the user to enter values for each constructor parameter.
		/// </summary>
		/// <param name="type">The type of object to create. Must have a public constructor.</param>
		/// <returns>
		/// An instance of <paramref name="type"/> with its constructor parameters set to the values entered by the user.
		/// </returns>
		public static object CreateFromInput(Type type)
		{
			ArgumentNullException.ThrowIfNull(type);

			// Get the constructor with the most parameters (prefer the most complete one)
			ConstructorInfo ctor = type.GetConstructors()
			.OrderByDescending(c => c.GetParameters().Length)
			.FirstOrDefault() ?? throw new InvalidOperationException($"No public constructor found for type {type.FullName}.");
			ParameterInfo[] parameters = ctor.GetParameters();
			object[] args = new object[parameters.Length];

			for (int i = 0; i < parameters.Length; i++)
			{
				ParameterInfo param = parameters[i];
				Type paramType = param.ParameterType;
				object value;

				// Use EnterValue<T> for primitives and strings, otherwise fallback to recursion for complex types
				if (paramType.IsPrimitive || paramType == typeof(string) || paramType == typeof(decimal))
				{
					MethodInfo enterValueMethod = typeof(PipBoy).GetMethod(nameof(EnterValue)).MakeGenericMethod(paramType);
					value = enterValueMethod.Invoke(null, [$"Enter value for {param.Name}"]);
				}
				else if (paramType.IsEnum)
				{
					Console.WriteLine($"Select {param.Name} ({paramType.Name}):");
					string[] names = Enum.GetNames(paramType);
					for (int j = 0; j < names.Length; j++)
						Console.WriteLine($"{j}: {names[j]}");
					int selected = -1;
					while (selected < 0 || selected >= names.Length)
					{
						Console.Write("Enter number: ");
						string input = Console.ReadLine();
						if (int.TryParse(input, out selected) && selected >= 0 && selected < names.Length)
							break;
						Console.WriteLine("Invalid selection.");
					}
					value = Enum.Parse(paramType, names[selected]);
				}
				else
				{
					Console.WriteLine($"Creating value for parameter '{param.Name}' of type '{paramType.Name}'...");
					value = CreateFromInput(paramType);
				}

				args[i] = value;
			}

			return ctor.Invoke(args);
		}

		/// <summary>
		/// Returns the corresponding Type for a given serializable item/entity name.
		/// </summary>
		/// <param name="typeName">The name of the type (e.g., "Weapon", "Player", "Ghoul").</param>
		/// <returns>The Type if found; otherwise, null.</returns>
		public static Type? GetSerializableTypeByName(string typeName)
		{
			// List of serializable base types
			Type[] baseTypes = [
				typeof(Item),
				typeof(Entity),
				typeof(Perk),
				typeof(Location),
				typeof(Quest),
				typeof(Effect),
				typeof(Faction)
			];

			// Gather all non-abstract subclasses for each base type
			foreach (Type baseType in baseTypes)
			{
				// If the base type matches the name
				if (baseType.Name.Equals(typeName, StringComparison.OrdinalIgnoreCase))
				{
					List<Type> subtypes = [.. GetAllSubtypesOf(baseType)];
					if (subtypes.Count == 0)
						Console.WriteLine($"No non-abstract subclasses found for '{baseType.Name}'.");
					else
					{
						Console.WriteLine($"Subclasses of {baseType.Name}:");
						foreach (Type subtype in subtypes)
							Console.WriteLine($"- {subtype.FullName}");
					}
					return baseType;
				}

				// If a subclass matches the name
				List<Type> subtypesList = [.. GetAllSubtypesOf(baseType)];
				Type? subtypeMatch = subtypesList.FirstOrDefault(t => t.Name.Equals(typeName, StringComparison.OrdinalIgnoreCase));
				if (subtypeMatch != null)
				{
					Console.WriteLine($"Subclasses of {baseType.Name}:");
					foreach (Type subtype in subtypesList)
						Console.WriteLine($"- {subtype.FullName}");
					return subtypeMatch;
				}
			}

			Console.WriteLine($"Type '{typeName}' not found among serializable base types or their subclasses.");
			return null;
		}

		/// <summary>
		/// Gets all non-abstract subclasses of the specified base type from its assembly.
		/// </summary>
		/// <param name="baseType">The base type to find subclasses of.</param>
		/// <returns>An enumerable of all non-abstract subclasses of <paramref name="baseType"/>.</returns>
		public static IEnumerable<Type> GetAllSubtypesOf(Type baseType)
		{
			return Assembly.GetAssembly(baseType)
			.GetTypes()
			.Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(baseType));
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
					Location location => location.Name,
					_ => throw new Exception("Object is invalid type!")
				};

				if (name == string.Empty || name is null)
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
				writer.WriteProcessingInstruction("xml-stylesheet", "type=\"text/css\" href=\"..\\Inventory Styling.css\"");
				x.WriteObject(writer, obj);
				writer.Close();

				return filePath;
			}
			throw new DirectoryNotFoundException("Folder not found. " + folderPath);
		}

		/// <summary>
		/// Deserializes an object from an <c>*.xml</c> file.
		/// </summary>
		/// <typeparam name="T">The type to deserialize to</typeparam>
		/// <param name="filePath">The path to the <c>*.xml</c> file.</param>
		/// <returns>The deserialized object.</returns>
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
						IgnoreWhitespace = true,
						IgnoreComments = true,
						IgnoreProcessingInstructions = true,
						CheckCharacters = true,
						DtdProcessing = DtdProcessing.Ignore,
						ValidationType = ValidationType.None,
						CloseInput = true,
					};

					using XmlReader reader = XmlReader.Create(filePath, readerSettings);
					return (T)x.ReadObject(reader) ?? throw new NullReferenceException("Deserialized object is null.");
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
					return typeName switch
					{
						#region Items
						"Weapon" => typeof(Weapon),
						"HeadPiece" => typeof(HeadPiece),
						"TorsoPiece" => typeof(TorsoPiece),
						"Aid" => typeof(Aid),
						"Ammo" => typeof(Ammo),
						"Misc" => typeof(Misc),
						#endregion
						#region Entities
						"Player" => typeof(Player),
						"Human" => typeof(Human),
						"Robot" => typeof(Robot),
						#region Mutants
						"Ghoul" => typeof(Ghoul),
						"Feral" => typeof(Feral),
						"SuperMutant" => typeof(SuperMutant),
						"Nightkin" => typeof(Nightkin),
						#endregion
						#region Creatures
						"Dog" => typeof(Dog),
						"NightStalker" => typeof(NightStalker),
						"BloatFly" => typeof(BloatFly),
						"DeathClaw" => typeof(DeathClaw),
						#endregion
						#endregion
						_ => throw new TypeLoadException("Invalid Type!")
					};
				}
				throw new FormatException("File is not '*.xml'!");
			}
			throw new FileNotFoundException("File not found.", filePath);
		}
		#endregion
		#endregion

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
