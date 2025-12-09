using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This component shows which tile the player is standing on. It is used for debugging.
 */
public class TileLogger : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private Tilemap tilemap = null;

    [Header("Output")]
    [SerializeField] private Vector3Int cellPosition;
    [SerializeField] private TileBase tile = null;
    [SerializeField] private string tileName = null;

    private void Update()
    {
        if (tilemap == null)
        {
            return;
        }

        cellPosition = tilemap.WorldToCell(transform.position);
        tile = tilemap.GetTile(cellPosition);
        tileName = tile != null ? tile.name : "NULL";
    }
}
