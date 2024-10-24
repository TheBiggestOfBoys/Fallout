using Pip_Boy.Items;
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
            _ = PipBoy.ToFile<Weapon>("C:\\Users\\jrsco\\source\\repos\\Pip-Boy\\Pip-Boy\\PIP-Boy\\Inventory\\Weapon\\", new Weapon("10mm Pistol", 5.5f, 55, [], Weapon.WeaponType.Gun, 3, 20, 10, 100));
            _ = PipBoy.ToFile<Ammo>("C:\\Users\\jrsco\\source\\repos\\Pip-Boy\\Pip-Boy\\PIP-Boy\\Inventory\\Ammo\\", new Ammo("10mm Ammo", 1, [], Ammo.AmmoType.Bullet, Ammo.AmmoModification.Standard));
            _ = PipBoy.ToFile<Aid>("C:\\Users\\jrsco\\source\\repos\\Pip-Boy\\Pip-Boy\\PIP-Boy\\Inventory\\Aid\\", new Aid("Stimpack", 1f, 30, [], Aid.AidType.Syringe));
            _ = PipBoy.ToFile<HeadPiece>("C:\\Users\\jrsco\\source\\repos\\Pip-Boy\\Pip-Boy\\PIP-Boy\\Inventory\\Apparel\\", new HeadPiece("Baseball Cap", 1f, 5, [], 1, false));
            _ = PipBoy.ToFile<TorsoPiece>("C:\\Users\\jrsco\\source\\repos\\Pip-Boy\\Pip-Boy\\PIP-Boy\\Inventory\\Apparel\\", new TorsoPiece("Vault 13 Jumpsuit", 5f, 25, [], 3, false));
            _ = PipBoy.ToFile<Misc>("C:\\Users\\jrsco\\source\\repos\\Pip-Boy\\Pip-Boy\\PIP-Boy\\Inventory\\Misc\\", new Misc("Journal Entry", 1f, 15, Misc.MiscType.Other));

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
