using System;
using IntPair = System.ValueTuple<int, int>;

public class CaveStartPositionFinder
{
    private readonly CaveGenerator caveGenerator;
    private readonly int gridSize;
    private readonly Random random;

    public CaveStartPositionFinder(CaveGenerator caveGenerator, int gridSize)
    {
        this.caveGenerator = caveGenerator;
        this.gridSize = gridSize;
        this.random = new Random();
    }

    public IntPair FindGoodStartPosition(int minReachableTiles)
    {
        int[,] map = caveGenerator.GetMap();
        var graph = new CaveGraph(map);

        while (true)
        {
            int x = random.Next(0, gridSize);
            int y = random.Next(0, gridSize);

            if (map[x, y] == 1)
                continue;

            IntPair start = (x, y);

            int reachable = CaveReachability.CountReachable(
                start,
                graph,
                maxIterations: 100000,
                earlyStopIfAtLeast: minReachableTiles
            );

            if (reachable >= minReachableTiles)
            {
                return start;
            }

        }
    }
}
