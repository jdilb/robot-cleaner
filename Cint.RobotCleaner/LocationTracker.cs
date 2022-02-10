namespace Cint.RobotCleaner;

public interface ILocationTracker
{
    void ShiftEast();
    void ShiftWest();
    void ShiftNorth();
    void ShiftSouth();
    int UniqueLocationsVisited();
    (int, int) GetCurrentLocation();
}

public class LocationTracker : ILocationTracker
{
    private readonly HashSet<(int, int)> _uniqueCoordinatesVisited = new();
    private int _x;
    private int _y;

    public LocationTracker(int x, int y)
    {
        _x = x;
        _y = y;
        RecordLocation();
    }
    
    public void ShiftEast()
    {
        ++_x;
        RecordLocation(); // RecordLocation() could be pulled out for the sake of DRY
    }

    public void ShiftWest()
    {
        --_x;
        RecordLocation();
    }

    public void ShiftNorth()
    {
        ++_y;
        RecordLocation();
    }

    public void ShiftSouth()
    {
        --_y;
        RecordLocation();
    }

    public int UniqueLocationsVisited()
    {
        return _uniqueCoordinatesVisited.Count;
    }

    public (int, int) GetCurrentLocation()
    {
        return (_x, _y);
    }

    private void RecordLocation()
    {
        _uniqueCoordinatesVisited.Add(GetCurrentLocation());
    }
}