using System;

namespace Pip_Boy
{
    public class Data(string title, string text)
    {
        public readonly string Title = title;
        public readonly string Text = text;

        public override string ToString() => $"{Title}:{Environment.NewLine}\t{Text}";
    }
}
