using UnityEngine;
using UnityEngine.AI;
using System;
using TMPro;
using static UnityEngine.GraphicsBuffer;
[RequireComponent(typeof(NavMeshAgent))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _reachDistance;

    private NavMeshAgent _mover;

    private void Awake()
    {
        _mover = GetComponent<NavMeshAgent>();

        _mover.updateUpAxis = false;
        _mover.updateRotation = false;
    }

    public void Move(Unit target)
    {
        if (target == null)
            return;

        if (IsOnTarget(target) == false)
        {
            _mover.SetDestination(GetTargetPosition(target));
            _mover.isStopped = false;
        }
        else
        {
            _mover.isStopped = true;
        }
    }


    public bool IsOnTarget(Unit target)
    {
        return (GetTargetPosition(target) - transform.position).sqrMagnitude <= _reachDistance * _reachDistance;
    }

    private Vector3 GetTargetPosition(Unit target)
    {
        Vector3 targetPosition = Vector3.zero;
        const float offset = 0.01f;

        if (target.TryGetComponent<Collider2D>(out Collider2D tagetCollder) == false)
            return Vector3.zero;

        targetPosition = tagetCollder.ClosestPoint(transform.position);
        targetPosition = new Vector3(targetPosition.x + offset, targetPosition.y, 0);

        return targetPosition;
    }

    private void OnDrawGizmosSelected()
    {
        int segments = 50;
        Gizmos.color = Color.red;

        Vector3 center = transform.position;
        float step = 2 * Mathf.PI / segments;
        Vector3 prev = center + Vector3.right * _reachDistance;

        for (int i = 1; i <= segments; i++)
        {
            float angle = step * i;
            Vector3 next = center + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * _reachDistance;
            Gizmos.DrawLine(prev, next);
            prev = next;
        }
    }
}
