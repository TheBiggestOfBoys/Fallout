using Pip_Boy.Entities.Mutants;
using System;
using System.IO;
using System.Reflection;

namespace Pip_Boy.Objects
{
	public abstract class Spawner
	{
		public static T Spawn<T>() where T : Entities.Entity, new()
		{
			return new T();
		}

		public static void Prompt()
		{
			Console.Write("Enter Key: ");
			Type type;
			const string folder = "C:\\Users\\jrsco\\source\\repos\\Pip-Boy\\Pip-Boy\\Entity Files\\";
			ConsoleKey key = ConsoleKey.Escape;
			while (key != ConsoleKey.Q)
			{
				key = Console.ReadKey(true).Key;
				Console.Clear();
				switch (key)
				{
					case ConsoleKey.S:
						string typeName = PipBoy.EnterValue<string>("Enter Type");

						type = Type.GetType("Pip_Boy.Entities.Mutants." + typeName, true);

						if (type != null && typeof(Entities.Entity).IsAssignableFrom(type))
						{
							MethodInfo method = typeof(Spawner).GetMethod(nameof(Spawn)).MakeGenericMethod(type);
							Entities.Entity entity = (Entities.Entity)method.Invoke(null, null);
							PipBoy.ToFile(folder, entity);
							Console.WriteLine($"Successfully spawned a {typeName}!");
						}
						else
						{
							Console.WriteLine("Invalid type. Make sure the type name is correct and it inherits from Entity.");
						}
						break;

					case ConsoleKey.L:
						string[] files = Directory.GetFiles(folder, "*.xml");
						foreach (string file in files)
						{
							PipBoy.FromFile<Ghoul>(file);
						}
						break;
				}

				key = Console.ReadKey(true).Key; // Pause to see the result
			}
		}
	}
}
