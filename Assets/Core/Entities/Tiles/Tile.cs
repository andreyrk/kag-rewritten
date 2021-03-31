using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Tile", menuName = "Tile", order = 1)]
public class Tile : UnityEngine.Tilemaps.Tile
{
    public string tileName;
    public Color32 tileColor;

    public bool isSpawn;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position, tilemap, ref tileData);

        tileData.sprite = sprite;
    }
}
