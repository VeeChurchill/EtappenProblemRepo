namespace EtappenProblem
{
    public class ConsoleHandler
    {
        public int AmountDays { get; private set; } = int.MaxValue;
        public List<int> StageLengths { get; private set; } = new();
        public int TotalLength { get; private set; }

        public void InitializeValuesWithUserInput()
        {
            Console.WriteLine("Aus wie vielen Etappen soll die Strecke bestehen?");
            var amountStages = GetPositiveIntInput();

            Console.WriteLine("In wie vielen Tagen soll die Strecke zurück gelegt werden?");
            while(AmountDays > amountStages)
            {
                AmountDays = GetPositiveIntInput();
            }

            Console.WriteLine("Geben Sie die Längen der Strecken an:");
            for (int i = 0; i < amountStages; i++)
            {
                var stage = GetPositiveIntInput();
                StageLengths.Add(stage);
                TotalLength += stage;
            }
        }

        private int GetPositiveIntInput()
        {
            var result = 0;
            var successfulParse = false;

            while (!successfulParse)
            {
                var input = Console.ReadLine();
                successfulParse = Int32.TryParse(input, out result);

                // We're only allowing int values above 0
                if (successfulParse && result <= 0)
                {
                    successfulParse = false;
                }
            }
            return result;
        }

        public void PrintSolution(List<int> lengthsPerDaySolution, int maxLength)
        {
            for (int i = 0; i < AmountDays; i++)
            {
                Console.WriteLine($"{i+1}. Tag: {lengthsPerDaySolution[i]} km");
            }

            Console.WriteLine($"\nMaximum: {maxLength} km");
        }
    }
}
