using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(MapDrawer), typeof(MapInfluencer))]
public class Map : MonoBehaviour
{
    [SerializeField] private Transform _playerHall;
    [SerializeField] private Transform _enemyHall;

    private TileBase _enemyTile;
    private TileBase _playerTile;

    private MapDrawer _mapDrawer;
    private MapInfluencer _mapInfluencer;

    public Tilemap Tilemap => _mapDrawer.Tilemap;

    private bool _isOn = true;

    private void Awake()
    {
        _mapDrawer = GetComponent<MapDrawer>();
        _mapInfluencer = GetComponent<MapInfluencer>();
    }

    private void Start()
    {
        Vector3Int playerTilePosition = _mapDrawer.Tilemap.WorldToCell(_playerHall.position);
        Vector3Int enemyTilePosition = _mapDrawer.Tilemap.WorldToCell(_enemyHall.position);

        _playerTile = _mapDrawer.Tilemap.GetTile(playerTilePosition);
        _enemyTile = _mapDrawer.Tilemap.GetTile(enemyTilePosition);

        _mapInfluencer.SetStartInfluence(_mapDrawer.Tilemap, _playerTile, _enemyTile);
        StartCoroutine(UpdateMap());
    }

    private IEnumerator UpdateMap()
    {
        var waitForSeconds = new WaitForSeconds(0.2f);

        while (_isOn)
        {
            _mapInfluencer.UpdateInfluence(_mapDrawer.Tilemap);
            _mapDrawer.DrawMap(_mapInfluencer.GetInfluence(), _mapInfluencer.EmptyCellInfluence, _playerTile, _enemyTile);

            yield return waitForSeconds;
        }
    }

    public bool CanSpawn(Vector3 position)
    {
        Vector3Int tilePosition = _mapDrawer.Tilemap.WorldToCell(position);

        TileBase selectTile = _mapDrawer.Tilemap.GetTile(tilePosition);

        if (selectTile == _playerTile)
            return true;

        return false;
    }
}
