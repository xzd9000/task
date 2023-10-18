using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class Player : CombatCharacter
{
    [SerializeField] CollisionDetector _itemDetector;
    [SerializeField] Inventory _inventory;
    [SerializeField] bool requireAmmo;
    [SerializeField] Item ammoItem;

    private void Awake() => _inventory = GetComponent<Inventory>();

    protected sealed override void OnEnableCustom() => _itemDetector.OnCollisionDetected += ItemDetection;
    protected sealed override void OnDisableCustom() => _itemDetector.OnCollisionDetected -= ItemDetection;

    private void ItemDetection(GameObject item, int i) { if (item.TryGetComponent(out Inventory inventory)) inventory.TransferItems(_inventory); }

    public override void Attack()
    {
        if (!requireAmmo || _inventory.TakeItem(ammoItem)) base.Attack();
    }
}
