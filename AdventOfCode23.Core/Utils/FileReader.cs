namespace AdventOfCode23.Core.Utils;

public static class FileReader
{
    public static IEnumerable<string> Read(string fileName)
    {
        using var reader = new StreamReader(fileName);
        string? line = reader.ReadLine();

        while(line != null)
        {
            yield return line;
            line = reader.ReadLine();
        }
    }
}
