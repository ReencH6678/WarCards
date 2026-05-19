using System.Collections.Generic;
using UnityEngine;

public class UnitsHandler : MonoBehaviour
{
    [SerializeField] private List<Unit> _units;
    private void Awake()
    {
        Unit[] units = FindObjectsOfType<Unit>();

        foreach (Unit unit in units)
        {
            AddUnit(unit);
        }
    }

    public void AddUnit(Unit unit)
    {
        _units.Add(unit);
    }

    public void RemoveUnit(Unit unit)
    {
        _units.Remove(unit);
    }

    public List<Unit> GetUnits()
    {
        List<Unit> units = new List<Unit>();

        foreach (Unit unit in _units) 
            units.Add(unit);

        return units;
    }
}
