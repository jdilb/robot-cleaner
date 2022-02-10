namespace Cint.RobotCleaner;

// Keeping simple interface in class file of standard implementation for simplicity.
public interface ICleaningService
{
    public void Clean();
}

public class CleaningService : ICleaningService
{
    private readonly IConsoleReaderWriter _consoleReaderWriter;
    private readonly IInstructionsRepo _instructionsRepo;

    public CleaningService(IInstructionsRepo instructionsRepo, IConsoleReaderWriter consoleReaderWriter)
    {
        _instructionsRepo = instructionsRepo;
        _consoleReaderWriter = consoleReaderWriter;
    }

    public void Clean()
    {
        var (startingX, startingY) = GetStartLocation();

        // DI of LocationTracker for testability
        var robot = new Robot(new LocationTracker(startingX, startingY));

        foreach (var instruction in _instructionsRepo.GetMovementInstructions())
            robot.ProcessInstruction(instruction);

        _consoleReaderWriter.WriteLine($"=> Cleaned: {robot.PlacesCleaned()}");

        (int, int) GetStartLocation()
        {
            var startInstruction = _instructionsRepo.GetStartLocationInstruction();
            return (startInstruction.X, startInstruction.Y);
        }
    }
}