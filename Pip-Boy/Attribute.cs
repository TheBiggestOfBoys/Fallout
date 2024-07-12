namespace Pip_Boy
{
    public struct Attribute(string name, byte value)
    {
        public readonly string Name = name;
        public byte Value = value;

        public override readonly string ToString()
        {
            return $"{Name}:\t{Value}";
        }
    }
}
