namespace Pip_Boy
{
    internal readonly struct Song(string path)
    {
        public readonly string Path = path;
        public readonly string Name = path.Split('\\')[^1].Split(".wav")[0];
    }
}
