namespace Cint.RobotCleaner.Models;

// Different classes could go into different class files. Keeping them together for simplicity.
public class MovementInstruction
{
    public Direction Direction { get; init; }
    public int Steps { get; init; }
}

public class StartLocationInstruction
{
    public int X { get; set; }
    public int Y { get; set; }
}

public enum Direction
{
    E,
    W,
    S,
    N
}