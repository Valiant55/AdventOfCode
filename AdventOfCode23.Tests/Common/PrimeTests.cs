using AdventOfCode23.Core.Common;
using FluentAssertions;

namespace AdventOfCode23.Tests.Common;

public class PrimeTests
{
    [Fact]
    public void Seive_ReturnsCorrectPrimes_ThroughOneHundred()
    {
        HashSet<int> primes = Primes.Sieve(100);

        int[] expectedResult = new int[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97 };

        primes.Should().BeEquivalentTo(expectedResult);
    }

    [Theory]
    [InlineData(10,    new int[] { 2, 5 })]
    [InlineData(20303, new int[] { 79, 257 })]
    [InlineData(1999,  new int[] { 1999 })]
    [InlineData(1998,  new int[] { 2, 3, 3, 3, 37 })]
    public void PrimeFactors_ReturnsCorrectPrimes(int num, int[] factors)
    {
        List<int> result = Primes.PrimeFactors(num);

        result.Should().BeEquivalentTo(factors);
    }
}
