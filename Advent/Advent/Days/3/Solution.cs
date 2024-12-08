using System.Text.RegularExpressions;

namespace Advent.Days._3;

public static partial class Solution
{
    private record Instruction(int Index, bool IsDo);

    public static string SolvePartOne()
    {
        var input = File.ReadAllText("Days/3/input.txt");

        var sum = MulRegEx()
            .Matches(input)
            .Select(x =>
            {
                var numbers = NumberRegEx().Matches(x.Value);

                return int.Parse(numbers[0].Value) * int.Parse(numbers[1].Value);
            })
            .Sum();

        return sum.ToString();
    }

    public static string SolvePartTwo()
    {
        var input = File.ReadAllText("Days/3/input.txt");

        var doInstructions = DoRegEx()
            .Matches(input)
            .Select(x => x.Index)
            .Select(x => new Instruction(x, true))
            .ToList();

        var dontInstructions = DontRegEx()
            .Matches(input)
            .Select(x => x.Index)
            .Select(x => new Instruction(x, false))
            .ToList();

        var allInstructions = doInstructions
            .Concat(dontInstructions)
            .OrderBy(x => x.Index)
            .ToList();

        var multiplyMatches = MulRegEx().Matches(input);

        var sum = multiplyMatches
            .Select(x =>
            {
                var multiplyIndex = x.Index;

                var multiplyInstructions = allInstructions
                    .Where(i => i.Index < multiplyIndex)
                    .ToList();

                var lastDont = multiplyInstructions.LastOrDefault(i => i.IsDo is false);
                var lastDo = multiplyInstructions.LastOrDefault(i => i.IsDo);

                var isDisabled = (lastDo?.Index ?? 0) < (lastDont?.Index ?? 0);

                if (isDisabled)
                {
                    return 0;
                }

                var numbers = NumberRegEx().Matches(x.Value);
                return int.Parse(numbers[0].Value) * int.Parse(numbers[1].Value);
            })
            .Sum();

        return sum.ToString();
    }

    [GeneratedRegex(@"mul\(\d*,\d*\)", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled,
        "en-US")]
    private static partial Regex MulRegEx();

    [GeneratedRegex(("\\d+"))]
    private static partial Regex NumberRegEx();

    [GeneratedRegex(@"don't\(\)", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled, "en-US")]
    private static partial Regex DontRegEx();

    [GeneratedRegex(@"do\(\)", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled, "en-US")]
    private static partial Regex DoRegEx();
}