using Pip_Boy.Objects;
using System;
using System.IO;

namespace Pip_Boy
{
	/// <summary>
	/// Where the main code for the app runs
	/// </summary>
	public class Program
	{
		static void Main()
		{
			string folderPath = Directory.GetCurrentDirectory() + "\\PIP-Boy\\";
			PipBoy pipBoy = new(folderPath, ConsoleColor.DarkYellow, false);

			pipBoy.MainLoop();
		}
	}
}
