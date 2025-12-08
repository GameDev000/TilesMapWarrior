using System;
using IntPair = System.ValueTuple<int, int>;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Start cave reachable test");

        float randomFillPercent = 0.5f;
        int gridSize = 100;

        var caveGenerator = new CaveGenerator(randomFillPercent, gridSize);
        caveGenerator.RandomizeMap();

        int simulationSteps = 5;
        for (int i = 0; i < simulationSteps; i++)
        {
            caveGenerator.SmoothMap();
        }

        var finder = new CaveStartPositionFinder(caveGenerator, gridSize);

        int minReachable = 100;
        IntPair start = finder.FindGoodStartPosition(minReachable);

        Console.WriteLine($"Chosen start position: {start}");
        Console.WriteLine("End cave reachable test");
    }
}
