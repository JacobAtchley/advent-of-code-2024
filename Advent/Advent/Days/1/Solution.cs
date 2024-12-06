namespace Advent.Days._1;

public static class Solution
{
    private record Inputs(List<int> ListOne, List<int> ListTwo);
    private record Data(int Index, int Value);

    private record Group(int Similarity, int Count);

    private static Inputs ReadInputs()
    {
        var splits = File.ReadAllLines("Days/1/input.txt").Select(x => x.Split("   "));
        
        var input = new Inputs([], []);
        
        foreach (var x in splits)
        {
            input.ListOne.Add(int.Parse(x[0]));
            input.ListTwo.Add(int.Parse(x[1]));
        }

        return input;
    }

    private static IEnumerable<Data> Prep(List<int> array) => array
        .OrderBy(x => x)
        .Select((value, index) => new Data(index, value));

    public static int SolvePartOne()
    {
        var (listOne, listTwo) = ReadInputs();
        
        return Prep(listOne)
            .Join(Prep(listTwo),
                x => x.Index,
                y => y.Index,
                (x, y) => Math.Abs(x.Value - y.Value))
            .Sum(x => x);
    }

    public static int SolvePartTwo()
    {
        var (listOne, listTwo) = ReadInputs();
        
        var listTwoHash = listTwo
            .GroupBy(x => x)
            .ToDictionary(x => x.Key, x => x.Count());

        var sum = listOne.Select(x => new Group(x, listTwoHash.GetValueOrDefault(x, 0)))
            .Select(x => x.Count * x.Similarity)
            .Sum();

        return sum;
    }
}