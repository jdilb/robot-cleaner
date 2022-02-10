using Cint.RobotCleaner.Models;

namespace Cint.RobotCleaner;

public class Robot
{
    private readonly ILocationTracker _locationTracker;

    public Robot(ILocationTracker locationTracker)
    {
        _locationTracker = locationTracker;
    }

    public void ProcessInstruction(MovementInstruction movementInstruction)
    {
        Action move = movementInstruction.Direction switch
        {
            Direction.E => () => _locationTracker.ShiftEast(),
            Direction.W => () => _locationTracker.ShiftWest(),
            Direction.S => () => _locationTracker.ShiftSouth(),
            Direction.N => () => _locationTracker.ShiftNorth(),
            _ => () => throw new IndexOutOfRangeException()
        };

        for (var i = 0; i < movementInstruction.Steps; i++)
            move.Invoke(); // ... and do some cleaning.
    }

    public int PlacesCleaned()
    {
        return _locationTracker.UniqueLocationsVisited();
    }
}