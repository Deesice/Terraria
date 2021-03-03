using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGenerator : MonoBehaviour
{
    static WorldGenerator instance;
    Tilemap tilemap;
    public TileBase tile;
    public GameObject playerPrefab;

    public float scale;
    [Range (0, 1)]
    public float treshold;
    public float groundLevel;
    public float groundSpread;

    GameObject player;
    void Start()
    {
        instance = this;
        ReloadLevel();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            ReloadLevel();
    }
    void ReloadLevel()
    {
        GenerateLevel();

        if (player == null)
        {
            player = Instantiate(playerPrefab, new Vector2(0, 0), Quaternion.identity);
            Camera.main.GetComponent<Follow>().target = player.transform;
        }
        else
            player.transform.position = Vector3.zero;
    }

    public void GenerateLevel()
    {
        if (tilemap == null)
            tilemap = GetComponentInChildren<Tilemap>();

        Vector2 offset = new Vector2(Random.Range(-100000, 100000), Random.Range(-100000, 100000));

        tilemap.ClearAllTiles();
        for (int i = -Constants.worldSize.x / 2; i <= Constants.worldSize.x / 2; i++)
            for (int j = -Constants.worldSize.y / 2; j <= Constants.worldSize.y / 2; j++)
                if (Examine(i, j, offset))
                    tilemap.SetTile(new Vector3Int(i, j, 0), tile);
    }
    bool Examine(int xPos, int yPos, Vector2 offset)
    {
        var caveLayer = Mathf.PerlinNoise(xPos * scale + offset.x, yPos * scale + offset.y);

        var groundLayer = Mathf.PerlinNoise(xPos * scale + offset.y, offset.x);

        groundLayer = Mathf.Lerp(groundLevel - groundSpread, groundLevel + groundSpread, groundLayer);
        return caveLayer <= treshold && yPos <= groundLayer;
    }
    public static void DeleteTile(Vector3 worldPos)
    {
        instance.tilemap.SetTile(instance.tilemap.WorldToCell(worldPos), null);
    }
    public static void SetTile(Vector3 worldPos)
    {
        instance.tilemap.SetTile(instance.tilemap.WorldToCell(worldPos), instance.tile);
    }
}
