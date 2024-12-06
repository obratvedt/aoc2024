using System.Text.RegularExpressions;
using Advent_of_code_2024.IO;

namespace Advent_of_code_2024._1;

public static partial class FirstOfDecember
{
    public static void Part1()
    {
        var fixedInput = ReadFileAndFixInput();

        var firstList = fixedInput.Select(l => l.Item1);
        var secondList = fixedInput.Select(l => l.Item2);
        

        var orderedFirstList = firstList.Order().ToList();
        var orderedSecondList = secondList.Order().ToList();

        var total = orderedFirstList.Zip(orderedSecondList, (first, second) => (first, second))
            .Select(t => Math.Abs(t.first - t.second))
            .Aggregate((a, b) => a + b);
        Console.WriteLine(total);

    }

    public static void Part2()
    {
        var fixedInput = ReadFileAndFixInput();
        var occurences = fixedInput.Select(l => l.Item2)
            .GroupBy(x => x)
            .Select(g => new Tuple<int, int>(g.Key, g.Count()));
        
        var total = fixedInput.Select(f => f.Item1 * occurences.FirstOrDefault(o => o.Item1 == f.Item1)?.Item2 ?? 0).Aggregate((a, b) => a + b);

        Console.WriteLine(total);

    }

    private static List<Tuple<int,int>> ReadFileAndFixInput()
    {
        var lines = FileReader.ReadFile("1/input.txt");

        var fixedInput = FixInput(lines);
        return fixedInput;
    }

    private static List<Tuple<int,int>> FixInput(List<string> lines)
    {
        return lines.Select(l => MyRegex().Replace(l, " "))
            .Select(l => l.Split(" "))
            .Select(l => new Tuple<int, int>(int.Parse(l[0]), int.Parse(l[1])))
            .ToList();
    }

    [GeneratedRegex(@"\s+")]
    private static partial Regex MyRegex();
    
}