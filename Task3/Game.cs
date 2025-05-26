namespace Task3
{
    public class Game
    {
        private readonly List<Dice> diceList;

        public Game(List<Dice> diceList)
        {
            this.diceList = diceList;
        }

        public void Run()
        {
            //ProbabilityTable.DisplayProbabilityTable(diceList);
            bool isPlayerFirst = DetermineFirstMove();
            PlayGame(isPlayerFirst);
        }

        private int GetUserInput(string messageForUser, int maxValue, IEnumerable<string> options)
        {
            Console.WriteLine(messageForUser);
            foreach (var option in options)
                Console.WriteLine(option);
            Console.WriteLine("X - exit");
            Console.WriteLine("? - help");

            while (true)
            {
                Console.Write("Your selection: ");
                string input = Console.ReadLine()?.ToUpper();
                if (input == "X") Environment.Exit(0);
                if (input == "?")
                {
                    Console.WriteLine($"Enter a number from 0 to {maxValue - 1}, X to exit, or ? for help.");
                    continue;
                }
                if (int.TryParse(input, out int choice) && choice >= 0 && choice < maxValue)
                    return choice;
                Console.WriteLine($"Invalid input. Enter a number from 0 to {maxValue - 1}, X, or ?.");
            }
        }

        // Выполнение честного выбора числа
        // range: диапазон чисел (2 для 0..1, 6 для 0..5)
        // description: описание действия (например, "determine who makes the first move")
        // resultHandler: делегат для обработки результата (суммы чисел по модулю)
        // Возвращает результат, обработанный делегатом
        private T PerformFairSelection<T>(int range, string description, Func<int, int, T> resultHandler)
        {
            Console.WriteLine($"\nLet's {description}.");
            var (computerChoice, hmac, key) = FairRandomGenerator.GenerateFairNumber(range);
            Console.WriteLine($"I selected a random value in the range 0..{range - 1} (HMAC={hmac}).");
            var options = Enumerable.Range(0, range).Select(i => $"{i} - {i}");
            int playerChoice = GetUserInput("Try to guess my selection.", range, options);
            Console.WriteLine($"My selection: {computerChoice} (KEY={BitConverter.ToString(key).Replace("-", "")}).");
            int result = (computerChoice + playerChoice) % range;
            Console.WriteLine($"The fair number generation result is {computerChoice} + {playerChoice} = {result} (mod {range}).");
            return resultHandler(computerChoice, result);
        }

        private bool DetermineFirstMove()
        {
            return PerformFairSelection(2, "determine who makes the first move",
                (computerChoice, result) =>
                {
                    bool isPlayerFirst = result == 0;
                    Console.WriteLine(isPlayerFirst ? "You make the first move!" : "I make the first move!");
                    return isPlayerFirst;
                });
        }

        // Основной игровой процесс
        private void PlayGame(bool isPlayerFirst)
        {
            Dice computerDice = null;
            Dice playerDice = null;

            // Выбор кубиков
            if (isPlayerFirst)
            {
                playerDice = SelectPlayerDice(null);
                computerDice = SelectComputerDice(playerDice);
            }
            else
            {
                computerDice = SelectComputerDice(null);
                playerDice = SelectPlayerDice(computerDice);
            }

            // Броски кубиков
            int computerRoll = PerformFairSelection(6, "my roll",
                (computerChoice, result) => playerDice.GetFace(result));
            int playerRoll = PerformFairSelection(6, "your roll",
                (computerChoice, result) => computerDice.GetFace(result));

            // Определение победителя
            Console.WriteLine($"My roll result is {computerRoll}.");
            Console.WriteLine($"Your roll result is {playerRoll}.");
            if (playerRoll > computerRoll)
                Console.WriteLine($"You win ({playerRoll} > {computerRoll})!");
            else if (computerRoll > playerRoll)
                Console.WriteLine($"I win ({computerRoll} > {playerRoll})!");
            else
                Console.WriteLine($"It's a tie ({playerRoll} = {computerRoll})!");
        }

        private Dice SelectPlayerDice(Dice excludedDice)
        {
            var options = diceList
                .Select((dice, index) => (dice, index))
                .Where(x => x.dice != excludedDice)
                .Select(x => $"{x.index} - {x.dice}");
            int choice = GetUserInput("\nChoose your dice:", diceList.Count, options);
            return diceList[choice];
        }
         
        private Dice SelectComputerDice(Dice excludedDice)
        {
            var availableDice = diceList.Where(d => d != excludedDice).ToList();
            var selectedDice = availableDice[Random.Shared.Next(availableDice.Count)];
            Console.WriteLine($"\nI choose the {selectedDice} dice.");
            return selectedDice;
        }
    }
}
