namespace Pip_Boy
{
    public class Faction(string name, string description)
    {
        public readonly string Name = name;
        public readonly string Description = description;
        public sbyte ReputationValue = 0;
        public Reputation reputation = Reputation.Neutral;

        public enum Reputation : sbyte
        {
            Villified = -128,
            MercifulThug = -84,
            Mixed = -42,
            Neutral = 0,
            Accepted = 41,
            Liked = 80,
            Idolized = 127
        }

        public override string ToString() => $"{Name}:\t{reputation}";
    }
}
