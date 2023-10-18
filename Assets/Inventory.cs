using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour, IGUIList
{
    [SerializeField] List<InventoryItem> _items = new List<InventoryItem>();
    [SerializeField] bool _destroyOnEmpty;

    public int length => _items.Count;

    public IList guiList => _items;

    public void AddItem(InventoryItem item)
    {
        int index;
        if ((index = _items.FindIndex((i) => i == item)) >= 0)
            _items[index] = new InventoryItem(_items[index].count + item.count,
                                                  _items[index].item);
        else _items.Add(item);
    }
    public void AddItems(IEnumerable<InventoryItem> items)
    {
        bool added = false;
        foreach (InventoryItem item in items)
        {
            AddItem(item);
            added = true;
        }
        if (added) ListChanged?.Invoke();
    }
    public bool TakeItem(Item item, int count = 1)
    {
        if (count > 0) 
        {
            int index;
            if ((index = FindItemIndex(item)) >= 0) return TakeItemAt(index, count);
        }
        return false;
    }
    public bool TakeItemAt(int i, int count = 1)
    {
        if (count > 0 && i >= 0 && i < _items.Count)
        {
            int newCount = _items[i].count - count;
            if (newCount > 0)
            {
                _items[i] = new InventoryItem(newCount, _items[i].item);
                ListChanged?.Invoke();
                return true;
            }
            else if (newCount == 0)
            {
                _items.RemoveAt(i);
                ListChanged?.Invoke();
                return true;
            }
        }
        return false;
    }
    public void RemoveItems()
    {
        _items.Clear();
        if (_destroyOnEmpty) Destroy(gameObject);
    }
    public void TransferItems(Inventory inventory)
    {
        inventory.AddItems(_items);
        RemoveItems();
    }

    public int FindItemIndex(Item item) => _items.FindIndex((i) => i.item == item);

    public event Action ListChanged;

    public InventoryItem this[int i] => _items[i];
}