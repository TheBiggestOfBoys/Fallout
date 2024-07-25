using System;
using System.IO;

namespace Pip_Boy.Data_Types
{
    /// <summary>
    /// A way to keep <c>*.txt</c> file name and contents as an object.
    /// </summary>
    /// <param name="filePath">The path to the data entry (file).</param>
    public class Data(string filePath)
    {
        /// <summary>
        /// The file/data entry's name.
        /// </summary>
        public readonly string Title = Path.GetFileNameWithoutExtension(filePath);

        /// <summary>
        /// The file/data entry's content.
        /// </summary>
        public readonly string Text = File.ReadAllText(filePath);

        /// <summary>
        /// Shows the <see cref="Data"/> entry.
        /// </summary>
        /// <returns>The <see cref="Data"/> entry's <see cref="Title"/> and <see cref="Text"/></returns>
        public override string ToString() => $"{Title}:{Environment.NewLine}\t{Text}";
    }
}
