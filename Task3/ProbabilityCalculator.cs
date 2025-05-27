namespace Task3
{
    public static class ProbabilityCalculator
    {
        public static double CalculateWinProbability(Dice diceA, Dice diceB)
        {
            int winCount = 0;
            int totalOutcomes = 0;

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (diceA.GetFace(i) > diceB.GetFace(j))
                    {
                        winCount++;
                    }
                    totalOutcomes++;
                }
            }
            return (double)winCount / totalOutcomes;
        }
    }
}
