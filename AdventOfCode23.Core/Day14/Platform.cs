using System.ComponentModel.DataAnnotations;

namespace AdventOfCode23.Core.Day14;

public class Platform
{
    public char[][] Map {  get; set; }

    public long TiltAndCalculateLoad()
    {
        char[][] map = new char[Map.Length][];
        Array.Copy(Map, map, Map.Length);
        Tilt(map, MoveNorth);
        return CalculateLoad(map);
    }

    public long TiltAndCalculateLoad(int x = 1_000_000_000)
    {
        char[][] map = new char[Map.Length][];
        Array.Copy(Map, map, Map.Length);
        return CalculateLoad(map);
    }

    public string GetNextMap(char[][] map)
    {


        var strMap = string.Join("", map.Select(x => new string(x)));
        return strMap;
    }

    public void Tilt(char[][] map, Action<char[][], int, int> action)
    {
        for(int x = 0; x < Map[0].Length; x++)
        {
            for(int y = 0; y < Map.Length; y++)
            {
                if (Map[y][x] == 'O')
                {
                    action(map, x, y);
                }
            }
        }
    }

    private void MoveNorth(char[][] map, int x, int y)
    {
        int newY = y;
        for(int i = y-1; i >= 0; i--)
        {
            if (map[i][x] == '.') newY = i;
            else break;
        }

        if (newY == y) return;
        map[newY][x] = 'O';
        map[y][x] = '.';
    }

    private void MoveWest(char[][] map, int x, int y)
    {
        int newY = y;
        for (int i = y - 1; i >= 0; i--)
        {
            if (map[i][x] == '.') newY = i;
            else break;
        }

        if (newY == y) return;
        map[newY][x] = 'O';
        map[y][x] = '.';
    }

    private void MoveSouth(char[][] map, int x, int y)
    {
        int newY = y;
        for (int i = y - 1; i >= 0; i--)
        {
            if (map[i][x] == '.') newY = i;
            else break;
        }

        if (newY == y) return;
        map[newY][x] = 'O';
        map[y][x] = '.';
    }

    private void MoveEast(char[][] map, int x, int y)
    {
        int newY = y;
        for (int i = y - 1; i >= 0; i--)
        {
            if (map[i][x] == '.') newY = i;
            else break;
        }

        if (newY == y) return;
        map[newY][x] = 'O';
        map[y][x] = '.';
    }

    public long CalculateLoad(char[][] map)
    {
        long load = 0;
        
        foreach((var row, var index) in map.Select((r, i) => (r, i)))
        {
            long points = map.Length - index;
            var rockCount = row.Where(c => c == 'O').Count();
            load += rockCount * points;
        }

        return load;
    }
}
