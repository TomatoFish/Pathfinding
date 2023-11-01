namespace Pathfinding;

public class Line : ILine
{
    public IPointD Start { get; }
    public IPointD End { get; }
    public double Length { get; }

    public Line(IPointD start, IPointD end)
    {
        Start = start;
        End = end;
        Length = Math.Sqrt( Math.Pow(end.X - start.X, 2) + Math.Pow(end.Y - start.Y, 2) );
    }
    
    public void Reverse()
    {
        
    }
}