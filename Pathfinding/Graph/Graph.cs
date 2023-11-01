namespace Pathfinding;

public class Graph
{
    private const double Precision = double.Epsilon;
    private readonly List<GraphNode> _nodes;

    public Graph()
    {
        _nodes = new List<GraphNode>();
    }

    public void BuildGraph(IEnumerable<ILine> input)
    {
        foreach (var item in input)
        {
            if (!TryGetNodeForPointD(item.Start, out var startNode))
            {
                _nodes.Add(startNode);
            }

            if (!TryGetNodeForPointD(item.End, out var endNode))
            {
                _nodes.Add(endNode);
            }

            startNode.LinkNode(endNode, item.Length);
            endNode.LinkNode(startNode, item.Length);
        }
    }

    private bool TryGetNodeForPointD(IPointD point, out GraphNode outNode)
    {
        foreach (var node in _nodes)
        {
            if (Math.Abs(point.X - node.Point.X) < Precision &&
                Math.Abs(point.Y - node.Point.Y) < Precision)
            {
                outNode = node;
                return true;
            }
        }

        outNode = new GraphNode(point);
        return false;
    }
    
    public GraphNode GetNearestNode(IPointD point)
    {
        var distance = double.MaxValue;
        GraphNode foundNode = null;
        foreach (var node in _nodes)
        {
            var foundDistance = node.FindDistanceTo(point);
            if (foundDistance < distance)
            {
                distance = foundDistance;
                foundNode = node;
            }
        }

        return foundNode;
    }
    
    public void CalculateDistanceTo(GraphNode target)
    {
        foreach (var node in _nodes)
        {
            node.SetDistanceToEnd(target);
        }
    }
}