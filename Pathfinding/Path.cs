namespace Pathfinding;

public class PathTest
{
    public PathTest()
    {
        var path1 = new List<ILine>()
        {
            new Line(new PointD(0, 0), new PointD(1, 1)),
            new Line(new PointD(1, 1), new PointD(2, 3)),
            new Line(new PointD(2, 3), new PointD(0, 8)),
            new Line(new PointD(0, 8), new PointD(1, 5))
        };
        
        var path2 = new List<ILine>()
        {
            new Line(new PointD(0, 0), new PointD(1, 1)),
            new Line(new PointD(1, 1), new PointD(4, 3)),
            new Line(new PointD(4, 3), new PointD(2, 5)),
            new Line(new PointD(2, 5), new PointD(1, 4)),
            new Line(new PointD(1, 4), new PointD(4, 3)),
            new Line(new PointD(4, 3), new PointD(5, 1))
        };
        
        var path3 = new List<ILine>()
        {
            new Line(new PointD(0, 0), new PointD(1, 1)),
            new Line(new PointD(1, 1), new PointD(4, 3)),
            new Line(new PointD(4, 3), new PointD(1, 3)),
            new Line(new PointD(1, 3), new PointD(4, 1)),
            new Line(new PointD(4, 1), new PointD(5, 0))
        };
        
        var path4 = new List<ILine>()
        {
            new Line(new PointD(1, 1), new PointD(2, 1)),
            new Line(new PointD(2, 1), new PointD(4, 2)),
            new Line(new PointD(4, 2), new PointD(2, 3)),
            new Line(new PointD(2, 3), new PointD(2, 1)),
            new Line(new PointD(2, 1), new PointD(5, 1)),
            new Line(new PointD(5, 1), new PointD(4, 2)),
            new Line(new PointD(4, 2), new PointD(4, 3))
        };

        var result1 = Calculate(path1);
        var result2 = Calculate(path2);
        var result3 = Calculate(path3);
        var result4 = Calculate(path4);
        
        Print(result1);
        Print(result2);
        Print(result3);
        Print(result4);
    }

    private IEnumerable<ILine> Calculate(IEnumerable<ILine> input)
    {
        var graph = new Graph();
        graph.BuildGraph(input);

        var start = input.First().Start;
        var end = input.Last().End;
        var path = AStar.GetPath(graph, start, end);
        
        return path;
    }

    private void Print(IEnumerable<ILine> input)
    {
        foreach (var item in input)
        {
            Console.WriteLine($"[{item.Start.X},{item.Start.Y}] -> [{item.End.X},{item.End.Y}]");
        }
        Console.WriteLine("----------");
    }
}