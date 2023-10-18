using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUIComponentHolder : MonoBehaviour
{
    private Image[] images;
    private TMP_Text[] texts;

    public Image GetImage(int i) => images[i];
    public TMP_Text GetText(int i) => texts[i];

    private void Awake()
    {
        images = GetComponentsInChildren<Image>();
        texts = GetComponentsInChildren<TMP_Text>();

        AwakeCustom();
    }
    protected virtual void AwakeCustom() { }
}
