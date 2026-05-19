using System.Collections;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] protected float _damage;
    [SerializeField] protected float _attackSpeed;
    [SerializeField] private float _ratio;

    protected Coroutine _attackCoroutine;
    private Team _team;

    protected bool _isAttackFrame = false;

    private void Awake()
    {
        _team = gameObject.GetComponent<Unit>().Team;
    }

    public void Init(int level)
    {
        _damage += _ratio * level;
        _attackSpeed += _ratio * level;
    }

    public void TryAttack(Unit target)
    {
        if (_attackCoroutine == null)
            _attackCoroutine = StartCoroutine(Attack(target));
    }

    public virtual IEnumerator Attack(Unit target)
    {
        var waitForSeconds = new WaitForSeconds(_attackSpeed);

        yield return new WaitUntil(() => _isAttackFrame);

        if (target.TryGetComponent<Health>(out Health health))
        {
            health.TakeDamage(_damage, _team);
            yield return waitForSeconds;
        }

        _attackCoroutine = null;
        _isAttackFrame = false;
    }

    public void OnAttackFrame()
    {
        _isAttackFrame = true;
    }
}
