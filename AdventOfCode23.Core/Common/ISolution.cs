using System.Diagnostics;

namespace AdventOfCode23.Core.Common;

public abstract class ISolution
{
    public abstract long Part01();
    public abstract long Part02();

    public override string ToString()
    {
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();
        var part01 = Part01();
        stopWatch.Stop();
        var part01Time = stopWatch.Elapsed;
        stopWatch.Restart();
        stopWatch.Start();
        var part02 = Part02();
        stopWatch.Stop();
        var part02Time = stopWatch.Elapsed;

        var day = this.GetType().Namespace?.Split(".").Last();

        return $"{day} Solution\nThe answer to part 1 is {part01} ({part01Time.Milliseconds}ms)\nThe answer to part 2 is {part02} ({part02Time.Milliseconds}ms)";
    }
}
