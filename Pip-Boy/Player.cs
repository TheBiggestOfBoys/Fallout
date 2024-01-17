using System.Text;

namespace Pip_Boy
{
    internal class Player
    {
        public Dictionary<string, byte> special = new(7);

        public string[] attributeNames = ["Strength", "Perception", "Endurance", "Charisma", "Intelligence", "Agility", "Luck"];

        public readonly string name;
        public byte level = 1;
        public ushort health = 100;

        public Player(string name, byte[] attributeValues)
        {
            this.name = name;

            Dictionary<string, byte> tempSpecial = [];
            for (byte index = 0; index < 7; index++)
            {
                tempSpecial.Add(attributeNames[index], attributeValues[index]);
            };
            special = tempSpecial;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.AppendLine("Name:\t" + name);
            stringBuilder.AppendLine("Level:\t" + level);
            stringBuilder.AppendLine("Health:\t" + health);

            stringBuilder.AppendLine("S.P.E.C.I.A.L.:");
            foreach (KeyValuePair<string, byte> attribute in special)
            {
                stringBuilder.AppendLine($"\t{attribute.Key}:\t{attribute.Value}");
            }

            return stringBuilder.ToString();
        }
    }
}
