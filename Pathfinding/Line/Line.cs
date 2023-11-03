namespace Pathfinding;

public class Line : ILine
{
    public IPointD Start { get; private set; }
    public IPointD End { get; private set; }
    public double Length { get; }

    public Line(IPointD start, IPointD end)
    {
        Start = start;
        End = end;
        Length = Math.Sqrt( Math.Pow(end.X - start.X, 2) + Math.Pow(end.Y - start.Y, 2) );
    }
    
    public void Reverse()
    {
        (Start, End) = (End, Start);
    }
}