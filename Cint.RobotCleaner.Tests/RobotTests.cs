using System;
using System.Linq.Expressions;
using Cint.RobotCleaner.Models;
using Moq;
using Xunit;

namespace Cint.RobotCleaner.Tests;

public class RobotTests
{
    private readonly Mock<ILocationTracker> _locationTrackerMock = new();
    private readonly Robot _sut;
    
    public RobotTests()
    {
        _sut = new Robot(_locationTrackerMock.Object);
    }

    [Theory]
    [InlineData(Direction.E, 5)]
    [InlineData(Direction.W, 1)]
    [InlineData(Direction.S, 3)]
    [InlineData(Direction.N, 10)]
    public void ProcessInstruction_ValidInstruction_CallsCorrectDirectionOnLocationTracker(Direction direction, int steps)
    {
        // Arrange
        var movementInstruction = GenerateMovementInstruction(direction, steps);
        var expectedExpression = GetExpectedExpression(direction);
        
        // Act
        _sut.ProcessInstruction(movementInstruction);

        // Assert
        _locationTrackerMock.Verify(expectedExpression, Times.Exactly(steps));
    }

    [Fact]
    public void PlacesCleaned__CallsUniqueLocationsVisited()
    {
        // Arrange
        _locationTrackerMock.Setup(l => l.UniqueLocationsVisited()).Returns(25);
        
        // Act
        _sut.PlacesCleaned();
        
        // Assert
        _locationTrackerMock.Verify(l => l.UniqueLocationsVisited(), Times.Once);
    }
    

    private static MovementInstruction GenerateMovementInstruction(Direction direction, int steps)
    {
        return new MovementInstruction
        {
            Direction = direction,
            Steps = steps
        };
    }

    private static Expression<Action<ILocationTracker>> GetExpectedExpression(Direction d) => d switch
    {
        Direction.E => l => l.ShiftEast(),
        Direction.N => l => l.ShiftNorth(),
        Direction.W => l => l.ShiftWest(),
        Direction.S => l => l.ShiftSouth(),
        _ => throw new ArgumentOutOfRangeException(nameof(d), d, null)
    };

}