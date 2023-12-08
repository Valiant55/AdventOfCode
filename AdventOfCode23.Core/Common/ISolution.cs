namespace AdventOfCode23.Core.Common;

public abstract class ISolution
{
    public abstract long Part01();
    public abstract long Part02();

    public override string ToString()
    {
        return $"{this.GetType().Namespace}\nThe answer to part 1 is {Part01()}\nThe answer to part 2 is {Part02()}";
    }
}
