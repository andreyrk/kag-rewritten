using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    public GameObject spawnPrefab;

    [HideInInspector]
    public List<GameObject> spawnPoints;

    public Texture2D mapTexture;
    public Tile[] tileIndex;

    public Tilemap tilemap;
    public TilemapRenderer tilemapRenderer;
    public TilemapCollider2D tilemapCollider;

    public UnityEvent loadingFinished;

    void Start()
    {
        int i = 0;
        foreach (Color32 color in mapTexture.GetPixels32())
        {
            int x = i % mapTexture.width;
            int y = i / mapTexture.width;

            foreach (Tile tile in tileIndex)
            {
                if (tile.tileColor.Equals(color))
                {
                    if (tile.isSpawn)
                    {
                        Vector3 spawnPos = tilemap.CellToWorld(new Vector3Int(x, y, 0));
                        spawnPos.y = spawnPos.y + spawnPrefab.transform.position.y;

                        GameObject spawnPoint = Instantiate(spawnPrefab, transform);
                        spawnPoint.transform.position = spawnPos;

                        spawnPoints.Add(spawnPoint);
                    }

                    Vector3Int pos = new Vector3Int(x, y, 0);
                    tilemap.SetTile(pos, tile);
                }
            }

            i++;
        }

        loadingFinished.Invoke();
    }
}
