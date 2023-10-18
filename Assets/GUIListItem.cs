using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GUIFormatter))]
public class GUIListItem : GUIComponentHolder
{
    [SerializeField] int _index;
    [SerializeField] GUIListController _owner;

    private bool _indexSet;

    public int index => _index;
    public GUIListController onwer { set { _owner = value; } }
    public GUIFormatter formatter { get; private set; }

    protected sealed override void AwakeCustom() => formatter = GetComponent<GUIFormatter>();

    public void SetIndex(int i)
    {
        if (!_indexSet)
        {
            _index = i;
            _indexSet = true;
        }
    }

    public void ItemClick() => _owner.ItemButtonClicked(_index);
}
