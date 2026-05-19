using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyFinder), typeof(Attacker), typeof(Mover))]
[RequireComponent(typeof(AnimationsHandler), typeof(Rotator))]
public class Warrior : Unit
{
    private EnemyFinder _enemyFinder;
    private Attacker _attacker;
    private Mover _mover;
    private AnimationsHandler _animationsHandler;
    private Rotator _rotator;
    private Unit _target;

    protected override void AwakeInternal()
    {
        base.AwakeInternal();
        _enemyFinder = GetComponent<EnemyFinder>();
        _attacker = GetComponent<Attacker>();
        _mover = GetComponent<Mover>();
        _animationsHandler = GetComponent<AnimationsHandler>();
        _rotator = GetComponent<Rotator>();

        Health.Init(Level);
        _attacker.Init(Level);
    }

    private void Update()
    {
        _target = _enemyFinder.GetBestEnemy(this);

        if (_target != null)
        {
            _mover.Move(_target);
            _animationsHandler.SetMoution(_mover.IsOnTarget(_target) == false);

            if (_mover.IsOnTarget(_target))
            {
                _attacker.TryAttack(_target);
                _animationsHandler.PlayAttackAnimation();
            }
            else
            {
                _rotator.Rotate(_target.transform.position);
            }
        }
    }
}
