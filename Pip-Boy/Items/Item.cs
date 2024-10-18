using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Pip_Boy.Items
{
    /// <summary>
    /// Generic super-class for all other <see cref="Objects.Inventory"/> items.
    /// </summary>
    [Serializable]
    public abstract class Item
    {
        /// <summary>
        /// What the <see cref="Item"/> is called.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// How much the <see cref="Item"/> weighs.
        /// </summary>
        public readonly float Weight;

        /// <summary>
        /// How much the <see cref="Item"/> costs.
        /// </summary>
        public ushort Value;

        /// <summary>
        /// An emoji representation of the <see cref="Item"/>.
        /// </summary>
        public string Icon;

        #region Constructors
        public Item(string name, float weight, ushort value)
        {
            Name = name;
            Weight = weight;
            Value = value;
        }

        /// <summary>
        /// Empty constructor for serialization.
        /// </summary>
        public Item()
        {
            Name = string.Empty;
            Weight = 0;
            Value = 0;
        }
        #endregion

        #region File Stuff
        /// <summary>
        /// Serializes the <see cref="Item"/> to an <c>*.xml</c> file.
        /// </summary>
        /// <param name="folderPath">The folder to write the <c>*.xml</c> file to.</param>
        public string ToFile(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                string filePath = folderPath + Name + ".xml";
                XmlSerializer x = new(GetType());
                StreamWriter writer = new(filePath);
                //writer.WriteProcessingInstruction("xml-stylesheet", "type=\"text/css\" href=\"../Inventory Styling.css\"");
                x.Serialize(writer, this);
                writer.Close();
                return filePath;
            }
            else
            {
                throw new DirectoryNotFoundException();
            }
        }

        /// <summary>
        /// Reads the root tag of an <c>*.xml</c> file.
        /// </summary>
        /// <param name="filePath">The path to the file</param>
        /// <returns>The <see cref="Type"/> from the tag name.</returns>
        /// <exception cref="NullReferenceException">If no head object tag is found.</exception>
        /// <exception cref="FormatException">IF the file is no <c>*.xml</c>.</exception>
        public static Type GetTypeFromXML(string filePath)
        {
            if (Path.GetExtension(filePath) == ".xml")
            {
                XmlDocument doc = new();
                doc.Load(filePath);
                string typeName = doc.DocumentElement?.LocalName ?? throw new NullReferenceException("No head object tag found!");
                Type type = typeName switch
                {
                    "Weapon" => typeof(Weapon),
                    "HeadPiece" => typeof(HeadPiece),
                    "TorsoPiece" => typeof(TorsoPiece),
                    "Aid" => typeof(Aid),
                    "Misc" => typeof(Misc),
                    "Ammo" => typeof(Ammo),
                    _ => throw new NullReferenceException("The type is null!"),
                };
                return type;
            }
            else
            {
                throw new FormatException("File is not '*.xml'!");
            }
        }
        #endregion

        /// <returns>The <see cref="Item"/>'s <see cref="Name"/>, <see cref="Icon"/>, <see cref="Value"/> and <see cref="Weight"/>, with logic handling if <see cref="Value"/> or <see cref="Weight"/> are 0</returns>
        public override string ToString()
        {
            StringBuilder defaultHeading = new('\t' + Name + ':' + Icon);
            defaultHeading.AppendLine();
            defaultHeading.Append("\t\tValue: ");
            defaultHeading.Append(Value == 0 ? "--" : Value.ToString());
            defaultHeading.AppendLine();
            defaultHeading.Append("\t\tWeight: ");
            defaultHeading.Append(Weight == 0 ? "--" : Weight.ToString());
            return defaultHeading.ToString();
        }
    }
}
