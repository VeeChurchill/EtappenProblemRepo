namespace EtappenProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            var consoleHandler = new ConsoleHandler();

            consoleHandler.InitializeValuesWithUserInput();

            var algorithm = new Algorithm(consoleHandler.StageLengths, consoleHandler.AmountDays, consoleHandler.TotalLength);
            algorithm.FindSolution();

            consoleHandler.PrintSolution(algorithm.LengthsPerDaySolution, algorithm.MaxLength);
        }
    }
}