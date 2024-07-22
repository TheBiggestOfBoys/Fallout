namespace Pip_Boy
{
    public readonly struct Data(string title, string text)
    {
        public readonly string Title = title;
        public readonly string Text = text;

        public override string ToString() => $"{Title}:\n\t{Text}";
    }
}
