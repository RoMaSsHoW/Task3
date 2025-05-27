namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var diceList = DiceParser.Parse(args);
                var game = new Game(diceList);
                game.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}