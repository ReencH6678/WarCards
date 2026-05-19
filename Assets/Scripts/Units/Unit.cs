using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Unit : MonoBehaviour, IPoolable
{
    private Health _health;
    
    [field: SerializeField] public Team Team { get; private set; }
    public Health Health => _health;
    public int Level { get; private set; }
    public float Damage { get; private set; }
    public int HealthCount { get; private set; }

    public event Action<IPoolable> DeactivationRequested;

    private void Awake()
    {
        _health = GetComponent<Health>();

        AwakeInternal();
    }

    private void OnEnable()
    {
        _health.Died += Die;
    }

    public void SetTeam(Team team)
    {
        Team = team;
    }

    public void ResetObject()
    {
        _health.ResetCount();
    }

    protected virtual void AwakeInternal()
    {

    }

    protected virtual void Die(Team team)
    {
        DeactivationRequested?.Invoke(this);
        Destroy(gameObject);
    }
}
