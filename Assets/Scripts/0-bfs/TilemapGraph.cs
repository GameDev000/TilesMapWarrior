using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * A graph that represents a tilemap, using only the allowed tiles.
 */
public class TilemapGraph : IGraph<Vector3Int>
{
    private readonly Tilemap _tilemap;
    private readonly TileBase[] _allowedTiles;

    private static readonly Vector3Int[] Directions =
    {
        new Vector3Int(-1, 0, 0),
        new Vector3Int(1, 0, 0),
        new Vector3Int(0, -1, 0),
        new Vector3Int(0, 1, 0),
    };

    public TilemapGraph(Tilemap tilemap, TileBase[] allowedTiles)
    {
        _tilemap = tilemap;
        _allowedTiles = allowedTiles;
    }

    public IEnumerable<Vector3Int> Neighbors(Vector3Int node)
    {
        foreach (var direction in Directions)
        {
            Vector3Int neighborPos = node + direction;
            TileBase neighborTile = _tilemap.GetTile(neighborPos);

            if (_allowedTiles.Contains(neighborTile))
            {
                yield return neighborPos;
            }
        }
    }
}
