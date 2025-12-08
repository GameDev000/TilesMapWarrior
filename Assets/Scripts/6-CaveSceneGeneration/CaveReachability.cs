using System.Collections.Generic;
using IntPair = System.ValueTuple<int, int>;

public static class CaveReachability
{
    public static int CountReachable(
        IntPair start,
        CaveGraph graph,
        int maxIterations = 100000,
        int earlyStopIfAtLeast = int.MaxValue)
    {
        var openQueue = new Queue<IntPair>();
        var visited = new HashSet<IntPair>();

        openQueue.Enqueue(start);
        visited.Add(start);

        int iterations = 0;

        while (openQueue.Count > 0 && iterations < maxIterations)
        {
            var current = openQueue.Dequeue();

            foreach (var neighbor in graph.Neighbors(current))
            {
                if (visited.Contains(neighbor))
                    continue;

                visited.Add(neighbor);
                openQueue.Enqueue(neighbor);

                if (visited.Count >= earlyStopIfAtLeast)
                    return visited.Count;
            }

            iterations++;
        }

        return visited.Count;
    }
}
