using System.IO;

namespace Terminal_Minigame
{
    class Data(string filePath)
    {
        readonly string Title = Path.GetFileNameWithoutExtension(filePath);
        readonly string Text = File.ReadAllText(filePath);

        public override string ToString() => Title + '\n' + new string('-', Title.Length - 2) + '\n' + Text;
    }
}
