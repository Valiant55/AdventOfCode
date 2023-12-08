namespace AdventOfCode23.Core.Day05;

public class Map
{
    public List<long> SourceStart { get; set; } = new List<long>();
    public List<long> DestinationStart { get; set; } = new List<long>();
    public List<long> Length { get; set; } = new List<long>();

    public long GetMappedValue(long source)
    {
        foreach(var (sourceStart, destinationStart, length) in SourceStart.Zip(DestinationStart, Length))
        {
            var sourceEnd = sourceStart + length - 1;
            if (source >= sourceStart && source <= sourceEnd)
            {
                return destinationStart + (source - sourceStart);
            }
        }

        return source;
    }
     
    public List<Range> GetMappedRanges(Range range)
    {
        var ranges = new List<Range>();

        foreach (var (sourceStart, destinationStart, length) in SourceStart.Zip(DestinationStart, Length))
        {
            var sourceEnd = sourceStart + length - 1;

            if(range.Start >= sourceStart && range.End <= sourceEnd)
            {
                var diff = destinationStart - sourceStart;
                ranges.Add(new Range() { Start = range.Start + diff, Length = range.Length });
                return ranges;
            }
            else if (range.Start < sourceStart && range.End > sourceEnd)
            {
                var diff = destinationStart - sourceStart;
                var bottom = new Range() { Start = range.Start, Length = sourceStart - range.Start };
                var middle = new Range() { Start = sourceStart + diff, Length = length };
                var top = new Range() { Start = sourceEnd + 1, Length = range.Length - length - (sourceStart - range.Start) };

                ranges.Add(middle);
                return ranges
                    .Concat(GetMappedRanges(bottom))
                    .Concat(GetMappedRanges(top))
                    .ToList();
            }
            else if(range.Start <= sourceEnd && range.End > sourceEnd)
            {
                var diff = destinationStart - sourceStart;
                var rangeLength = sourceEnd - range.Start + 1;
                var newRange = new Range() { Start = range.Start + diff, Length = rangeLength };
                ranges.Add(newRange);
                
                var remainderRange = new Range() { Start = range.Start + newRange.Length, Length = range.Length - rangeLength };
                return ranges.Concat(GetMappedRanges(remainderRange)).ToList();
            }
            else if(range.Start < sourceStart && range.End >= sourceStart)
            {
                var rangeLength = sourceStart - range.Start;
                var newRange = new Range() { Start = range.Start, Length = rangeLength };
                ranges.Add(newRange);

                var remainderRange = new Range() { Start = range.Start + rangeLength, Length = range.Length - rangeLength};
                return ranges.Concat(GetMappedRanges(remainderRange)).ToList();
            }
            
        }

        ranges.Add(range);
        return ranges;
    }

}
