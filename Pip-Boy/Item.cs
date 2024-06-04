namespace Pip_Boy
{
    internal abstract class Item(string name, double weight, ushort value)
    {
        public readonly string Name = name;

        public readonly double Weight = weight;
        public ushort Value = value;

        public override string ToString()
        {
            string defaultHeading = $"\t{Name}:";
            defaultHeading += "\n\t\tValue: ";
            if (Value == 0)
            {
                defaultHeading += "--";
            }
            else
            {
                defaultHeading += Value;
            }

            defaultHeading += "\n\t\tWeight: ";
            if (Value == 0)
            {
                defaultHeading += "--";
            }
            else
            {
                defaultHeading += Weight;
            }
            return defaultHeading;
        }
    }
}
