namespace Pathfinding;

public static class CollectLines
{
    public static List<ILine> Execute(List<ILine> lines)
    {
        var path = new List<KeyValuePair<ILine, double>>(); // line and distance to next
        var restLines = lines;                     // not visited lines
        var currentLine = restLines.First();                // next line to connect to
        restLines.Remove(currentLine);
        
        // building path by connecting all line ends
        while (restLines.Count > 0)
        {
            var foundDistance = GetDistanceToNearest(currentLine.End, restLines, out var foundLine);
            path.Add(new KeyValuePair<ILine, double>(currentLine, foundDistance));
            currentLine = foundLine;
            restLines.Remove(currentLine);
        }
        path.Add(new KeyValuePair<ILine, double>(currentLine, currentLine.End.GetDistance(path.First().Key.Start)));

        // rebuilding path after removing longest connection between lines
        var resultPath = new List<ILine>();
        var next = path.MinBy(x => x.Value);
        var nextIndex = path.IndexOf(next);
        for (int i = 0; i < path.Count; i++)
        {
            resultPath.Add(next.Key);
            nextIndex = nextIndex == path.Count-1 ? 0 : nextIndex + 1;
            next = path[nextIndex];
        }
        
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

    private static double GetDistance(this IPointD from, IPointD to)
    {
        return Math.Sqrt(Math.Pow(to.X - from.X, 2) + Math.Pow(to.Y - from.Y, 2));
    }
}