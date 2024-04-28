namespace Pip_Boy
{
    internal struct Faction(string name, string description)
    {
        public readonly string Name = name;
        public readonly string Description = description;
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
