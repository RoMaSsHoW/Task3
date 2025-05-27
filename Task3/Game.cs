using System.Security.Cryptography;

namespace Task3
{
    public class Game
    {
        private readonly List<Dice> diceList;

        public Game(List<Dice> diceList)
        {
            this.diceList = diceList;
        }

        public void Start()
        {
            Dice computerDice = null;
            Dice playerDice = null;

            if (IsPlayerFirst())
            {
                playerDice = SelectPlayerDice(null);
                computerDice = SelectComputerDice(playerDice);
            }
            else
            {
                playerDice = SelectPlayerDice(computerDice);
                computerDice = SelectComputerDice(null);
            }

            int computerRollIndex = PerformFairSelection(6, "my roll");
            int computerRoll = playerDice.GetFace(computerRollIndex);

            int playerRollIndex = PerformFairSelection(6, "your roll");
            int playerRoll = computerDice.GetFace(playerRollIndex);

            Console.WriteLine($"My roll result is {computerRoll}.");
            Console.WriteLine($"Your roll result is {playerRoll}.");
            if (playerRoll > computerRoll)
                Console.WriteLine($"You win ({playerRoll} > {computerRoll})!");
            else if (computerRoll > playerRoll)
                Console.WriteLine($"I win ({computerRoll} > {playerRoll})!");
            else
                Console.WriteLine($"It's a tie ({playerRoll} = {computerRoll})!");
        }

        private int GetUserInput(string message, int maxValue, IEnumerable<string> options)
        {
            while (true)
            {
                DisplayMenu(message, options);
                string input = GetUserSelection();
                if (input == "X") Environment.Exit(0);
                if (input == "?")
                {
                    TableRenderer.RenderProbabilityTable(diceList);
                    continue;
                }
                if (int.TryParse(input, out int choice) && choice >= 0 && choice < maxValue)
                {
                    return choice;
                }
                Console.WriteLine($"Invalid input. Enter a number from 0 to {maxValue - 1}, X or ?.\n");
            }
        }

        private void DisplayMenu(string message, IEnumerable<string> options)
        {
            Console.WriteLine(message);
            foreach (var option in options)
            {
                Console.WriteLine(option);
            }
            Console.WriteLine("X - exit");
            Console.WriteLine("? - Probability Table");
        }

        private string GetUserSelection()
        {
            Console.Write("Your selection: ");
            return Console.ReadLine()?.ToUpper() ?? string.Empty;
        }

        private int PerformFairSelection(int range, string description)
        {
            Console.WriteLine($"\nLet's {description}.");
            var (computerChoice, hmac, key) = FairRandomGenerator.GenerateFairNumber(range);
            Console.WriteLine($"I selected a random value in the range 0..{range - 1} (HMAC={hmac}).");
            var options = Enumerable.Range(0, range).Select(i => $"{i} - {i}");
            int playerChoice = GetUserInput("Try to guess my selection.", range, options);
            Console.WriteLine($"My selection: {computerChoice} (KEY={BitConverter.ToString(key).Replace("-", "")}).");
            int result = (computerChoice + playerChoice) % range;
            Console.WriteLine($"The fair number generation result is {computerChoice} + {playerChoice} = {result} (mod {range}).");
            return result;
        }

        private bool IsPlayerFirst()
        {
            int result = PerformFairSelection(2, "determine who makes the first move");
            bool isPlayerFirst = result == 0;
            Console.WriteLine(isPlayerFirst ? "You make the first move!" : "I make the first move!");
            return isPlayerFirst;
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
            var selectedDice = availableDice[RandomNumberGenerator.GetInt32(availableDice.Count)];
            Console.WriteLine($"\nI choose the {selectedDice} dice.");
            return selectedDice;
        }
    }
}