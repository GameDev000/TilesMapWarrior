using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using IntPair = System.ValueTuple<int, int>;

public class CaveSceneManager : MonoBehaviour
{
    [Header("Tilemap & Tiles")]
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase wallTile;
    [SerializeField] private TileBase floorTile;

    [Header("Cave generation settings")]
    [Range(0f, 1f)]
    [SerializeField] private float randomFillPercent = 0.5f;

    [SerializeField] private int gridSize = 100;
    [SerializeField] private int simulationSteps = 5;

    [Header("Player spawn settings")]
    [SerializeField] private int minReachableTiles = 100;
    [SerializeField] private GameObject playerPrefab;

    private CaveGenerator caveGenerator;

    private void Start()
    {
        UnityEngine.Random.InitState(12345);

        caveGenerator = new CaveGenerator(randomFillPercent, gridSize);
        caveGenerator.RandomizeMap();

        for (int i = 0; i < simulationSteps; i++)
        {
            caveGenerator.SmoothMap();
        }

        int[,] map = caveGenerator.GetMap();

        DrawMapOnTilemap(map);

        var finder = new CaveStartPositionFinder(caveGenerator, gridSize);
        IntPair startCell = finder.FindGoodStartPosition(minReachableTiles);

        Debug.Log($"Chosen start cell for player: {startCell}");

        SpawnOrMovePlayerToCell(startCell);
    }

    private void DrawMapOnTilemap(int[,] data)
    {
        tilemap.ClearAllTiles();

        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                var cellPos = new Vector3Int(x, y, 0);
                TileBase tile = data[x, y] == 1 ? wallTile : floorTile;
                tilemap.SetTile(cellPos, tile);
            }
        }
    }

    private void SpawnOrMovePlayerToCell(IntPair cell)
    {
        var cellPos = new Vector3Int(cell.Item1, cell.Item2, 0);
        Vector3 worldPos = tilemap.GetCellCenterWorld(cellPos);

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            player.transform.position = worldPos;
        }
        else if (playerPrefab != null)
        {
            player = Instantiate(playerPrefab, worldPos, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No player found and no playerPrefab assigned.");
        }

        if (Camera.main != null)
        {
            Vector3 camPos = Camera.main.transform.position;
            camPos.x = worldPos.x;
            camPos.y = worldPos.y;
            Camera.main.transform.position = camPos;
        }
    }


}
