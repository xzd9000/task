using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CombatCharacter))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] Character _target;
    [SerializeField] float _attackDistance;

    private CombatCharacter _character;
    private bool _targetSet;

    private void Awake() => _character = GetComponent<CombatCharacter>();

    private void OnEnable() => _character.OnSetTarget += TargetFound;
    private void OnDisable() => _character.OnSetTarget -= TargetFound;    

    private void Update()
    {
        if (_character.state != CharacterState.dead)
        {
            if (_targetSet)
            {
                if (_target.state != CharacterState.dead)
                {
                    if (Vector3.Distance(transform.position, _target.transform.position) > _attackDistance) _character.MoveTowards(_target.transform.position);
                    else _character.Attack();
                }
                else
                {
                    _targetSet = false;
                    _target = null;
                }
            }
        }
    }

    private void TargetFound(Character target)
    {
        _target = target;
        _targetSet = true;
    }
}
