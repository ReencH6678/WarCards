using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationsHandler : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetMoution(bool isOn)
    {
        _animator.SetBool("IsMoving", isOn);
    }

    public void PlayAttackAnimation()
    {
        _animator.SetTrigger("Attack");
    }
}
