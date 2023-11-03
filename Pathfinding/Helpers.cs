namespace Pathfinding;

public static class Helpers
{
    public static void Print(IEnumerable<ILine> input)
    {
        foreach (var item in input)
        {
            Console.WriteLine($"[{item.Start.X},{item.Start.Y}] -> [{item.End.X},{item.End.Y}]");
        }
        Console.WriteLine("----------");
    }
}