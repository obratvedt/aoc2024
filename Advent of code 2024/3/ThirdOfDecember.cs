using System.Text.RegularExpressions;
using Advent_of_code_2024.IO;

namespace Advent_of_code_2024._3;

public partial class ThirdOfDecember
{
    public static void Part1()
    {
        var readFile = FileReader.ReadFile("3/input.txt");
        var plainText = string.Join("", readFile.ToArray());
        var matches = Part1Regex().Matches(plainText);
        var total = matches.Select(match => Multiply(match.Value)).Aggregate((a, b) => a + b);;
        Console.WriteLine(total);
    }
    
    public static void Part2()
    {
        var readFile = FileReader.ReadFile("3/input.txt");
        var plainText = string.Join("", readFile.ToArray());
        
        var regex = Part2Regex();
        
        var matches = regex.Matches(plainText);
        var shouldBeMultiplied = true;
        var multiplyList = new List<int>();
        foreach (Match match in matches)
        {
            var matchedValue = match.Value;
            if (matchedValue.Contains("don"))
            {
                shouldBeMultiplied = false;
            }
            else if (matchedValue.Contains("do"))
            {
                shouldBeMultiplied = true;
            }
            else
            {
                if (shouldBeMultiplied)
                {
                    multiplyList.Add(Multiply(matchedValue));
                }  
            }
        }
        var total = multiplyList.Aggregate((a, b) => a + b);;
        Console.WriteLine(total);
    }
    
    private static int Multiply(string input)
    {
        var split = input.Split(",");
        const string pattern = @"\d+";
        

        var first = split[0];
        var second = split[1];
        var firstOnlyDigits = DigitRegex().Matches(first)[0].Value;
        var secondOnlyDigits = DigitRegex().Matches(second)[0].Value;
        
        return int.Parse(firstOnlyDigits) * int.Parse(secondOnlyDigits);
    }

    [GeneratedRegex(@"mul\(\d{1,3},\d{1,3}\)")]
    private static partial Regex Part1Regex();
    [GeneratedRegex(@"\d+")]
    private static partial Regex DigitRegex();
    [GeneratedRegex(@"(mul\(\d{1,3},\d{1,3}\)|do\(\)|don't\(\))")]
    private static partial Regex Part2Regex();
}