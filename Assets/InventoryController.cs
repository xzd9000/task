using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GUIListController _uiInventory;
    [SerializeField] GUIFormatter _selectedItem;
    [SerializeField] Button _deleteButton;
    [SerializeField] Button _closeButton;

    private IGUIList _inventory;

    private void Awake()
    {
        _inventory = _uiInventory.data;
        _deleteButton.onClick.AddListener(DeleteInventoryItem);
        _closeButton.onClick.AddListener(Hide);
    }

    private void OnEnable()
    {
        _uiInventory.SelectionChanged += UpdateSelected;
        _uiInventory.ListUpdated += UpdateSelected;
        UpdateSelected();
    }
    private void OnDisable()
    {
        _uiInventory.SelectionChanged -= UpdateSelected;
        _uiInventory.ListUpdated -= UpdateSelected;
    }

    private void UpdateSelected() => UpdateSelected(_uiInventory.currentSelection);
    private void UpdateSelected(int selection)
    {
        if (selection >= 0 && selection < _inventory.guiList.Count) _selectedItem.Format(_inventory.guiList[selection]);
        else _selectedItem.ClearFormat();
    }
    private void DeleteInventoryItem()
    {
        int selection = _uiInventory.currentSelection;
        if (selection >= 0) ((Inventory)_inventory).TakeItemAt(selection);
    }

    public void Hide() => gameObject.SetActive(false);
    public void Show() => gameObject.SetActive(true);
}