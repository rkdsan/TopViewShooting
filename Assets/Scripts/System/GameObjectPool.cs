using UnityEngine;
using UnityEngine.Pool;

public abstract class GameObjectPool<T> : MonoBehaviour where T : Component
{
    private IObjectPool<T> _pool;
    public IObjectPool<T> Pool
    {
        get
        {
            if (_pool == null)
            {
                _pool = new ObjectPool<T>(OnCreate, OnGet, OnRelease, DestroyItem);
            }
            return _pool;
        }
    }

    public void Release(T item)
    {
        Pool.Release(item);
    }

    protected abstract T OnCreate();
    protected abstract void OnGet(T item);
    protected abstract void OnRelease(T item);
    protected virtual void DestroyItem(T item)
    {
        //GameEventManager.DetachAllEvent(item);
        Destroy(item.gameObject);
    }
}
