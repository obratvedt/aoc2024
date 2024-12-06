using Advent_of_code_2024.IO;

namespace Advent_of_code_2024._2;

public static class SecondOfDecember
{
    public static void Part1()
    {
        var inputList = FileReader.ReadFile("2/input.txt");
        var withListOfNumbers = inputList.Select(l => l.Split(" ").Select(int.Parse)).ToList();
        var result = withListOfNumbers.Select(l =>
        {
            var listVersion = l.ToList();
            if (!CheckSafe(listVersion)) return false;
            var sortedAscending = listVersion.OrderBy(i => i).ToList();
            var sortedDescending = listVersion.OrderByDescending(i => i).ToList();
            if (!sortedAscending.SequenceEqual(listVersion))
            {
                if (!sortedDescending.SequenceEqual(listVersion))
                {
                    return false;
                }
            }
            return true;
        });
        
        var count = result.Count(r => r);
        Console.WriteLine(count);
        
    }

    private static bool CheckSafe(List<int> listVersion)
    {
        for (var i =1; i < listVersion.Count; i++)
        {
            var previous = listVersion[i - 1];
            var current = listVersion[i];
            var difference = Math.Abs(previous - current);
            if (difference is < 1 or >= 4)
            {
                return false;
            }
        }

        return true;
    }
    public static void Part2()
    {
        var inputList = FileReader.ReadFile("2/input.txt");
        var withListOfNumbers = inputList.Select(l => l.Split(" ").Select(int.Parse)).ToList();
        var result = withListOfNumbers.Select(l =>
        {
            var listVersion = l.ToList();
            var checkSafeWithDampener = CheckSafeWithDampener(listVersion);
            return checkSafeWithDampener;
        });
        
        var count = result.Count(r => r);
        Console.WriteLine(count);

    }
    
    private static bool CheckSafeWithDampener(List<int> listVersion)
    {
        var success = Check(listVersion);
        if (success)
            return true;
        for (var i = 0; i < listVersion.Count; i++)
        {
            var listCopy = listVersion.Select(l => l).ToList();

            listCopy.RemoveAt(i);
            var successWithDampener = Check(listCopy);
            if (successWithDampener)
            {
                return true;
            }
        }
        return false;
    }
    
    private static bool Check(List<int> listVersion)
    {
        for (var i =1; i < listVersion.Count; i++)
        {
            var previous = listVersion[i - 1];
            var current = listVersion[i];
            var difference = Math.Abs(previous - current);
            if (difference is < 1 or >= 4)
            {
                return false;
            }
        }
        
        var sortedAscending = listVersion.OrderBy(i => i).ToList();
        var sortedDescending = listVersion.OrderByDescending(i => i).ToList();
        return sortedAscending.SequenceEqual(listVersion) || sortedDescending.SequenceEqual(listVersion);
    }
}