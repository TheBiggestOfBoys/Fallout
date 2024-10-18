using Pip_Boy.Data_Types;
using System;
using System.IO;
using System.Xml.Serialization;

namespace Pip_Boy.Items
{
    /// <summary>
    /// A Head armor
    /// </summary>
    public class HeadPiece : Apparel
    {
        public PieceType pieceType ;

        private static PieceType GetPieceType(float weight) => weight switch
        {
            <= (byte)PieceType.Glasses => PieceType.Glasses,
            <= (byte)PieceType.Hat => PieceType.Hat,
            <= (byte)PieceType.Helmet => PieceType.Helmet,
            _ => PieceType.Other,
        };

        #region Constructors
        public HeadPiece(string name, float weight, ushort value, Effect[] effects, byte DT, bool powerArmor) : base(name, weight, value, effects, DT, powerArmor)
        {
            Icon = IconDeterminer.Determine(pieceType);
            pieceType = GetPieceType(Weight);
        }

        /// <inheritdoc/>
        public HeadPiece() : base() { }
        #endregion

        /// <summary>
        /// Deserializes the <see cref="HeadPiece"/> object from an <c>*.xml</c> file.
        /// </summary>
        /// <typeparam name="T">The <see cref="HeadPiece"/> sub-class type to serialize to</typeparam>
        /// <param name="filePath">The path to the <c>*.xml</c> file.</param>
        /// <returns>The deserialized <see cref="HeadPiece"/> object.</returns>
        /// <exception cref="NullReferenceException">If the <c>*.xml</c> file returns a null object.</exception>
        public static HeadPiece FromFile(string filePath)
        {
            if (Path.GetExtension(filePath) == ".xml")
            {
                XmlSerializer x = new(typeof(HeadPiece));
                StringReader reader = new(filePath);
                HeadPiece? tempItem = (HeadPiece?)x.Deserialize(reader) ?? throw new NullReferenceException("XMl file object is null!");
                reader.Close();
                return tempItem;
            }
            else
            {
                throw new FormatException("File is not '*.xml'!");
            }
        }

        public enum PieceType : byte
        {
            Glasses = 1,
            Hat = 2,
            Helmet = 7,
            Other
        }
    }
}
