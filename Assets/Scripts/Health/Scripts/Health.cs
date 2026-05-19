using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxCount;
    [SerializeField] private float _ratio;

    public float Count { get; private set; }

    public event Action<float, float> Changed;
    public event Action<Team> Died;

    private bool _died = false;

    private void Awake()
    {
        Count = _maxCount;
        Changed?.Invoke(Count, _maxCount);
    }

    public void Init(int level)
    {

        Count += level * _ratio;
        Changed?.Invoke(Count, _maxCount);
    }

    public void TakeDamage(float damage, Team team)
    {
        if (damage > 0)
        {
            Count -= damage;
            Changed?.Invoke(Count, _maxCount);
        }

        if(Count <= 0 && _died == false)
        {
            Died?.Invoke(team);
            _died = true;
        }
    }

    public void Heal(float healCount)
    {
        if (healCount > 0)
        {
            Count += healCount;
            Changed?.Invoke(Count, _maxCount);
        }
    }

    public void ResetCount()
    {
        Count = _maxCount;
    }
}
