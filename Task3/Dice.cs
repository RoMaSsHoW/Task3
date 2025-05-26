namespace Task3
{
    public class Dice
    {
        public int[] Faces { get; }

        public Dice(string fascesWithSeparator)
        {
            var parts = fascesWithSeparator.Split(',');
            if (parts.Length != 6)
            {
                throw new ArgumentException("Each die must have exactly 6 faces.");
            }
            Faces = new int[parts.Length];
            for (int i = 0; i < parts.Length; i++)
            {
                if (!int.TryParse(parts[i], out Faces[i]))
                {
                    throw new ArgumentException("All die faces must be integers.");
                }
            }
        }
    }
}
