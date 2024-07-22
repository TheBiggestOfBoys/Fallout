using Pip_Boy.Objects;
using System;

namespace Pip_Boy
{
    public class Program
    {
        static void Main()
        {
            PipBoy pipBoy = new("C:\\Users\\jrsco\\source\\repos\\Pip-Boy\\Pip-Boy\\PIP-Boy\\", ConsoleColor.DarkYellow);
            bool boot = false;
            bool createPlayer = false;

            Console.ForegroundColor = pipBoy.Color;
            Console.Title = "PIP-Boy 3000 MKIV";
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

            pipBoy.map.MovePlayer(null, null);

            #region Main Loop
            ConsoleKey key = ConsoleKey.Escape;
            while (key != ConsoleKey.Q)
            {
                Console.Clear();

                pipBoy.Highlight(pipBoy.currentPage.ToString(), true);
                Console.WriteLine();

                Console.WriteLine(pipBoy.ShowMenu());
                Console.WriteLine();

                pipBoy.ShowSubMenu(pipBoy.GetSubMenu());

                key = Console.ReadKey().Key;

                switch (key)
                {
                    #region Menu
                    case ConsoleKey.A:
                        pipBoy.ChangeMenu(false);
                        break;
                    case ConsoleKey.D:
                        pipBoy.ChangeMenu(true);
                        break;
                    #endregion

                    #region Sub-Menu
                    case ConsoleKey.LeftArrow:
                        pipBoy.ChangeSubMenu(false);
                        break;
                    case ConsoleKey.RightArrow:
                        pipBoy.ChangeSubMenu(true);
                        break;

                    case ConsoleKey.UpArrow when pipBoy.currentPage == PipBoy.Pages.STATS && pipBoy.statPage == PipBoy.StatsPages.General:
                        pipBoy.ChangeSelectedFation(false);
                        break;
                    case ConsoleKey.DownArrow when pipBoy.currentPage == PipBoy.Pages.STATS && pipBoy.statPage == PipBoy.StatsPages.General:
                        pipBoy.ChangeSelectedFation(true);
                        break;
                    #endregion

                    #region Radio
                    case ConsoleKey.Enter when pipBoy.currentPage == PipBoy.Pages.DATA && pipBoy.dataPage == PipBoy.DataPages.Radio:
                        pipBoy.radio.Play();
                        break;

                    case ConsoleKey.Add when pipBoy.currentPage == PipBoy.Pages.DATA && pipBoy.dataPage == PipBoy.DataPages.Radio:
                        pipBoy.radio.AddSong(pipBoy);
                        break;

                    case ConsoleKey.UpArrow when pipBoy.radio.songIndex > 0:
                        pipBoy.radio.ChangeSong(false);
                        pipBoy.radio.Play();
                        break;
                    case ConsoleKey.DownArrow when pipBoy.radio.songIndex < pipBoy.radio.songs.Count:
                        pipBoy.radio.ChangeSong(true);
                        pipBoy.radio.Play();
                        break;
                    #endregion

                    #region Map
                    case ConsoleKey.NumPad8 when pipBoy.currentPage == PipBoy.Pages.DATA && pipBoy.dataPage == PipBoy.DataPages.Map:
                        pipBoy.map.MovePlayer(true, null);
                        break;
                    case ConsoleKey.NumPad2 when pipBoy.currentPage == PipBoy.Pages.DATA && pipBoy.dataPage == PipBoy.DataPages.Map:
                        pipBoy.map.MovePlayer(false, null);
                        break;
                    case ConsoleKey.NumPad4 when pipBoy.currentPage == PipBoy.Pages.DATA && pipBoy.dataPage == PipBoy.DataPages.Map:
                        pipBoy.map.MovePlayer(null, false);
                        break;
                    case ConsoleKey.NumPad6 when pipBoy.currentPage == PipBoy.Pages.DATA && pipBoy.dataPage == PipBoy.DataPages.Map:
                        pipBoy.map.MovePlayer(null, true);
                        break;
                        #endregion
                }
            }
            //pipBoy.player.ToFile(pipBoy.activeDirectory);
            pipBoy.player.Inventory.Save();
            #endregion
        }
    }
}
