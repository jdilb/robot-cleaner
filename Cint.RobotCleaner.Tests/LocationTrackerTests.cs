using System;
using System.Collections.Generic;
using Xunit;

namespace Cint.RobotCleaner.Tests;

public class LocationTrackerTests
{
    private readonly LocationTracker _sut;

    public LocationTrackerTests()
    {
        _sut = new LocationTracker(0, 0);
    }

    [Fact]
    public void GetCurrentLocation__ReturnsCurrentLocation()
    {
        // Arrange
        int expectedY;
        var expectedX = expectedY = -10000;
        var sut = new LocationTracker(expectedX, expectedY);

        // Act
        var location = sut.GetCurrentLocation();

        // Assert
        Assert.Equal((expectedX, expectedY), location);
    }

    [Theory]
    [MemberData(nameof(MovementExpectedLocationData))]
    public void ShiftXX__MovesCurrentLocation(Action<ILocationTracker> action, (int, int) expectedLocation)
    {
        // Arrange
        // Act
        action.Invoke(_sut);
        var location = _sut.GetCurrentLocation();
        
        // Assert
        Assert.Equal(expectedLocation, location);
    }
    
    [Fact]
    public void Constructor__RecordsStartLocation()
    {
        // Arrange
        // Act
        // Assert
        Assert.Equal(1, _sut.UniqueLocationsVisited());
    }
    
    [Theory]
    [MemberData(nameof(MovementData))]
    public void ShiftXX__RecordsOneAdditionalLocation(Action<ILocationTracker> action)
    {
        // Arrange
        const int expectedUniqueLocation = 2;
        // Act
        action.Invoke(_sut);
        
        // Assert
        Assert.Equal(expectedUniqueLocation, _sut.UniqueLocationsVisited());
    }
    
    [Theory]
    [MemberData(nameof(MovementExpectedUniqueLocationData))]
    public void ShiftXX__RecordsLocationsUniquely(List<Action<ILocationTracker>> actionList, int expectedUniqueLocations)
    {
        // Arrange
        // Act
        foreach (var action in actionList)
        {
            action.Invoke(_sut);
        }
        // Assert
        Assert.Equal(expectedUniqueLocations, _sut.UniqueLocationsVisited());
    }
    
    public static IEnumerable<object[]> MovementExpectedLocationData =>
        new List<object[]>
        {
            new object[] { (ILocationTracker sut) => sut.ShiftEast(), (1, 0) },
            new object[] { (ILocationTracker sut) => sut.ShiftNorth(), (0, 1) },
            new object[] { (ILocationTracker sut) => sut.ShiftSouth(), (0, -1) },
            new object[] { (ILocationTracker sut) => sut.ShiftWest(), (-1, 0) }
        };
    
    public static IEnumerable<object[]> MovementData =>
        new List<object[]>
        {
            new object[] { (ILocationTracker sut) => sut.ShiftEast()},
            new object[] { (ILocationTracker sut) => sut.ShiftNorth()},
            new object[] { (ILocationTracker sut) => sut.ShiftSouth()},
            new object[] { (ILocationTracker sut) => sut.ShiftWest()},
        };
    
    public static IEnumerable<object[]> MovementExpectedUniqueLocationData =>
        new List<object[]>
        {
            new object[]
            {
                new List<Action<ILocationTracker>>
                {
                    (sut) => sut.ShiftEast(),
                    (sut) => sut.ShiftWest()
                }, 2
            },
            new object[]
            {
                new List<Action<ILocationTracker>>
                {
                    (sut) => sut.ShiftEast(),
                    (sut) => sut.ShiftEast(),
                    (sut) => sut.ShiftWest()
                }, 3
            },
            new object[]
            {
                new List<Action<ILocationTracker>>
                {
                    (sut) => sut.ShiftEast(),
                    (sut) => sut.ShiftNorth(),
                    (sut) => sut.ShiftWest(),
                    (sut) => sut.ShiftWest(),
                    (sut) => sut.ShiftSouth(),
                    (sut) => sut.ShiftSouth(),
                    (sut) => sut.ShiftEast(),
                    (sut) => sut.ShiftEast(),
                    (sut) => sut.ShiftNorth(),
                    (sut) => sut.ShiftWest()
                }, 9
            }
        };
    
    
    
}