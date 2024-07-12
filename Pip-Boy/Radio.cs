using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Text;

namespace Pip_Boy
{
    public class Radio
    {
        public static readonly SoundPlayer soundPlayer = new();
        public List<Song> songs = [];
        public byte songIndex = 0;

        /// <summary>
        /// Initialize a radio object with '.wav' files from a folder
        /// </summary>
        /// <param name="folderPath">The path to the folder contaaing the songs</param>
        public Radio(string folderPath)
        {
            foreach (string path in Directory.GetFiles(folderPath, "*.wav"))
            {
                songs.Add(new(path));
            }
        }

        /// <summary>
        /// Plays a random song from the list
        /// </summary>
        public void Play()
        {
            soundPlayer.SoundLocation = songs[songIndex].Path;
            soundPlayer.Load();
            soundPlayer.Play();
        }

        /// <summary>
        /// Add a new song to the list using an inputted path
        /// </summary>
        /// <param name="pipBoy">Used for error handling</param>
        public void AddSong(PipBoy pipBoy)
        {
            Console.Write("Enter path to '.wav' file: ");
            string path = Console.ReadLine();
            if (path.EndsWith(".wav"))
            {
                songs.Add(new(path));
            }
            else
            {
                pipBoy.Error("Please give a '.wav' file!");
            }
        }

        public void ChangeSong(bool up)
        {
            if (up)
            {
                if (songIndex < songs.Count)
                {
                    songIndex++;
                }
                else
                {
                    songIndex = 0;
                }
            }
            else
            {
                if (songIndex > 0)
                {
                    songIndex--;
                }
                else
                {
                    songIndex = (byte)(songs.Count - 1);
                }
            }
        }

        /// <summary>
        /// Shows all songs
        /// </summary>
        /// <returns>A table with all the song names</returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.AppendLine("Songs:\t" + songs.Count);
            foreach (Song song in songs)
            {
                stringBuilder.AppendLine('\t' + song.Name);
            }

            return stringBuilder.ToString();
        }
    }
}
