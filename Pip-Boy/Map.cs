using System.Text;

namespace Pip_Boy
{
    internal class Map(int height, int width, int density)
    {
        private static readonly Random random = new();
        public char[][] Grid { get; } = GenerateMap(height, width, density);

        /// <summary>
        /// Locations of interest on the map
        /// </summary>
        private static readonly char[] markers = [
            '!', // Quest
            '?', // Undiscovered
            '#', // Settlement
            '@', // Base
            '+'  // Doctor
        ];

        public static char[][] GenerateMap(int height, int width, int density)
        {
            char[][] tempMap = new char[height][];
            // Size the map's height..
            for (int i = 0; i < height; i++)
            {
                // and width
                tempMap[i] = new char[width];
                for (int j = 0; j < width; j++)
                    tempMap[i][j] = ' ';
            }

            // Now randomly assign markers in the array
            for (int i = 0; i < density; i++)
                tempMap[random.Next(height)][random.Next(width)] = markers[random.Next(markers.Length)];

            return tempMap;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new();
            foreach (char[] row in Grid)
            {
                stringBuilder.Append(row);
                stringBuilder.AppendLine();
            }
            return stringBuilder.ToString();
        }
    }
}
