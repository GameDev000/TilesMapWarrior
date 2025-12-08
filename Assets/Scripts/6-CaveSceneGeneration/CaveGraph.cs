using System.Collections.Generic;
using IntPair = System.ValueTuple<int, int>;

public class CaveGraph : IGraph<IntPair>
{
    private readonly int[,] map;
    private readonly int width;
    private readonly int height;

    public CaveGraph(int[,] map)
    {
        this.map = map;
        this.width = map.GetLength(0);
        this.height = map.GetLength(1);
    }

    public IEnumerable<IntPair> Neighbors(IntPair node)
    {
        int x = node.Item1;
        int y = node.Item2;

        if (IsFloor(x + 1, y)) yield return (x + 1, y);
        if (IsFloor(x - 1, y)) yield return (x - 1, y);
        if (IsFloor(x, y + 1)) yield return (x, y + 1);
        if (IsFloor(x, y - 1)) yield return (x, y - 1);
    }

    private bool IsFloor(int x, int y)
    {
        if (x < 0 || x >= width || y < 0 || y >= height)
            return false;

        return map[x, y] == 0;
    }
}
