using System.Text;

namespace Pip_Boy
{
    internal abstract class Item(string name, double weight, ushort value)
    {
        public readonly string Name = name;
        public readonly double Weight = weight;
        public ushort Value = value;

        public override string ToString()
        {
            StringBuilder defaultHeading = new($"\t{Name}:\n\t\tValue: ");
            if (Value == 0)
            {
                defaultHeading.Append("--");
            }
            else
            {
                defaultHeading.Append(Value);
            }

            defaultHeading.Append("\n\t\tWeight: ");
            if (Weight == 0)
            {
                defaultHeading.Append("--");
            }
            else
            {
                defaultHeading.Append(Weight);
            }
            return defaultHeading.ToString();
        }
    }
}
