using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Pip_Boy.Data_Types
{
    /// <summary>
    /// A journey that needs to be completed
    /// </summary>
    /// <param name="name"></param>
    /// <param name="steps"></param>
    public class Quest(string name, List<Quest.Step> steps)
    {
        /// <summary>
        /// The Name of the <see cref="Quest"/>
        /// </summary>
        public readonly string Name = name;

        /// <summary>
        /// List of all the <see cref="Step"/>s, which must be completed
        /// </summary>
        public List<Step> Steps = steps;

        /// <summary>
        /// Parts of a <see cref="Quest"/> that need to be completed
        /// </summary>
        /// <param name="instructions"></param>
        /// <param name="position"></param>
        /// <param name="optional"></param>
        public struct Step(string instructions, Vector2 position, bool optional)
        {
            /// <summary>
            /// What must be done for the <see cref="Step"/> to be completed
            /// </summary>
            public readonly string Instructions = instructions;

            /// <summary>
            /// If the step is completed or not
            /// </summary>
            public bool Completed = false;

            /// <summary>
            /// If the step is optional, this can't be changed after construction
            /// </summary>
            public readonly bool Optional = optional;

            /// <summary>
            /// If the step is hidden, when the previous step is completed, <see cref="Completed"/> will be <c>true</c> and <see cref="Hidden"/> will be <c>false</c>.
            /// </summary>
            public bool Hidden = true;

            /// <summary>
            /// The location of the <see cref="Quest"/>'s <see cref="Step"/> on the <see cref="Objects.Map"/>
            /// </summary>
            public readonly Vector2 PositionX = position;
        }

        /// <returns>The <see cref="Quest"/> and all of the <see cref="Steps"/></returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new(Name);
            stringBuilder.AppendLine();
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
