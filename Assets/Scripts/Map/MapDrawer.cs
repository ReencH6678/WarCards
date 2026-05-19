using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapDrawer : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemap;

    public Tilemap Tilemap => _tilemap;

    public void DrawMap(Dictionary<Vector3Int, float> influence, float emptyCellInfluence, TileBase playerTile, TileBase enemyTile)
    {
        for (int x = _tilemap.cellBounds.min.x; x < _tilemap.cellBounds.max.x; x++)
        {
            for (int y = _tilemap.cellBounds.min.y; y < _tilemap.cellBounds.max.y; y++)
            {
                Vector3Int position = new Vector3Int(x, y);

                if (influence.ContainsKey(position))
                {
                    if (influence[position] == 0)
                        continue;

                    TileBase tile = influence[position] > 0 ? enemyTile : playerTile;
                    _tilemap.SetTile(new Vector3Int(x, y), tile);
                }
            }
        }
    }
}
