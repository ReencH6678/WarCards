using UnityEngine;

public class HealthView : MonoBehaviour
{
    [SerializeField] protected Health _health;

    private void OnEnable()
    {
        _health.Changed += UpdateHealthView;
    }

    private void OnDisable()
    {
        _health.Changed -= UpdateHealthView;
    }

    public virtual void UpdateHealthView(float healthCount, float maxHealthCount)
    {
    }
}
