using Cint.RobotCleaner.Models;

namespace Cint.RobotCleaner;

public interface IInstructionsRepo
{
    StartLocationInstruction GetStartLocationInstruction();
    ICollection<MovementInstruction> GetMovementInstructions();
}

public class InstructionsRepo : IInstructionsRepo
{
    private readonly IConsoleReaderWriter _consoleReaderWriter;
    private readonly List<MovementInstruction> _instructions = new();
    private readonly StartLocationInstruction _startLocationInstruction = new();
    private int _numberOfInstructions;


    public InstructionsRepo(IConsoleReaderWriter consoleReaderWriter)
    {
        _consoleReaderWriter = consoleReaderWriter;
        FetchInstructions();
    }

    public StartLocationInstruction GetStartLocationInstruction()
    {
        return _startLocationInstruction;
    }

    public ICollection<MovementInstruction> GetMovementInstructions()
    {
        return _instructions;
    }

    // This method can be made available on the interface as well if the requirements suggest that there should be 
    // control over the retrieval of information (from DB or std in) in a separate step after construction.
    // This would also enhance testability. However, these operations are bundled in this example to provide
    // atomicity and avoid corrupted state.
    private void FetchInstructions()
    {
        ReadNumberOfMovementInstructions();
        ReadStartingLocation();
        ReadMovementInstructions();
    }

    private void ReadStartingLocation()
    {
        var startingPositionStringArray = ReadAndSplitLineByWhitespace();

        // Validation should be added in an actual implementation
        _startLocationInstruction.X = Convert.ToInt32(startingPositionStringArray[0]);
        _startLocationInstruction.Y = Convert.ToInt32(startingPositionStringArray[1]);
    }

    private void ReadMovementInstructions()
    {
        for (var i = 0; i < _numberOfInstructions; i++)
        {
            var instructionStringArray = ReadAndSplitLineByWhitespace();
            
            // Validation should be added in an actual implementation
            var (direction, steps) = 
                (Enum.Parse<Direction>(instructionStringArray[0]), Convert.ToInt32(instructionStringArray[1]));
            
            _instructions.Add(new MovementInstruction {Direction = direction, Steps = steps});
        }
    }

    private string[] ReadAndSplitLineByWhitespace()
    {
        return _consoleReaderWriter.GetLine()!.Split(" ");
    }

    private void ReadNumberOfMovementInstructions()
    {
        // Validation should be added in an actual implementation
        _numberOfInstructions = Convert.ToInt32(_consoleReaderWriter.GetLine());
    }
}