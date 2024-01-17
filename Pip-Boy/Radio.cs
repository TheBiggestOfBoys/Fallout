using System.Media;
using System.Text;

namespace Pip_Boy
{
    internal class Radio(string folderPath)
    {
        public static readonly SoundPlayer soundPlayer = new();
        public List<string> songs = [.. Directory.GetFiles(folderPath, "*.wav")];

        /// <summary>
        /// Parses the file path to get jus the song's name
        /// </summary>
        /// <param name="filePath">The path of the audio file</param>
        /// <returns></returns>
        public static string GetSongName(string filePath)
        {
            return filePath.Split('\\')[^1].Split(".wav")[0];
        }

        public void Play(int songIndex)
        {
            soundPlayer.SoundLocation = songs[songIndex];
            soundPlayer.Load();
            soundPlayer.Play();
        }

        public void Play()
        {
            soundPlayer.SoundLocation = songs[0];
            soundPlayer.Load();
            soundPlayer.Play();
        }

        public void ShufflePlay()
        {
            foreach (string song in songs)
            {
                soundPlayer.SoundLocation = song;
                soundPlayer.Load();
                soundPlayer.Play();
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.AppendLine("Songs:\t" + songs.Count);
            foreach (string song in songs)
            {
                stringBuilder.AppendLine('\t' + GetSongName(song));
            }
            return stringBuilder.ToString();
        }
    }
}
