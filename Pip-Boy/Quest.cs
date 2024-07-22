using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Pip_Boy
{
    public class Quest(string name, List<Quest.Step> steps)
    {
        public readonly string Name = name;
        public List<Step> Steps = steps;

        public struct Step(string instructions, Vector2 position, bool optional)
        {
            public readonly string Instructions = instructions;
            public bool Completed = false;
            public readonly bool Optional = optional;
            public bool Hidden = true;

            public readonly Vector2 PositionX = position;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new(Name);
            foreach (Step step in Steps)
            {
                if (!step.Hidden)
                {
                    stringBuilder.AppendLine('\t' + step.Instructions);
                }
            }
            return stringBuilder.ToString();
        }
    }
}
