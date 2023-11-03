namespace Pathfinding;

public class PathTestVariant
{
    public PathTestVariant()
    {
        var r = new Random();
        var path = new List<ILine>();
        for (int i = 0; i < 5; i++)
        {
            path.Add(new Line(new PointD(r.Next(), r.Next()), new PointD(r.Next(), r.Next())));
        }
        
        var resultTake1 = CollectLines.ExecuteTake1(path);
        Helpers.Print(resultTake1);
        
        Console.WriteLine("------------------------------------------");
        
        var resultTake2 = CollectLines.ExecuteTake2(path);
        Helpers.Print(resultTake2);
    }
}