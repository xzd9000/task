using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollisionDetector : MonoBehaviour
{
    [SerializeField][Min(0)] int _startCapacity;
    [SerializeField] List<GameObject> _entered;

    public int length => _entered.Count;
    public GameObject latest => _entered[_entered.Count - 1];

    private void Awake() => _entered = new List<GameObject>(_startCapacity);

    public bool TryGetLastComponent<T>(out T component, Predicate<T> componentCondition = null)
    {
        for (int i = _entered.Count - 1; i >= 0; i--)
        {
            if (TryGetComponent(out component) && (componentCondition == null || componentCondition(component))) return true;           
        }
        component = default;
        return false;
    }

    protected void Collision(GameObject entered)
    {
        _entered.Add(entered);
        OnCollisionDetected?.Invoke(entered, _entered.Count - 1);
    }
    protected void ColliderLeft(GameObject left)
    {
        _entered.Remove(left);
        OnColliderLeft?.Invoke(left);
    }

    public event Action<GameObject, int> OnCollisionDetected;
    public event Action<GameObject> OnColliderLeft;

    public GameObject this[int i] => _entered[i];
}
