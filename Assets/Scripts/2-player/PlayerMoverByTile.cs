using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMoverByTile : KeyboardMover
{
    [Header("Tilemap")]
    [SerializeField] Tilemap tilemap = null;

    [Header("Allowed tiles per mode")]
    [SerializeField] AllowedTiles footAllowedTiles = null;
    [SerializeField] AllowedTiles boatAllowedTiles = null;
    [SerializeField] AllowedTiles horseAllowedTiles = null;
    [SerializeField] AllowedTiles axeAllowedTiles = null;

    [Header("Mode")]
    [SerializeField] PlayerMode mode = PlayerMode.OnFoot;

    [Header("Visuals")]
    [SerializeField] SpriteRenderer spriteRenderer = null;
    [SerializeField] Sprite footSprite = null;
    [SerializeField] Sprite boatSprite = null;
    [SerializeField] Sprite horseSprite = null;
    [SerializeField] Sprite axeSprite = null;

    [Header("Axe destroy settings")]
    [SerializeField] AllowedTiles axeBreakableTiles = null;
    [SerializeField] TileBase grassTile = null;



    private void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        UpdateVisual();
    }

    private TileBase TileOnPosition(Vector3 worldPosition)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        return tilemap.GetTile(cellPosition);
    }

    private AllowedTiles CurrentAllowedTiles
    {
        get
        {
            switch (mode)
            {
                case PlayerMode.OnBoat:
                    return boatAllowedTiles;
                case PlayerMode.OnHorse:
                    return horseAllowedTiles;
                case PlayerMode.OnAxe:
                    return axeAllowedTiles;
                case PlayerMode.OnFoot:
                default:
                    return footAllowedTiles;
            }
        }
    }

    public void SetMode(PlayerMode newMode)
    {
        if (mode == newMode)
            return;

        mode = newMode;
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        if (spriteRenderer == null)
            return;

        switch (mode)
        {
            case PlayerMode.OnFoot:
                if (footSprite != null)
                    spriteRenderer.sprite = footSprite;
                break;

            case PlayerMode.OnBoat:
                if (boatSprite != null)
                    spriteRenderer.sprite = boatSprite;
                break;

            case PlayerMode.OnHorse:
                if (horseSprite != null)
                    spriteRenderer.sprite = horseSprite;
                break;
            case PlayerMode.OnAxe:
                if (axeSprite != null)
                    spriteRenderer.sprite = axeSprite;
                break;
        }
    }

    void Update()
    {
        Vector3 newPosition = NewPosition();

        if (newPosition == transform.position)
            return;

        TileBase tileOnNewPosition = TileOnPosition(newPosition);

        if (tileOnNewPosition != null && CurrentAllowedTiles != null && CurrentAllowedTiles.Contains(tileOnNewPosition))
        {
            transform.position = newPosition;
            if (mode == PlayerMode.OnAxe && axeBreakableTiles != null && grassTile != null)
            {
                if (axeBreakableTiles.Contains(tileOnNewPosition))
                {
                    Vector3Int cellPosition = tilemap.WorldToCell(newPosition);
                    tilemap.SetTile(cellPosition, grassTile);
                }
            }
        }
        else
        {
            Debug.LogWarning("You cannot move there! Tile: " + tileOnNewPosition + " in mode " + mode);
        }
    }
}
