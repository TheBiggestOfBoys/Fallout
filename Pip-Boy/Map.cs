using System;
using System.Numerics;
using System.Text;

namespace Pip_Boy
{
    internal struct Map(byte height, byte width, byte density)
    {
        private static readonly Random random = new();
        public readonly char[][] Grid = GenerateMap(width, height, density);

        Vector2 playerPosition = Vector2.Zero;

        /// <summary>
        /// Locations of interest on the map
        /// </summary>
        private static readonly char[] markers = ['!', '?', '#', '@', '+'];

        /// <summary>
        /// The key telling what the markers represent
        /// </summary>
        public static readonly string[] keys = ["Quest", "Undiscovered", "Settlement", "Base", "Doctor"];

        /// <summary>
        /// Generates the Legend with the corresponding marker/key pairs
        /// </summary>
        /// <returns>The Map's Legend</returns>
        public static string GenerateLegend()
        {
            StringBuilder stringBuilder = new();
            if (markers.Length == keys.Length)
                for (byte x = 0; x < markers.Length; x++)
                    stringBuilder.Append($"{markers[x]} = {keys[x]}, ");

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Create a map with the given parameters
        /// </summary>
        /// <param name="height">The height of the map</param>
        /// <param name="width">The width of the map</param>
        /// <param name="density">How many points of interest there should be</param>
        /// <returns></returns>
        public static char[][] GenerateMap(byte height, byte width, byte density)
        {
            char[][] tempMap = new char[width][];
            // Size the map's width.
            for (byte i = 0; i < width; i++)
            {
                // and height
                tempMap[i] = new char[height];
                // set every cell to an empty space
                for (byte j = 0; j < height; j++)
                    tempMap[i][j] = ' ';
            }

            // Now randomly assign markers in the array
            for (int i = 0; i < density; i++)
                tempMap[random.Next(width)][random.Next(height)] = markers[random.Next(markers.Length)];

            return tempMap;
        }

        /// <summary>
        /// Moves the player icon on the map to the current X and Y coordinates
        /// </summary>
        /// <param name="up">If the player moves up/down</param>
        /// <param name="right">If the player moves right/left</param>
        public void MovePlayer(bool? up, bool? right)
        {
            Grid[(int)playerPosition.Y][(int)playerPosition.X] = ' ';

            if (up == true && playerPosition.Y > 0)
            {
                playerPosition.Y--;
            }
            else if (up == false && playerPosition.Y < Grid.Length)
            {
                playerPosition.Y++;
            }

            if (right == true && playerPosition.X < Grid[0].Length)
            {
                playerPosition.X++;
            }
            else if (right == false && playerPosition.X > 0)
            {
                playerPosition.X--;
            }

            // Player will be represented by '>' on the map
            // X & Y are intentionally flipped
            Grid[(int)playerPosition.Y][(int)playerPosition.X] = '>';
        }

        public readonly char[] this[int row] => Grid[row];

        /// <summary>
        /// Shows the map
        /// </summary>
        /// <returns>The 2D array as a string</returns>
        public override readonly string ToString()
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
