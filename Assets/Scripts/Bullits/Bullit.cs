using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullit : Unit
{
    [SerializeField] private float _speed;
    [SerializeField] private float _reachDistance;

    private float _damage;
    private Health _target;

    private void Update()
    {
        if (_target != null)
        {
            if ((_target.transform.position - transform.position).sqrMagnitude > _reachDistance * _reachDistance)
            {
                Vector3 direction = (_target.transform.position - transform.position).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
            else
            {
                _target.TakeDamage(_damage, Team);
                Destroy(gameObject);
            }
        }
    }

    public void Init(Health target, float damage)
    {
        _target = target;
        _damage = damage;
    }
}
