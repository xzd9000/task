using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIListController : MonoBehaviour
{
    [SerializeField] MonoBehaviour _monoData;
    [SerializeField] RectTransform _guiList;
    [SerializeField][Min(1)] int _minListSlots = 10;

    private RectTransform _slot;
    private GUIListItem _listItem;
    private List<GUIListItem> _list;

    private int _previousSelection = -1;
    public int currentSelection { get; private set; } = -1;

    public IGUIList data { get; private set; }

    private void Awake()
    {
        data = (IGUIList)_monoData;
        _slot = (RectTransform)_guiList.GetChild(0);
        _list = new List<GUIListItem>(_minListSlots);

        _listItem = _slot.GetComponent<GUIListItem>();
        _listItem.onwer = this;

        GUIListItem item;
        for (int i = 0; i < _minListSlots; i++)
        {
            if (i == 0) item = _listItem;
            else item = Instantiate(_listItem, _listItem.transform.parent);

            AddListItem(item, i);
        }
    }

    private void OnEnable()
    {
        UpdateList();
        data.ListChanged += UpdateList;
    }
    private void OnDisable()
    {
        data.ListChanged -= UpdateList;
        ResetSelection();
    }

    public event Action<int> SelectionChanged;
    public event Action ListUpdated;

    private void UpdateList()
    {
        int count = Mathf.Max(data.guiList.Count, _list.Count);
        for (int i = 0; i < count; i++)
        {
            if (i >= _list.Count) AddListItem(Instantiate(_listItem, _listItem.transform.parent), i);

            if (i < data.guiList.Count) _list[i].formatter.Format(data.guiList[i]);
            else _list[i].formatter.ClearFormat();
        }
        ListUpdated?.Invoke();
    }

    private void AddListItem(GUIListItem item, int index)
    {
        item.SetIndex(index);
        if (item.TryGetComponent(out Button button)) button.onClick.AddListener(item.ItemClick);
        _list.Add(item);
    }

    public void ItemButtonClicked(int index)
    {
        Button button = _list[index].GetComponent<Button>();

        int tSelection = currentSelection;

        SetSelectionIndex(index);

        button.image.color = button.colors.highlightedColor;
        ClearPreviousSelection(tSelection);
    }

    public void SetSelectionIndex(int selection)
    {
        currentSelection = selection;
        SelectionChanged?.Invoke(selection);
    }

    public void ResetSelection()
    {
        ClearSelection(currentSelection);
        SetSelectionIndex(-1);
        ClearPreviousSelection(-1);
    }
    private void ClearPreviousSelection(int newSelection)
    {
        ClearSelection(_previousSelection);
        _previousSelection = newSelection;
    }

    private void ClearSelection(int selection)
    {
        if (selection >= 0)
        {
            Button button = _list[selection].GetComponent<Button>();
            button.image.color = button.colors.normalColor;
        }
    }
}
