namespace Pathfinding;

public static class AStar
{
    public static IEnumerable<ILine> GetPath(Graph graph, IPointD start, IPointD end)
    {
        var firstNode = graph.GetNearestNode(start);
        var lastNode = graph.GetNearestNode(end);
        graph.CalculateDistanceTo(lastNode);
        Search(firstNode, lastNode);

        var path = BuildPath(lastNode);
        
        return path;
    }
    
    private static IEnumerable<ILine> BuildPath(GraphNode lastNode)
    {
        var path = new List<GraphNode>();
        path.Add(lastNode);
        for (var node = lastNode; node.Parent != null; node = node.Parent)
        {
            path.Add(node);
        }

        var lines = new List<ILine>();
        for (int i = 0; i < path.Count - 1; i++)
        {
            lines.Add(new Line(path[i].Point, path[i+1].Point));
        }
            
        return lines;
    }
    
    private static void Search(GraphNode first, GraphNode last)
    {
        first.MinCostToStart = 0;
        var nodeQueue = new List<GraphNode>();
        nodeQueue.Add(first);
        do
        {
            nodeQueue = nodeQueue.OrderBy(x => x.MinCostToStart + x.DistanceToEnd).ToList();
            var node = nodeQueue.First();
            nodeQueue.Remove(node);
            foreach (var cnn in node.LinkedNodes)
            {
                var childNode = cnn.Key;
                if (childNode.Visited)
                    continue;
                if (childNode.MinCostToStart == null || node.MinCostToStart.Value + cnn.Value < childNode.MinCostToStart)
                {
                    childNode.MinCostToStart = node.MinCostToStart + cnn.Value;
                    childNode.Parent = node;
                    if (!nodeQueue.Contains(childNode))
                        nodeQueue.Add(childNode);
                }
            }

            node.Visited = true;
            if (node == last)
                return;
        } while (nodeQueue.Any());
    }
}