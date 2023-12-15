using System.ComponentModel.DataAnnotations;
using System.Text;

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

        for(int x = 0; x < 1000; x++)
        {
            Cycle(map);
        }

        return CalculateLoad(map);
    }

    public void Cycle(char[][] map)
    {
        MoveNorth(map);
        MoveWest(map);
        MoveSouth(map);
        MoveEast(map);
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

                    if(newY != y)
                    {
                        map[newY][x] = 'O';
                        map[y][x] = '.';
                    } 
                }
            }
        } 
    }

    private void MoveWest(char[][] map)
    {
        for (int y = 0; y < map.Length; y++)
        {
            for (int x = 0; x < map[y].Length; x++)
            {
                if (map[y][x] == 'O')
                {
                    int newX = x;
                    for (int i = x - 1; i >= 0; i--)
                    {
                        if (map[y][i] == '.') newX = i;
                        else break;
                    }

                    if (newX != x)
                    {
                        map[y][newX] = 'O';
                        map[y][x] = '.';
                    }
                }
            }
        } 
    }

    private void MoveSouth(char[][] map)
    {
        for (int y = map.Length - 1; y >= 0; y--)
        {
            for (int x = 0; x < map[y].Length; x++)
            {
                if (Map[y][x] == 'O')
                {
                    int newY = y;
                    for (int i = y + 1; i < map.Length; i++)
                    {
                        if (map[i][x] == '.') newY = i;
                        else break;
                    }

                    if (newY != y)
                    {
                        map[newY][x] = 'O';
                        map[y][x] = '.';
                    }
                }
            }
        }  
    }

    private void MoveEast(char[][] map)
    {
        for (int y = 0; y < Map.Length; y++)
        {
            for (int x = Map[0].Length - 1; x >= 0; x--)
            {
                if (Map[y][x] == 'O')
                {
                    int newX = x;
                    for (int i = x + 1; i < map[y].Length; i++)
                    {
                        if (map[y][i] == '.') newX = i;
                        else break;
                    }

                    if (newX != x)
                    {
                        map[y][newX] = 'O';
                        map[y][x] = '.';
                    }
                }
            }
        }
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

    public static string Print<T>(T[][] array)
    {
        var sb = new StringBuilder();

        for (int row = 0; row < array.Length; row++)
        {
            for (int column = 0; column < array[row].Length; column++)
            {
                sb.Append(array[row][column].ToString());
            }

            sb.AppendLine();
        }

        return sb.ToString();
    }
}
