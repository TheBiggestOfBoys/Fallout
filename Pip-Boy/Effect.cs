namespace Pip_Boy
{
    internal readonly struct Effect(string effector, sbyte value)
    {
        public readonly string Effector = effector;
        public readonly sbyte Value = value;
    }
}
