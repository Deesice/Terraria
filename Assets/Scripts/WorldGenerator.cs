using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGenerator : MonoBehaviour
{
    Tilemap tilemap;
    public TileBase tile;
    public GameObject playerPrefab;

    public float scale;
    [Range (0, 1)]
    public float treshold;
    void Start()
    {
        GenerateLevel();

        Camera.main.GetComponent<Follow>().target = Instantiate(playerPrefab, new Vector2(0, 0), Quaternion.identity).transform;
    }

    public void GenerateLevel()
    {
        if (tilemap == null)
            tilemap = GetComponentInChildren<Tilemap>();

        Vector2 offset = new Vector2(Random.Range(-100000, 100000), Random.Range(-100000, 100000));

        tilemap.ClearAllTiles();
        for (int i = -Constants.worldSize.x / 2; i <= Constants.worldSize.x / 2; i++)
            for (int j = -Constants.worldSize.y / 2; j <= Constants.worldSize.y / 2; j++)
                if (Mathf.PerlinNoise(i * scale + offset.x, j * scale + offset.y) < treshold)
                    tilemap.SetTile(new Vector3Int(i, j, 0), tile);
    }
}
