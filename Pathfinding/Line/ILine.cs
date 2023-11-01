namespace Pathfinding;

public interface ILine
{
    IPointD Start { get; }
    IPointD End { get; }
    double Length { get; }
    void Reverse();
}