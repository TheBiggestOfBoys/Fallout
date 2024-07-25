namespace Pip_Boy.Data_Types
{
    /// <summary>
    /// Holds the <see cref="Name"/> and <see cref="Path"/> of a given <c>*.wav</c> file representing a song.
    /// </summary>
    /// <param name="path">The <c>*.wav</c> file's path.</param>
    public readonly struct Song(string path)
    {
        /// <summary>
        /// The Path of the <c>*.wav</c> file.
        /// </summary>
        public readonly string Path = path;

        /// <summary>
        /// The Name of the <see cref="Song"/> (file without extension).
        /// </summary>
        public readonly string Name = path.Split('\\')[^1].Split(".wav")[0];
    }
}
