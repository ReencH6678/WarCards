using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class UnitSpawner : Spawner<Unit>
{
    [SerializeField] private UnitsHandler _unitsHandler;

    private Vector3 _spawPosition;

    private Dictionary<Unit, ObjectPool<Unit>> _pools = new Dictionary<Unit, ObjectPool<Unit>>();
    public void Spawn(Vector3 spawnPosition, Unit unit)
    {
        _spawPosition = spawnPosition;

        if (_pools.TryGetValue(unit, out ObjectPool<Unit> pool) == false)
        {
            pool = CreatePool(unit);
            _pools.Add(unit, pool);
        }

        _pools[unit].Get();
    }

    public override void ActionOnGet(Unit obj)
    {
        base.ActionOnGet(obj);

        if (obj.TryGetComponent<EnemyFinder>(out EnemyFinder enemyFinder))
            enemyFinder.SetUnitsHandler(_unitsHandler);

        _unitsHandler.AddUnit(obj);

    }

    public override void Release(IPoolable obj)
    {
        base.Release(obj);
        _unitsHandler.RemoveUnit((Unit)obj);
    }
    protected override Vector3 GetSpawnPosition()
    {
        return (_spawPosition);
    }

    private ObjectPool<Unit> CreatePool(Unit prefabe)
    {
        int poolCapacity = 100;
        int poolMaxSize = 100;

        ObjectPool<Unit> pool = new ObjectPool<Unit>
                (
                    createFunc: () =>
                    {
                        Unit obj = Instantiate(prefabe, GetSpawnPosition(), Quaternion.identity);
                        obj.DeactivationRequested += Release;

                        return obj;
                    },
                    actionOnGet: (obj) => ActionOnGet(obj),
                    actionOnRelease: (obj) => obj.gameObject.SetActive(false),
                    actionOnDestroy: (obj) => Destroy(obj),
                    collectionCheck: true,
                    defaultCapacity: poolCapacity,
                    maxSize: poolMaxSize);

        return pool;
    }
}