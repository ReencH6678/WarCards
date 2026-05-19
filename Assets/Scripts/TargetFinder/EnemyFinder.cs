using System.Collections.Generic;
using UnityEngine;

public class EnemyFinder : MonoBehaviour
{
    protected UnitsHandler _unitsHandler;

    private void Awake()
    {
        _unitsHandler = Camera.main.GetComponent<UnitsHandler>();
    }

    public void SetUnitsHandler(UnitsHandler unitsHandler)
    {
        if (unitsHandler != null)
            _unitsHandler = unitsHandler;
    }

    public virtual Unit GetBestEnemy(Unit unit)
    {
        List<Unit> enemys = _unitsHandler.GetUnits();

        Unit nearstEnemy = null;
        Unit priorityEnemy = null;

        float nearestPriorityDistance = float.MaxValue;
        float nearestEnemyDistance = float.MaxValue;

        foreach (Unit enemy in enemys)
        {
            if (enemy == unit)
                continue;

            if (enemy.Team == unit.Team)
                continue;

            if (enemy.Health.Count <= 0)
                continue;

            if (enemy.gameObject.activeSelf == false)
                continue;

            float distance = (enemy.transform.position - unit.transform.position).sqrMagnitude;

            if (IsPriority(enemy))
            {
                if (distance < nearestPriorityDistance)
                {
                    priorityEnemy = enemy;
                    nearestPriorityDistance = distance;
                }
            }
            else
            {
                if (distance < nearestEnemyDistance)
                {
                    nearstEnemy = enemy;
                    nearestEnemyDistance = distance;
                }
            }

        }

        return priorityEnemy != null ? priorityEnemy : nearstEnemy;
    }

    protected virtual bool IsPriority(Unit unit)
    {
        return true;
    }
}