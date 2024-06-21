using System;
using System.IO;

namespace Terminal_Minigame
{
    readonly struct Data(string filePath)
    {
        readonly string Title = Path.GetFileNameWithoutExtension(filePath);
        readonly string Text = File.ReadAllText(filePath);
        readonly DateTime DateTime = File.GetLastWriteTime(filePath);

        public override string ToString() => Title + '\t' + DateTime.ToString() + '\n' + new string('-', (Title.Length + '\t' + DateTime.ToString().Length) - 2) + '\n' + Text;
    }
}
