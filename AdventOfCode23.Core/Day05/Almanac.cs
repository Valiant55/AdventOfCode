namespace AdventOfCode23.Core.Day05;

public class Almanac
{
    public List<Seed> Seeds { get; set; } = new List<Seed>();
    public Map SeedToSoilMap { get; set; }
    public Map SoilToFertilizerMap { get; set; }
    public Map FertilzerToWaterMap { get; set; }
    public Map WaterToLightMap { get; set; }
    public Map LightToTemperature { get; set; }
    public Map TemperatureToHumidityMap { get; set; }
    public Map HumidityToLocationMap { get; set; }

    public List<Range> SeedRanges => Seeds
        .Chunk(2)
        .Select(c => new Range { Start = c[0].Id, Length = c[1].Id })
        .ToList();

    public List<Map> Maps => new List<Map>()
    {
        SeedToSoilMap,
        SoilToFertilizerMap,
        FertilzerToWaterMap,
        WaterToLightMap,
        LightToTemperature,
        TemperatureToHumidityMap,
        HumidityToLocationMap
    };

    public List<long> GetLocations()
    {
        List<long> locations = new();

        foreach(var s in Seeds)
        {
            var soil = SeedToSoilMap.GetMappedValue(s.Id);
            var fertilizer = SoilToFertilizerMap.GetMappedValue(soil);
            var water = FertilzerToWaterMap.GetMappedValue(fertilizer);
            var light = WaterToLightMap.GetMappedValue(water);
            var temperature = LightToTemperature.GetMappedValue(light);
            var humidity = TemperatureToHumidityMap.GetMappedValue(temperature);
            var location = HumidityToLocationMap.GetMappedValue(humidity);

            locations.Add(location);
        }

        return locations;
    }

    public List<long> GetLocationsFromRanges()
    {
        List<Range> currentRanges = SeedRanges;

        foreach (var map in Maps)
        {
            List<Range> nextRanges = new();
            foreach (var range in currentRanges)
            {
                nextRanges = nextRanges
                    .Concat(map.GetMappedRanges(range))
                    .ToList();
            }
            currentRanges = nextRanges;
        }

        return currentRanges.Select(r => r.Start).ToList();
    }
}
