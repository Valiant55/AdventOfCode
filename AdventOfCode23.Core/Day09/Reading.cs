using System.Transactions;

namespace AdventOfCode23.Core.Day09;

public class Reading
{
    public List<long> Readings { get; set; } = new List<long>();

    public List<long> PredicatedReadings()
    {
        Stack<List<long>> depthReadings = new Stack<List<long>>();
        depthReadings.Push(Readings);
        depthReadings.Push(NextDepth(Readings));
        List<long> predictions = new List<long>();

        while (!depthReadings.First().All(r => r == 0))
        {
            depthReadings.Push(NextDepth(depthReadings.First()));
        }

        long lastReading = 0;
        foreach(var depth in depthReadings)
        {
            if(depth.All(r => r == 0) )
            {
                lastReading = 0;
                predictions.Add(0);
            }
            else
            {
                long nextPrediction = depth.Last() + lastReading;
                predictions.Add(nextPrediction);
                lastReading = nextPrediction;
            }
        }

        return predictions;
    }

    private List<long> NextDepth(List<long> currentReadings)
    {
        var result = new List<long>();

        foreach((var current, var next) in currentReadings.Zip(currentReadings.Skip(1)))
        {
            result.Add(next - current);
        }

        return result.Count == 0 ? [0] : result;
    }
}
