namespace Pathfinding;

public class PointD : IPointD
{
    public double X { get; }
    public double Y { get; }

    public PointD(double x, double y)
    {
        X = x;
        Y = y;
    }
}