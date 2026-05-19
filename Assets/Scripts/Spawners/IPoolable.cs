using System;
using UnityEngine;

public interface IPoolable
{
    public event Action<IPoolable> DeactivationRequested;
    public void ResetObject();
}