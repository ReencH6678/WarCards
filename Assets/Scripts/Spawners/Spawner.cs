using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour where T : MonoBehaviour, IPoolable
{
    [SerializeField] private T _prefabe;

    private ObjectPool<T> _pool;

    private int _poolCapacity = 100;
    private int _poolMaxSize = 100;

    private void Awake()
    {
        _pool = new ObjectPool<T>
            (
                createFunc: () => CreateFunc(),
                actionOnGet: (obj) => ActionOnGet(obj),
                actionOnRelease: (obj) => obj.gameObject.SetActive(false),
                actionOnDestroy: (obj) => Destroy(obj),
                collectionCheck: true,
                defaultCapacity: _poolCapacity,
                maxSize: _poolMaxSize);
    }

    private T CreateFunc()
    {
        T obj = Instantiate(_prefabe, GetSpawnPosition(), Quaternion.identity);
        obj.DeactivationRequested += Release;

        return obj;
    }

    public virtual void ActionOnGet(T obj)
    {
        obj.ResetObject();
        obj.transform.position = GetSpawnPosition();
        obj.gameObject.SetActive(true);
    }

    public virtual void Release(IPoolable obj)
    {
        obj.DeactivationRequested -= Release;

        _pool.Release((T)obj);
    }

    protected virtual Vector3 GetSpawnPosition()
    {
        return new Vector3(0, 0, 0);
    }
}