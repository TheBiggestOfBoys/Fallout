using Pip_Boy.Objects;
using System;
using System.IO;
using System.Text;

namespace Pip_Boy
{
    /// <summary>
    /// Where the main code for the app runs
    /// </summary>
    public class Program
    {
        static void Main()
        {
            PipBoy pipBoy = new(Directory.GetCurrentDirectory() + "\\PIP-Boy\\", ConsoleColor.DarkYellow);
            bool boot = true;
            bool createPlayer = false;

            Console.ForegroundColor = pipBoy.Color;
            Console.Title = "PIP-Boy 3000 MKIV";
            Console.OutputEncoding = Encoding.UTF8;
            if (boot)
            {
                pipBoy.Boot();
            }

            if (createPlayer)
            {
                pipBoy.player = new(pipBoy.activeDirectory);
            }
            else
            {
                pipBoy.player = new("Jake Scott", [5, 6, 7, 8, 9, 3, 4], pipBoy.activeDirectory);
            }

            pipBoy.map.MovePlayer(null, null, pipBoy.player);
        }
    }
}
