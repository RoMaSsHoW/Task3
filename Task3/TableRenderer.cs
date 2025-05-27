using ConsoleTables;

namespace Task3
{
    public static class TableRenderer
    {
        public static void RenderProbabilityTable(List<Dice> diceList)
        {
            Console.WriteLine("\nProbability Table:");
            var table = new ConsoleTable(new string[] { "User dice v" }.Concat(diceList.Select(d => d.ToString())).ToArray());
            for (int i = 0; i < diceList.Count; i++)
            {
                var row = new List<string> { diceList[i].ToString() };
                for (int j = 0; j < diceList.Count; j++)
                {
                    if (i == j)
                        row.Add("- (0.3333)");
                    else
                        row.Add(ProbabilityCalculator.CalculateWinProbability(diceList[i], diceList[j]).ToString("0.0000"));
                }

                table.AddRow(row.ToArray());
            }
            table.Write(Format.Alternative);
        }
    }
}
