using System.Collections.Generic;
using Cint.RobotCleaner.Models;
using Moq;
using Xunit;

namespace Cint.RobotCleaner.Tests;

public class CleaningServiceTests
{
    private readonly Mock<IInstructionsRepo> _instructionsRepoMock = new();
    private readonly Mock<IConsoleReaderWriter> _consoleReaderWriterMock = new();
    private readonly CleaningService _sut;

    public CleaningServiceTests()
    {
        ConfigureInstructionsRepo();
        _sut = new CleaningService(_instructionsRepoMock.Object, _consoleReaderWriterMock.Object);
    }

    [Fact]
    public void Clean__CallsGetStartLocationInstructionOnce()
    {
        // Arrange
        // Act
        _sut.Clean();
        
        // Assert
        _instructionsRepoMock.Verify(i => i.GetStartLocationInstruction(), Times.Once);
    }
    
    [Fact]
    public void Clean__CallsGetMovementInstructionsOnce()
    {
        // Arrange
        // Act
        _sut.Clean();
        
        // Assert
        _instructionsRepoMock.Verify(i => i.GetMovementInstructions(), Times.Once);
    }
    
    [Fact]
    public void Clean__WritesToConsoleReaderWriter()
    {
        // Arrange
        const string expectedOutput = "=> Cleaned: 1";
        // Act
        _sut.Clean();
        
        // Assert
        _consoleReaderWriterMock
            .Verify(c => c.WriteLine(It.Is<string>(i => string.Equals(expectedOutput, i))), Times.Once);
    }

    private void ConfigureInstructionsRepo()
    {
        _instructionsRepoMock.Setup(i => i.GetStartLocationInstruction()).Returns(
            new StartLocationInstruction()
            {
                X = 1,
                Y = 1
            });
        
        _instructionsRepoMock.Setup(i => i.GetMovementInstructions()).Returns(
            new List<MovementInstruction>());
    }
}