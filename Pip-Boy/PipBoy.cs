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
        public List<Item> inventory = [];
        public SoundPlayer soundEffects = new();
        #endregion

        public ConsoleColor color;
        public Pages currentPage = Pages.STAT;
        public string[] sounds = [];

        public PipBoy(ConsoleColor color, string folderPath)
        {
            this.color = color;
            sounds = [.. Directory.GetFiles(folderPath, "*.wav")];

            inventory.Add(new("10mm Pistol", "A common weapon in the wasteland", 5.5, 55, Item.Types.WEAPON));
            inventory.Add(new("Sniper Rifle", "A high damage, low fire rate marksman rifle", 12.75, 250, Item.Types.WEAPON));
            inventory.Add(new("Vault 13 Jumpsuit", "The standard jumpsuit for all Vault 13 residents", 5, 25, Item.Types.APPAREL));
            inventory.Add(new("10mm Ammo", "A common ammo for many weapons", 0, 1, Item.Types.AMMO));
            inventory.Add(new("Stimpack", "A healing device", 1, 30, Item.Types.AID));
            inventory.Add(new("Journal Entry", "Exposition thingy", 1, 15, Item.Types.MISC));
        }

        public void ChangeMenu(bool right)
        {
            soundEffects.SoundLocation = sounds[^3];
            soundEffects.PlaySync();
            if (right && currentPage != Pages.RADIO)
                currentPage += 1;
            if (!right && currentPage != Pages.STAT)
                currentPage -= 1;
        }

        public string ShowMenu() => currentPage switch
        {
            Pages.STAT => player.ToString(),
            Pages.INV => ShowInventory(),
            Pages.DATA => ShowData(),
            Pages.MAP => map.ToString(),
            Pages.RADIO => radio.ToString()
        };

        public string ShowInventory()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.AppendLine($"Items: {inventory.Count}");
            foreach (Item item in inventory)
            {
                stringBuilder.AppendLine($"\t{item.Name}: {item.Description}\n\t\tValue: {item.Value}\n\t\tWeight: {item.Weight}\n\t\tType: {item.Type}");
            }
            return stringBuilder.ToString();
        }

        public static string ShowData()
        {
            return DateTime.Now.ToString();
        }

        public enum Pages
        {
            STAT,
            INV,
            DATA,
            MAP,
            RADIO
        }

        public override string ToString()
        {
            return @$"
                        ___________     __________      __________      __________      ___________
                        |  STAT   |_____|   INV  |______|  DATA  |______|  MAP   |______|  RADIO  |";
        }
    }
}
