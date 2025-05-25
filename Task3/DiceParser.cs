namespace Task3
{
    public static class DiceParser
    {
        public static List<Dice> Parse(string[] args)
        {
            if (args.Length < 3)
                throw new ArgumentException("At least 3 dice must be provided.");

            var diceList = new List<Dice>();
            foreach (var arg in args)
            {
                diceList.Add(new Dice(arg));
            }
            return diceList;
        }
    }
}
