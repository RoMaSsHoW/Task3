namespace Task3
{
    public class Dice
    {
        private int[] _faces;

        public int[] Faces => _faces;

        public Dice(string fascesWithSeparator)
        {
            var parts = fascesWithSeparator.Split(',');
            if (parts.Length != 6)
            {

            }
        }
    }
}
