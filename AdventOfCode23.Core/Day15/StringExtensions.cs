namespace AdventOfCode23.Core.Day15;

public static class StringExtensions
{
    public static int ApplyHASH(this string s)
    {
        int hash = 0;

        foreach(int c in s.ToCharArray().Select(c => (int)c))
        {
            hash += c;
            hash *= 17;
            hash %= 256;
        }

        return hash;
    }
}
