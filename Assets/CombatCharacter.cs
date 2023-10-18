using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCharacter : Character
{
    [SerializeField] Character _target;
    [SerializeField] CollisionDetector _enemyDetector;
    [SerializeField] Projectile _projectile;
    [SerializeField] float _attackDelay;
    [SerializeField] bool _resetTarget = true;

    private bool _targetExists;
    private bool _attackReady = true;

    private void OnEnable()
    {
        _enemyDetector.OnCollisionDetected += EnemyDetection;
        _enemyDetector.OnColliderLeft += EnemyLost;

        OnEnableCustom();
    }
    private void OnDisable()
    {
        _enemyDetector.OnCollisionDetected -= EnemyDetection;
        _enemyDetector.OnColliderLeft -= EnemyLost;

        OnDisableCustom();
    }

    public event Action<Character> OnSetTarget;
    public event Action OnResetTarget;

    protected virtual void OnEnableCustom() { }
    protected virtual void OnDisableCustom() { }

    public virtual void Attack() 
    {
        if (_targetExists && _attackReady)
        {
            if (_attackDelay > 0f) StartCoroutine(IEAttackDelay());
            _projectile.Attack(_target);
        }
    }
    private IEnumerator IEAttackDelay()
    {
        _attackReady = false;
        yield return new WaitForSeconds(_attackDelay);
        _attackReady = true;
    }

    private void EnemyDetection(GameObject enemy, int i)
    {
        if (enemy.TryGetComponent(out Character target) && !_targetExists) SetTarget(target);     
    }
    private void EnemyLost(GameObject enemy)
    {
        if (_resetTarget)
        {
            if (_enemyDetector.length > 0)
            {
                if (_enemyDetector.TryGetLastComponent(out Character target, (t) => t.faction != faction)) _target = target;
                else ResetTarget();
            }
            else ResetTarget();
        }
    }

    private void SetTarget(Character target)
    {
        _targetExists = true;
        _target = target;
        OnSetTarget?.Invoke(target);
    }
    private void ResetTarget()
    {
        _target = null;
        _targetExists = false;
        OnResetTarget?.Invoke();
    }
}
