using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Faction
{
    player,
    enemy
}
public enum CharacterState
{
    alive,
    dead
}

public class Character : MonoBehaviour
{
    [SerializeField] CharacterState _state = CharacterState.alive;
    [SerializeField] Faction _faction;
    [SerializeField] float _hp = 100f;
    [SerializeField] float _maxHP = 100f;
    [SerializeField] float _speed = 1f;
    [SerializeField] bool _destroyOnKill;
    [SerializeField] float _destroyDelay;

    public float hp => _hp;
    public float maxHP
    {
        get => _maxHP;
        set { _maxHP = value >= 1f ? value : 1f; }
    }
    public Faction faction => _faction;
    public CharacterState state => _state;

    public event Action OnDeath;
    public event Action<float, float> OnHit;

    public void Move(Vector3 direction, float magnitude = 1f) => transform.Translate(direction * magnitude * Time.deltaTime * _speed);

    public void MoveTowards(Vector3 position, float magnitude = 1f) => Move(Vector3.Normalize(position - transform.position), magnitude);

    public void Hit(float damage)
    {
        if (damage > 0)
        {
            _hp -= damage;
            OnHit?.Invoke(_hp, damage);
            if (_hp <= 0) Kill();
        }
    }
    public void SetHPDirectly(float value)
    {
        _hp = value;
        if (_hp <= 0) Kill();
    }

    public virtual void Kill()
    {
        _state = CharacterState.dead;
        OnDeath?.Invoke();
        if (_destroyOnKill) StartCoroutine(IEDestroyOnKill());
    }
    public IEnumerator IEDestroyOnKill()
    {
        if (_destroyDelay > 0f) yield return new WaitForSeconds(_destroyDelay);
        Destroy(gameObject);
    }
}
