using Cint.RobotCleaner;
using Microsoft.Extensions.DependencyInjection;

// Using library for demoing purposes. Can of course be done without.
new ServiceCollection()
    .AddTransient<ICleaningService, CleaningService>() // DI for CleaningService not necessarily needed for this exercise
    .AddTransient<IConsoleReaderWriter, ConsoleReaderWriter>()
    .AddSingleton<IInstructionsRepo, InstructionsRepo>() // Singleton so that instructions are only read once
    .BuildServiceProvider()
    .GetService<ICleaningService>()
    ?.Clean();