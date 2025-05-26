namespace Task3
{
    public class Dice
    {
        private int[] faces;

        public Dice(string fascesWithSeparator)
        {
            var parts = ParseToIntArray(fascesWithSeparator);
            faces = new int[parts.Length];
            for (int i = 0; i < parts.Length; i++)
            {
                faces[i] = parts[i];
            }
        }

        public int[] GetFaces() => faces;

        public override string ToString() => $"[{string.Join(",", faces)}]";

        private int[] ParseToIntArray(string fascesWithSeparator)
        {
            var parts = fascesWithSeparator.Split(',')
                                           .Select(ParseIntOrThrow)
                                           .ToArray();
            if (IsValidParts(parts))
            {
                return parts;
            }
            throw new ArgumentException("Each die must have exactly 6 faces.");
        }

        private int ParseIntOrThrow(string s)
        {
            if (int.TryParse(s, out int num))
            {
                return num;
            }
            throw new ArgumentException($"Invalid value '{s}'. Only integers are allowed.");
        }

        private bool IsValidParts(int[] parts)
        {
            if (parts.Length != 6)
            {
                return false;
            }
            return true;
        }
    }
}
