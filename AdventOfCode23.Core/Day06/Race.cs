namespace AdventOfCode23.Core.Day06;

public class Race
{
    public long Time { get; set; }
    public long Distance { get; set; }

    public long PossibleWins()
    {
        long totalWins = 0;

        for(long holdTime = 1; holdTime < Time; holdTime++)
        {
            long remainingTime = Time - holdTime;
            long distanceTraveled = holdTime * remainingTime;

            if(distanceTraveled > Distance) totalWins++;
        }

        return totalWins;
    }
}
