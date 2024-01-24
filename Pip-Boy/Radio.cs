using System.Media;
using System.Text;

namespace Pip_Boy
{
    internal class Radio
    {
        public static readonly Random random = new();
        public static readonly SoundPlayer soundPlayer = new();
        public List<Song> songs = [];

        public Radio(string folderPath)
        {
            foreach (string path in Directory.GetFiles(folderPath, "*.wav"))
            {
                songs.Add(new(path));
            }
        }

        public void Play()
        {
            soundPlayer.SoundLocation = songs[random.Next(songs.Count)].Path;
            soundPlayer.PlaySync();
        }

        public void AddSong(string path)
        {
            if (path.Split('\\')[^1].Split('.')[^1] == "wav")
                songs.Add(new(path));
            else
                Console.Error.Write("Please give a '.wav' file!");
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.AppendLine("Songs:\t" + songs.Count);
            foreach (Song song in songs)
                stringBuilder.AppendLine('\t' + song.Name);

            return stringBuilder.ToString();
        }
    }
}
