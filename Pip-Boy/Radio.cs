using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace Pip_Boy
{
    internal class Radio
    {
        public static readonly SoundPlayer soundPlayer = new();
        public List<string> songs;

        public void Play()
        {
            foreach (string song in songs)
            {

            }
        }
    }
}
