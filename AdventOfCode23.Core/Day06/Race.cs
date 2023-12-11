namespace AdventOfCode23.Core.Day06;

public class Race
{
    public long Time { get; set; }
    public long Distance { get; set; }

    public long PossibleWins()
    {
        long firstWin = 0;
        long lastWin = 0;

        for(long holdTime = 1; holdTime < Time; holdTime++)
        {
            long remainingTime = Time - holdTime;
            long distanceTraveled = holdTime * remainingTime;

            if(distanceTraveled > Distance)
            {
                firstWin = holdTime;
                break;
            }
        }

        for (long holdTime = Time; holdTime >= firstWin; holdTime--)
        {
            long remainingTime = Time - holdTime;
            long distanceTraveled = holdTime * remainingTime;

            if (distanceTraveled > Distance)
            {
                lastWin = holdTime;
                break;
            }
        }

        return lastWin - firstWin + 1;
    }
}
