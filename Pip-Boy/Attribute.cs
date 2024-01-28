﻿namespace Pip_Boy
{
    internal struct Attribute(string name, byte value)
    {
        public readonly string Name = name;
        public byte Value = value;

        public override readonly string ToString()
        {
            return $"{Name}:\t{Value}";
        }
    }
}