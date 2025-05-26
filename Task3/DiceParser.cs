namespace Task3
{
    public static class DiceParser
    {
        public static List<Dice> Parse(string[] args)
        {
            var diceList = new List<Dice>();
            if (IsValidArgsLength(args))
            {
                foreach (var arg in args)
                {
                    diceList.Add(new Dice(arg));
                }
            }
            return diceList;
        }

        private static bool IsValidArgsLength(string[] args)
        {
            if (args.Length < 3)
            {
                throw new ArgumentException("At least 3 dice must be provided.");
            }
            return true;
        }
    }
}
