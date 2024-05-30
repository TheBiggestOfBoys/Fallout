using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Pip_Boy
{
    internal struct Quest(string name, List<Quest.Step> steps)
    {
        public readonly string Name = name;
        public List<Step> Steps = steps;

        internal struct Step(string instructions, Vector2 position)
        {
            public readonly string Instructions = instructions;
            public bool Completed = false;
            public readonly bool Optional = false;
            public bool Hidden = true;

            public readonly Vector2 PositionX = position;
        }

        public override readonly string ToString()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.AppendLine(Name);

            foreach (Step step in Steps)
                if (!step.Hidden)
                    stringBuilder.AppendLine('\t' + step.Instructions);

            return stringBuilder.ToString();
        }
    }
}
