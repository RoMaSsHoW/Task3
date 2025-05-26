namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            var diceList = DiceParser.Parse(args);
            var game = new Game(diceList);
            game.Run();
        }
    }
}