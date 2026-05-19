using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapInfluencer : MonoBehaviour
{
    [SerializeField] private Team _playerTeam;
    [SerializeField] private Team _enemyTeam;

    [SerializeField] private UnitsHandler _unitsHandler;

    private Dictionary<Vector3Int, float> _influence = new Dictionary<Vector3Int, float>();

    private float _maxInfluence = 10;
    private float _emptyCellInfluence = 20;

    public float EmptyCellInfluence => _emptyCellInfluence;

    public void SetStartInfluence(Tilemap tilemap, TileBase playerTile, TileBase enemyTile)
    {
        BoundsInt tilemapBounds = tilemap.cellBounds;

        for (int x = tilemapBounds.min.x; x < tilemapBounds.max.x; x++)
        {
            for (int y = tilemapBounds.min.y; y < tilemapBounds.max.y; y++)
            {
                Vector3Int position = new Vector3Int(x, y);

                if (tilemap.GetTile(position) == playerTile)
                    _influence.Add(position, -_maxInfluence);
                else if (tilemap.GetTile(position) == enemyTile)
                    _influence.Add(position, _maxInfluence);
                else if (tilemap.HasTile(position) == false)
                    continue;
                else
                    _influence.Add(position, 0);
            }
        }
    }

    public void UpdateInfluence(Tilemap tilemap)
    {
        foreach (Unit unit in _unitsHandler.GetUnits())
            AddInfluence(unit.transform.position, unit.Team, tilemap);
    }

    public void AddInfluence(Vector3 worldPos, Team team, Tilemap tilemap)
    {
        Vector3Int cell = tilemap.WorldToCell(worldPos);

        float sign = 0;
        int radius = 2;

        if (team == _playerTeam)
            sign = -1f;
        else if (team == _enemyTeam)
            sign = 1f;

        for (int x = -radius; x <= radius; x++)
        {
            for (int y = -radius; y <= radius; y++)
            {
                int tileX = cell.x + x;
                int tileY = cell.y + y;

                Vector3Int position = new Vector3Int(tileX, tileY);

                if (_influence.ContainsKey(position))
                {
                    float distance = Mathf.Sqrt(x * x + y * y);
                    float power = Mathf.Clamp01(1f - distance / radius);

                    _influence[position] = Mathf.Clamp(_influence[position] + sign * power, -_maxInfluence, _maxInfluence);
                }
            }
        }
    }

    public Dictionary<Vector3Int, float> GetInfluence()
    {
        Dictionary<Vector3Int, float> influences = new Dictionary<Vector3Int, float>();

        foreach (var influence in _influence)
            influences.Add(influence.Key, influence.Value);

        return influences;
    }
}
