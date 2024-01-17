using System.Media;

namespace Pip_Boy
{
    internal class Radio
    {
        public static readonly SoundPlayer soundPlayer = new();
        public List<string> songs = [];

        public Radio(string folderPath)
        {
            songs = [.. Directory.GetFiles(folderPath, "*.wav")];
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
    }
}
