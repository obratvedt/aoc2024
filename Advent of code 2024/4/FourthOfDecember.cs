using Advent_of_code_2024.IO;

namespace Advent_of_code_2024._4;

public class FourthOfDecember
{
    public static void Part1()
    {
        var readFile = FileReader.ReadFile("4/input.txt");
        var charList = readFile.Select(l => l.ToCharArray().ToList()).ToList();
        var mainDiagonals = GetDiagonals(charList, isAntiDiagonal: false);
        var antiDiagonals = GetDiagonals(charList, isAntiDiagonal: true);
        var verticals = GetVerticals(charList);
        var all = mainDiagonals.Concat(antiDiagonals).Concat(charList).Concat(verticals);
        
        var ngrams = all.Select(a => CreateNgrams(string.Join("",a)));
        
        var count = ngrams.SelectMany(n => n).Count(n => n is "XMAS" or "SAMX");
        Console.WriteLine(count);
        
    }

    public static void Part2()
    {
        var readFile = FileReader.ReadFile("4/input.txt");
        var charList = readFile.Select(l => l.ToCharArray().ToList()).ToList();
        var all3X3Squares = GetAll3X3Squares(charList);
        var count = 0;
        foreach (var square in all3X3Squares)
        {
            var mainDiagonals = GetDiagonals(square, isAntiDiagonal: false);
            var antiDiagonals = GetDiagonals(square, isAntiDiagonal: true);
            var diagonal = mainDiagonals.First(d => d.Count == 3);
            var antiDiagonal = antiDiagonals.First(d => d.Count == 3);
            var diagonalString = string.Join("",diagonal);
            var antidiagonalString = string.Join("",antiDiagonal);
            if (diagonalString is not ("MAS" or "SAM") || antidiagonalString is not ("MAS" or "SAM")) continue;
            count++;
        }
        Console.WriteLine(count);
    }
   
    private static List<List<List<char>>> GetAll3X3Squares(List<List<char>> matrix)
    {
        var n = matrix.Count; // Assuming a square matrix
        var squares = new List<List<List<char>>>();

        for (var row = 0; row <= n - 3; row++)
        {
            for (var col = 0; col <= n - 3; col++)
            {
                var square = new List<List<char>>();
                for (var i = 0; i < 3; i++)
                {
                    var rowList = new List<char>();
                    for (var j = 0; j < 3; j++)
                    {
                        rowList.Add(matrix[row + i][col + j]);
                    }
                    square.Add(rowList);
                }
                squares.Add(square);
            }
        }
        return squares;
    }
    private static IEnumerable<List<char>> GetDiagonals(List<List<char>> matrix, bool isAntiDiagonal)
    {
        var n = matrix.Count; 
        var m = matrix[0].Count; 

        return Enumerable.Range(0, n + m - 1)
            .Select(start =>
                Enumerable.Range(0, n)
                    .Where(i =>
                    {
                        var j = isAntiDiagonal ? start - (n - 1 - i) : start - i;
                        return j >= 0 && j < m;
                    })
                    .Select(i =>
                    {
                        var j = isAntiDiagonal ? start - (n - 1 - i) : start - i;
                        return matrix[i][j];
                    })
                    .ToList()
            )
            .Where(diagonal => diagonal.Count != 0);
    }
    private static IEnumerable<List<char>> GetVerticals(List<List<char>> matrix)
    {
        var numColumns = matrix[0].Count;

        return Enumerable.Range(0, numColumns)
            .Select(col => matrix.Select(row => row[col]).ToList());
    }
    private static List<string> CreateNgrams(string characters, int grams = 4)
    {
        if (characters.Length < grams)
        {
            return [];
        }
        var ngrams = Enumerable.Range(0, characters.Length - grams + 1)
            .Select(i => characters.Substring(i, grams));
        return ngrams.ToList();
    }
}