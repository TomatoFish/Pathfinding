namespace Pathfinding;

public class GraphNode
{
    public IPointD Point { get; }
    public double DistanceToEnd { get; private set; }
    public double? MinCostToStart { get; set; }
    public bool Visited { get; set; }
    public GraphNode Parent { get; set; }

    private Dictionary<GraphNode, double> _linkedNodes;

    public IOrderedEnumerable<KeyValuePair<GraphNode, double>> LinkedNodes =>
        _linkedNodes.ToList().OrderBy(x => x.Value);

    public GraphNode(IPointD point)
    {
        Point = point;
        _linkedNodes = new Dictionary<GraphNode, double>();
    }

    public void LinkNode(GraphNode node, double length)
    {
        _linkedNodes.TryAdd(node, length);
    }

    public double FindDistanceTo(IPointD target)
    {
        return Math.Sqrt(Math.Pow(Point.X - target.X, 2) + Math.Pow(Point.Y - target.Y, 2));
    }
    
    public void SetDistanceToEnd(GraphNode target)
    {
        DistanceToEnd = FindDistanceTo(target.Point);
    }
}