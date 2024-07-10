namespace Pip_Boy
{
    public readonly struct Song(string path)
    {
        public readonly string Path = path;
        public readonly string Name = path.Split('\\')[^1].Split(".wav")[0];
    }
}
