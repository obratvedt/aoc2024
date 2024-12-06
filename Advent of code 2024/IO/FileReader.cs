namespace Advent_of_code_2024.IO;

public static class FileReader
{
    public static List<string> ReadFile(string filePath)
    {
        var lines = new List<string>();

        using var reader = new StreamReader(filePath);
        while (reader.ReadLine() is { } line)
        {
            lines.Add(line);
        }
        return lines;
    }
   
}