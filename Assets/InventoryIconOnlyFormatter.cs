using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryIconOnlyFormatter : GUIFormatter
{
    [SerializeField] Image icon;
    [SerializeField] TMP_Text text;

    public override void Format(object data)
    {
        InventoryItem item = (InventoryItem)data;

        icon.sprite = item.item.icon;
        text.text = item.count > 1 ? item.count.ToString() : "";
    }

    public override void ClearFormat()
    {
        icon.sprite = null;
        text.text = "";
    }
}
