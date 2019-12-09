using System;
using System.Collections.Generic;

namespace FutureFinancingTriangle.Models
{
    internal class ResultPath
    {
        internal IList<Node> Nodes { get; set; }
        internal int? PathSum { get; set; }
        internal TimeSpan SolvingTime { get; set; }
    }
}
