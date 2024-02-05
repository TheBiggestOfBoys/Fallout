namespace Pip_Boy
{
    internal struct Faction(string name, string description)
    {
        readonly string Name = name;
        readonly public string Description = description;
        public sbyte ReputationValue = 0;
        public Reputation reputation = Reputation.Neutral;

        public enum Reputation
        {
            Villified,
            MercifulThug,
            Mixed,
            Neutral,
            Accepted,
            Liked,
            Idolized
        }

        public override readonly string ToString()
        {
            return $"{Name}:\t{reputation}";
        }
    }
}
