﻿using System;
using System.IO;
using System.Xml.Serialization;

namespace Pip_Boy.Items
{
    public class Aid : Equippable
    {
        #region Constructors
        public Aid(string name, double weight, ushort value, Effect[] effects) : base(name, weight, value, effects) { }

        public Aid() : base() { }
        #endregion

        public static Aid FromFile(string filePath)
        {
            XmlSerializer x = new(typeof(Aid));
            TextReader reader = new StreamReader(filePath);
            Aid? tempItem = (Aid?)x.Deserialize(reader) ?? throw new NullReferenceException("XMl file object is null!");
            reader.Close();
            return tempItem;
        }
    }
}