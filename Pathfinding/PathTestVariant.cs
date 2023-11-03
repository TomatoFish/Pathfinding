namespace Pathfinding;

public class PathTestVariant
{
    public PathTestVariant()
    {
        var path1 = new List<ILine>()
        {
            new Line(new PointD(0, 0), new PointD(1, 1)),
            new Line(new PointD(5, 8), new PointD(6, 3)),
            new Line(new PointD(2, 4), new PointD(10, 7)),
            new Line(new PointD(1, 8), new PointD(8, 1))
        };
        
        var result1 = CollectLines.Execute(path1);
        
        Helpers.Print(result1);
    }
}