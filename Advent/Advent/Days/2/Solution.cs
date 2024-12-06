namespace Advent.Days._2;

public class Solution
{
    public class Report(string[] valueStrings)
    {
        private int[]? _values;
        public int[] GetValues() => _values ??= valueStrings.Select(int.Parse).ToArray();
        
        public bool IsSafe()
        {
            var ints = GetValues();

            if (ints.Length < 2)
            {
                return true;
            }

            var diffs = new List<int>();
            
            for (var i = 1; i < ints.Length; i++)
            {
                diffs.Add((ints[i - 1] - ints[i]));
            }
            
            return diffs.All(x => x is >= 1 and <= 3) || diffs.All(x => x is >= -3 and <= -1);
        }
    }

    public static string SolvePartOne()
        => File
            .ReadAllLines("Days/2/input.txt")
            .Select(x => x.Split(' '))
            .Select(x => new Report(x))
            .Count(x => x.IsSafe())
            .ToString();
}