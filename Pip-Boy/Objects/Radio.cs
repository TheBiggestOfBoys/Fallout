using Pip_Boy.Data_Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Text;

namespace Pip_Boy.Objects
{
    /// <summary>
    /// Contains all songs and logic for playing them.
    /// </summary>
    public class Radio
    {
        /// <summary>
        /// What the songs will be played from.
        /// </summary>
        public SoundPlayer soundPlayer = new();

        /// <summary>
        /// The list of all <see cref="Song"/>s.
        /// </summary>
        public List<Song> songs = [];

        /// <summary>
        /// Selected position in the <c>songs</c> list.
        /// </summary>
        public byte songIndex = 0;

        /// <summary>
        /// Initialize a radio object with '.wav' files from a folder
        /// </summary>
        /// <param name="folderPath">The path to the folder containing the songs</param>
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

        #region Song Management
        /// <summary>
        /// Add a new song to the list using an inputted path
        /// </summary>
        /// <param name="pipBoy">Used for error handling</param>
        public void AddSong(PipBoy pipBoy)
        {
            Console.Write("Enter path to '.wav' file: ");
            string? path = Console.ReadLine();
            if (path != null && path.EndsWith(".wav"))
            {
                songs.Add(new(path));
            }
            else
            {
                pipBoy.Error("Please give a '.wav' file!");
            }
        }

        /// <summary>
        /// Changes the selected song, and handles array bounds.
        /// </summary>
        /// <param name="up">Whether the move the index up/down</param>
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
        #endregion

        /// <returns>A table with all the song names</returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new("Songs:\t" + songs.Count);
            stringBuilder.AppendLine();
            foreach (Song song in songs)
            {
                stringBuilder.AppendLine('\t' + song.Name);
            }
            return stringBuilder.ToString();
        }
    }
}
