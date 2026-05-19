using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldHandler : MonoBehaviour
{
    [SerializeField] private Wallet _enemyWallet;  
    [SerializeField] private Wallet _playerWallet;

    [SerializeField] private UnitsHandler _unitsHandler;

    [SerializeField] private float _deley;

    [SerializeField] private int _goldOfTick;
    [SerializeField] private int _goldOfMine;

    private List<GoldMine> _goldMines = new List<GoldMine>();

    private bool _isOn = true;

    private void Awake()
    {
        foreach(Unit unit in _unitsHandler.GetUnits())
        {
            if(unit.TryGetComponent<GoldMine>(out GoldMine goldMine))
                _goldMines.Add(goldMine);
        }
    }

    private void Start()
    {
        StartCoroutine(AddGold());
    }

    private IEnumerator AddGold()
    {
        var waitForSeconds = new WaitForSeconds(_deley);

        while (_isOn) 
        {
            _playerWallet.Add(_goldOfTick);
            _enemyWallet.Add(_goldOfTick);

            foreach(GoldMine goldMine in _goldMines)
            {
                if (goldMine.Team == Team.Blue)
                    _playerWallet.Add(_goldOfMine);
                else if (goldMine.Team == Team.Red)
                    _enemyWallet.Add(_goldOfMine);
            }


            Debug.Log(_enemyWallet.Count);
            yield return waitForSeconds;
        }
    }
}
