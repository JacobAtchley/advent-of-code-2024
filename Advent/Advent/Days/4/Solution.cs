namespace Advent.Days._4;

public static class Solution
{
    public record Coordinate(int X, int Y)
    {
        public override int GetHashCode() => HashCode.Combine(X, Y);
    }

    public record CoordinateList(Coordinate[] Coordinates)
    {
        public override int GetHashCode()
            => Coordinates.Aggregate(0, (hash, coordinate) => hash ^ coordinate.GetHashCode());
    }

    private static char GetCharAtCoordinate(int x, int y, string[] lines)
    {
        if (y < 0 || y >= lines.Length)
        {
            return '\0';
        }

        var line = lines[y];

        if (x < 0 || x >= line.Length)
        {
            return '\0';
        }

        return line[x];
    }


    private static IEnumerable<CoordinateList> GetCoordinateSearch(int x, int y, string word)
    {
        var length = word.Length;

        yield return new CoordinateList(MoveUp().ToArray());
        yield return new CoordinateList(MoveDown().ToArray());
        yield return new CoordinateList(MoveLeft().ToArray());
        yield return new CoordinateList(MoveRight().ToArray());
        
        yield return new CoordinateList(MoveDiagonalUpRight().ToArray());
        yield return new CoordinateList(MoveDiagonalDownRight().ToArray());
        
        yield return new CoordinateList(MoveDiagonalUpLeft().ToArray());
        yield return new CoordinateList(MoveDiagonalDownLeft().ToArray());


        yield break;

        IEnumerable<Coordinate> MoveUp()
        {
            yield return new(x, y);
            
            for (var i = 0; i < length; i++)
            {
                yield return new(x, y - i);
            }
        }

        IEnumerable<Coordinate> MoveDown()
        {
            for (var i = 0; i < length; i++)
            {
                yield return new(x, y + i);
            }
        }

        IEnumerable<Coordinate> MoveLeft()
        {
            for (var i = 0; i < length; i++)
            {
                yield return new(x - i, y);
            }
        }

        IEnumerable<Coordinate> MoveRight()
        {
            for (var i = 0; i < length; i++)
            {
                yield return new(x + i, y);
            }
        }

        IEnumerable<Coordinate> MoveDiagonalUpRight()
        {
            for (var i = 0; i < length; i++)
            {
                yield return new(x + i, y - i);
            }
        }

        IEnumerable<Coordinate> MoveDiagonalDownRight()
        {
            for (var i = 0; i < length; i++)
            {
                yield return new(x + i, y + i);
            }
        }

        IEnumerable<Coordinate> MoveDiagonalUpLeft()
        {
            for (var i = 0; i < length; i++)
            {
                yield return new(x - i, y - i);
            }
        }

        IEnumerable<Coordinate> MoveDiagonalDownLeft()
        {
            for (var i = 0; i < length; i++)
            {
                yield return new(x - i, y + i);
            }
        }
    }


    private static void Search(int y, string word, string[] lines, Dictionary<int, CoordinateList> found)
    {
        for (var x = 0; x < lines.Length; x++)
        {
            var searchCoordinateList = GetCoordinateSearch(x, y, word);

            foreach (var direction in searchCoordinateList)
            {
                var chars = direction
                    .Coordinates
                    .Select(coordinate => GetCharAtCoordinate(coordinate.X, coordinate.Y, lines))
                    .ToList();

                CoordinateList? potentialMatch = null;

                if (string.Join(string.Empty, chars).Equals(word, StringComparison.InvariantCultureIgnoreCase))
                {
                    potentialMatch = new CoordinateList(direction.Coordinates);
                }
                else
                {
                    chars.Reverse();

                    if (string.Join(string.Empty, chars).Equals(word, StringComparison.InvariantCultureIgnoreCase))
                    {
                        potentialMatch = new CoordinateList(direction.Coordinates.Reverse().ToArray());
                    }
                }

                if (potentialMatch is null)
                {
                    continue;
                }

                found[potentialMatch.GetHashCode()] = potentialMatch;
            }
        }
    }

    public static string PartOne()
    {
        const string search = "XMAS";

        var lines = File
            .ReadAllText("Days/4/input.txt")
            .Split(Environment.NewLine)
            .ToArray();

        var found = new Dictionary<int, CoordinateList>();

        for (var y = 0; y < lines.Length; y++)
        {
            Search(y, search, lines, found);
        }
        
        return found.Count.ToString();
    }
}