namespace FutureFinancingTriangle.Models
{
    internal class Node
    {
        internal int Level { get; set; }
        internal int Column { get; set; }
        internal int Value { get; set; }
        internal Node LeftChild { get; set; }
        internal Node RightChild { get; set; }
        internal int? GoLeftSum { get; set; }
        internal int? GoRightSum { get; set; }
    }
}
