namespace Pathfinding;

public static class CollectLines
{
    #region take1

    private static Leaf BestLeaf;
    
    // too greedy
    public static IEnumerable<ILine> ExecuteTake2(List<ILine> lines)
    {
        BestLeaf = new Leaf(null, double.MaxValue, null, false);
        var leaves = new List<Leaf>(lines.Select(l => new Leaf(l, l.Length, null, false)));
        leaves.AddRange(lines.Select(l => new Leaf(l, l.Length, null, true)));

        Parallel.ForEach(leaves, leaf =>
        {
            CalculateLeaf(leaf, lines);
        });

        var path = new List<ILine>();
        for (var curLeaf = BestLeaf; curLeaf != null; curLeaf = curLeaf.Parent)
        {
            if (curLeaf.IsReversed)
                curLeaf.Line.Reverse();
            path.Add(curLeaf.Line);
        }
        path.Reverse();
        return path;
    }
    
    private static void CalculateLeaf(Leaf leaf, IEnumerable<ILine> lines)
    {
        var restLines = lines.Where(l => l != leaf.Line);
        if (!restLines.Any())
        {
            if (leaf.Cost < BestLeaf.Cost)
                BestLeaf = leaf;
            return;
        }
        foreach (var l in restLines)
        {
            var pointToConnect = !leaf.IsReversed ? leaf.Line.End : leaf.Line.Start;
            
            var dist1 = leaf.Cost + l.Length + pointToConnect.GetDistance(l.Start);
            var newLeaf1 = new Leaf(l, dist1, leaf, false);
            CalculateLeaf(newLeaf1, restLines);

            var dist2 = leaf.Cost + l.Length + pointToConnect.GetDistance(l.End);
            var newLeaf2 = new Leaf(l, dist2, leaf, true);
            CalculateLeaf(newLeaf2, restLines);
        }
    }
    
    public class Leaf
    {
        public readonly ILine Line;
        public readonly double Cost;
        public readonly Leaf Parent;
        public readonly bool IsReversed;

        public Leaf(ILine l, double c, Leaf p, bool isR)
        {
            Line = l;
            Cost = c;
            Parent = p;
            IsReversed = isR;
        }
    }
    
    #endregion

    #region take2

    public static IEnumerable<ILine> ExecuteTake1(List<ILine> lines)
    {
        var path = new KeyValuePair<ILine, double>[lines.Count]; // line and distance to next
        var restLines = new List<ILine>(lines);                     // not visited lines
        var currentLine = restLines.First();                // next line to connect to
        restLines.Remove(currentLine);
        
        // building path by connecting all line ends
        for (int i = 0, restLinesCount = restLines.Count; i < restLinesCount; i++)
        {
            var foundDistance = GetDistanceToNearest(currentLine.End, restLines, out var foundLine);
            path[i] = new KeyValuePair<ILine, double>(currentLine, foundDistance);
            currentLine = foundLine;
            restLines.Remove(currentLine);
        }
        path[^1] = new KeyValuePair<ILine, double>(currentLine, currentLine.End.GetDistance(path.First().Key.Start));

        // rebuilding path after removing longest connection between lines
        var resultPath = new List<ILine>();
        var next = path.MinBy(x => x.Value);
        var nextIndex = Array.IndexOf(path, next);
        resultPath.AddRange(path[nextIndex..].Select(p => p.Key));
        resultPath.AddRange(path[..^(resultPath.Count)].Select(p => p.Key));
        
        return resultPath;
    }

    private static double GetDistanceToNearest(IPointD point, List<ILine> lines, out ILine foundLine)
    {
        var minDistance = double.MaxValue;
        foundLine = null;
        foreach (var line in lines)
        {
            var distanceToStart = point.GetDistance(line.Start);
            var distanceToEnd = point.GetDistance(line.End);
            var distanceToLine = Math.Min(distanceToStart, distanceToEnd);
            if (distanceToLine < minDistance)
            {
                minDistance = distanceToLine;
                if (distanceToStart > distanceToEnd)
                    line.Reverse();
                foundLine = line;
            }
        }

        return minDistance;
    }
    
    #endregion

    private static double GetDistance(this IPointD from, IPointD to)
    {
        return Math.Sqrt(Math.Pow(to.X - from.X, 2) + Math.Pow(to.Y - from.Y, 2));
    }
}