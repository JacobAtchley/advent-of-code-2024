namespace Advent.Days._2;

public record Diff(int Index, int Difference)
{
    public bool IsAscending = Difference > 0;
    public bool IsDescending = Difference < 0;
    public int Absolute = Math.Abs(Difference);
    public bool IsSafe => Absolute is >= 1 and <= 3;
}

public class Solution
{
    public class Report(string[] valueStrings)
    {
        private int[]? _values;
        public int[] GetValues() => _values ??= valueStrings.Select(int.Parse).ToArray();

        private static bool ComputeSafe(int[] ints, int? skipIndex = null)
        {
            if (ints.Length < 2)
            {
                return true;
            }
            
            var values = skipIndex is null
                ? ints
                : ints.Where((_, index) => index != skipIndex).ToArray();

            var diffs = values
                .Select((value, index) => new { value, index })
                .Where(x => x.index != 0)
                .Select(x => new Diff(x.index, values[x.index - 1] - x.value))
                .ToArray();

            var isAllAscending = diffs.All(x => x.IsAscending);
            var isAllDescending = diffs.All(x => x.IsDescending);
            var isAllSafe = diffs.All(x => x.IsSafe);

            var isSafe = (isAllAscending || isAllDescending) && isAllSafe;

            return isSafe;
        }

        public bool IsSafe(bool useDampener)
        {
            var ints = GetValues();
            var isSafe = ComputeSafe(ints);

            if (useDampener is false)
            {
                return isSafe;
            }
            
            for (var i = 0; i < ints.Length; i++)
            {
                if (ComputeSafe(ints, i))
                {
                    return true;
                }
            }

            return false;
        }
    }
    
    private static string Solve(bool useDapmener)
        => File
            .ReadAllLines("Days/2/input.txt")
            .Select(x => x.Split(' '))
            .Select(x => new Report(x))
            .Count(x => x.IsSafe(useDapmener))
            .ToString();

    public static string SolvePartOne()
        => Solve(false);
    
    public static string SolvePartTwo()
        => Solve(true);
}