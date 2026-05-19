using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(EnemyFinder), typeof(Wallet))]
public class Bot : MonoBehaviour
{
    [SerializeField] private float _attackTime;
    [SerializeField] private UnitsHandler _unitsHandler;
    [SerializeField] private Deck _deck;
    [SerializeField] private Map _map;
    [SerializeField] private Tile _botTile;
    [SerializeField]private Unit _enemyCastel;

    private Wallet _wallet;
    private EnemyFinder _enemyFinder;
    private bool _isOn = true;

    private void Awake()
    {
        _enemyFinder = GetComponent<EnemyFinder>();
        _wallet = GetComponent<Wallet>();

        _enemyFinder.SetUnitsHandler(_unitsHandler);
    }

    private void Start()
    {
        StartCoroutine(Play());
    }

    private IEnumerator Play()
    {
        var waitForSeconds = new WaitForSeconds(_attackTime);

        while (_isOn)
        {
            int randomPrice = Random.Range(1, _wallet.MaxCount + 1);

            yield return new WaitUntil(() => _wallet.Count >= randomPrice);

            Unit target = _enemyFinder.GetBestEnemy(_enemyCastel);
            _deck.PlayRandomCards(GetNearstTile(target), randomPrice);

            yield return waitForSeconds;
        }
    }

    private Vector3 GetNearstTile(Unit unit)
    {
        Tilemap tilemap = _map.Tilemap;

        Vector3? nearestWorldPos = null;

        for (int x = tilemap.cellBounds.min.x; x < tilemap.cellBounds.max.x; x++)
        {
            for (int y = tilemap.cellBounds.min.y; y < tilemap.cellBounds.max.y; y++)
            {
                Vector3Int cellPos = new Vector3Int(x, y);

                if (tilemap.GetTile(cellPos) != _botTile)
                    continue;

                Vector3 worldPos = tilemap.GetCellCenterWorld(cellPos);

                if (nearestWorldPos == null ||
                    (worldPos - unit.transform.position).sqrMagnitude <
                    (nearestWorldPos.Value - unit.transform.position).sqrMagnitude)
                {
                    nearestWorldPos = worldPos;
                }
            }
        }

        return nearestWorldPos ?? unit.transform.position;
    }
}

