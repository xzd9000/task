using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsInput : MonoBehaviour
{
    [SerializeField] Button _shoot;
    [SerializeField] Button _inventory;

    private void Awake()
    {
        _shoot.onClick.AddListener(ShootButtonTap);
        _inventory.onClick.AddListener(InventoryButtonTap);
    }

    private void ShootButtonTap() => OnShootButtonTap?.Invoke();
    private void InventoryButtonTap() => OnInventoryButtonTap?.Invoke();

    public event Action OnShootButtonTap;
    public event Action OnInventoryButtonTap;
}
