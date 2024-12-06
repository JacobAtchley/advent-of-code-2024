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

            var diffs = ints
                .Select((value, index) => new { value, index })
                .Where(x => x.index > 0)
                .Select(x => ints[x.index - 1] - x.value)
                .ToArray();
            
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