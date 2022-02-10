namespace Cint.RobotCleaner;

// Keeping simple interface in class file of standard implementation for simplicity.
public interface IConsoleReaderWriter
{
    string? GetLine();

    void WriteLine(string line);
}

public class ConsoleReaderWriter : IConsoleReaderWriter
{
    public string? GetLine()
    {
        return Console.ReadLine();
    }

    public void WriteLine(string line)
    {
        Console.WriteLine(line);
    }
}