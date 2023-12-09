namespace AdventOfCode23.Core.Common;

public static class Primes
{
    private static HashSet<int> _sieve { get; set; }
    private static int _currentMax {  get; set; }

    public static HashSet<int> Sieve(int maxValue)
    {
        if (maxValue < 2) return new HashSet<int>();
        if (_sieve is not null && _currentMax >= maxValue)
        {
            return _sieve.Where(x => x <= int.MaxValue).ToHashSet();
        }

        bool[] seive = Enumerable.Repeat(true, maxValue+1).ToArray();
        int sqrt = (int)Math.Ceiling(Math.Sqrt(maxValue+1));

        for(int i = 2; i < sqrt; i++)
        {
            if (seive[i] == true)
            {
                for(int j = i*i; j <= maxValue; j+=i)
                {
                    seive[j] = false;
                }
            }
        }

        var result = seive
            .Select((b, i) => new { b = b, i = i })
            .Where(t => t.b == true)
            .Select(t => t.i)
            .Skip(2)
            .ToHashSet();

        _sieve = result;
        _currentMax = maxValue;

        return result;
    }

    public static List<int> PrimeFactors(int x)
    {
        List<int> result = new();
        HashSet<int> primes = Primes.Sieve(x);

        if (primes.Contains(x))
        {
            result.Add(x);
            return result;
        }

        foreach(int p in primes)
        {
            if (x % p == 0)
            {
                result.Add(p);
                if (primes.Contains(x / p))
                {
                    result.Add(x / p);
                    return result;
                }
                else
                {
                    return result
                        .Concat(Primes.PrimeFactors(x / p))
                        .ToList();
                }
            }  
        }

        return result;
    }
}
