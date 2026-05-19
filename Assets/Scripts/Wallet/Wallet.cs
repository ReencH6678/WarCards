using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private int _maxCount;

    public int Count { get; private set; }
    public int MaxCount => _maxCount;

    public event Action<int> Changed;

    public void Add(int count)
    {
        Count = Mathf.Clamp(Count + count, 0, _maxCount);
        Changed?.Invoke(Count);
    }

    public void Remove(int count)
    {
        Count -= count;
        Changed?.Invoke(Count);
    }
}
