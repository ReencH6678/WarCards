using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullitAttacker : Attacker
{
    [SerializeField] private Bullit _bullitPrefabe;
    [SerializeField] private Transform _shootPosition;

    public override IEnumerator Attack(Unit target)
    {
        var waitForSeconds = new WaitForSeconds(_attackSpeed);

        yield return new WaitUntil(() => _isAttackFrame);

        if (target.TryGetComponent<Health>(out Health health))
        {

            Bullit bullit = Instantiate(_bullitPrefabe, _shootPosition.position, Quaternion.identity);
            bullit.Init(health, _damage);

            yield return waitForSeconds;
        }

        _attackCoroutine = null;
        _isAttackFrame = false;
    }
}
