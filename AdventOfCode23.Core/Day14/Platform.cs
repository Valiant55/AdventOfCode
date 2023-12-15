using System.ComponentModel.DataAnnotations;

namespace AdventOfCode23.Core.Day14;

public class Platform
{
    public char[][] Map {  get; set; }

    public long TiltAndCalculateLoad()
    {
        char[][] map = new char[Map.Length][];
        Array.Copy(Map, map, Map.Length);
        MoveNorth(map);
        return CalculateLoad(map);
    }

    public long CycleAndCalculateLoad(int totalCycles = 1_000_000_000)
    {
        char[][] map = new char[Map.Length][];
        Array.Copy(Map, map, Map.Length);

        long lastLoad = 0;
        long currentLoad = 1;
        int x = 0;

        while(lastLoad != currentLoad)
        {
            lastLoad = CalculateLoad(map);
            Cycle(map);
            currentLoad = CalculateLoad(map);
            x++;
            Console.WriteLine($"Iteration: {x}");
        }

        return currentLoad;
    }

    public void Cycle(char[][] map)
    {
        //Tilt(map, MoveNorth);
        Tilt(map, MoveWest);
        Tilt(map, MoveSouth);
        Tilt(map, MoveEast);
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

    private void MoveNorth(char[][] map)
    {
        for (int x = 0; x < map[0].Length; x++)
        {
            for (int y = 0; y < map.Length; y++)
            {
                if (map[y][x] == 'O')
                {
                    int newY = y;
                    for (int i = y - 1; i >= 0; i--)
                    {
                        if (map[i][x] == '.') newY = i;
                        else break;
                    }

                    map[newY][x] = 'O';
                    map[y][x] = '.';
                }
            }
        } 
    }

    private void MoveWest(char[][] map, int x, int y)
    {
        int newX = x;
        for (int i = x - 1; i >= 0; i--)
        {
            if (map[y][i] == '.') newX = i;
            else break;
        }

        if (newX == x) return;
        map[y][newX] = 'O';
        map[y][x] = '.';
    }

    private void MoveSouth(char[][] map, int x, int y)
    {
        int newY = y;
        for (int i = y + 1; i < map.Length; i++)
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
        int newX = x;
        for (int i = x + 1; i < map[0].Length; i++)
        {
            if (map[y][i] == '.') newX = i;
            else break;
        }

        if (newX == x) return;
        map[y][newX] = 'O';
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
