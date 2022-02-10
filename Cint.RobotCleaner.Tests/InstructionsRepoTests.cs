using System;
using System.Linq;
using Cint.RobotCleaner.Models;
using Moq;
using Xunit;

namespace Cint.RobotCleaner.Tests;

public class InstructionsRepoTests
{
    private readonly Mock<IConsoleReaderWriter> _consoleReaderWriterMock = new();
    private InstructionsRepo _sut;
    
    private void InitSut()
    {
        _sut = new InstructionsRepo(_consoleReaderWriterMock.Object);
    }

    [Theory]
    [InlineData(0, 2)]
    [InlineData(1, 3)]
    [InlineData(2, 4)]
    public void Constructor_ValidInput_CallsReaderWriterMultipleTimes(int amountMovementInstructions, int expectedTimesCalled)
    {
        // Arrange
        ConfigureReaderWriterMockDynamically("E 1", amountMovementInstructions);
        InitSut();
        
        // Act
        // Assert
        _consoleReaderWriterMock.Verify(c => c.GetLine(), Times.Exactly(expectedTimesCalled));
    }

    [Theory]
    [InlineData("0 0", 0, 0)]
    [InlineData("-100 100", -100, 100)]
    [InlineData("100000 100000", 100000, 100000)]
    [InlineData("-100000 -100000", -100000, -100000)]
    public void GetStartLocationInstruction__ReturnsValidLocationInstruction(string location, int expectedX, int expectedY)
    {
        // Arrange
        ConfigureReaderWriterMockToReturnLocation(location);
        InitSut();
        
        // Act
        var startLocationInstruction = _sut.GetStartLocationInstruction();
        
        // Assert
        Assert.Equal(expectedX, startLocationInstruction.X);
        Assert.Equal(expectedY, startLocationInstruction.Y);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(10000)]
    public void GetMovementInstructions__ContainsCorrectAmountOfInstructions(int amountMovementInstructions)
    {
        // Arrange
        ConfigureReaderWriterMockDynamically("E 1", amountMovementInstructions);
        InitSut();
        
        // Act
        var movementInstructions = _sut.GetMovementInstructions();
        
        // Assert
        Assert.Equal(amountMovementInstructions, movementInstructions.Count);
    }

    [Theory]
    [InlineData("E 1", Direction.E, 1)]
    [InlineData("N 1", Direction.N, 1)]
    [InlineData("W 1", Direction.W, 1)]
    [InlineData("S 1", Direction.S, 1)]
    private void GetMovementInstructions__ReturnsValidInstructions(string movementInstruction,
        Direction expectedDirection, int expectedSteps)
    {
        // Arrange
        ConfigureReaderWriterMockDynamically(movementInstruction);
        InitSut();

        // Act
        var movementInstructions = _sut.GetMovementInstructions();
        
        // Assert
        Assert.Equal(expectedDirection, movementInstructions.Single().Direction);
        Assert.Equal(expectedSteps, movementInstructions.Single().Steps);
    }
    
    private void ConfigureReaderWriterMockDynamically(string movementInstruction, int amountMovementInstructions = 1)
    {
        _consoleReaderWriterMock.SetupSequence(c => c.GetLine())
            .Returns(Convert.ToString(amountMovementInstructions))
            .Returns(() =>
            {
                _consoleReaderWriterMock.Setup(c => c.GetLine()).Returns(movementInstruction);
                return "1 2";
            });
    }

    private void ConfigureReaderWriterMockToReturnLocation(string location)
    {
        _consoleReaderWriterMock.SetupSequence(c => c.GetLine())
            .Returns("0")
            .Returns(location);
    }
}