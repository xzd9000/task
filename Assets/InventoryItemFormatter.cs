using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItemFormatter : GUIFormatter
{
    [SerializeField] Image icon;
    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text count;

    public override void Format(object data)
    {
        InventoryItem item = (InventoryItem)data;
        icon.sprite = item.item.icon;
        title.text = item.item.uiName;
        count.text = item.count > 1 ? item.count.ToString() : "";
    }
    public override void ClearFormat()
    {
        icon.sprite = null;
        title.text = "";
        count.text = "";
    }
}
