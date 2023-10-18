using UnityEngine;

public abstract class GUIFormatter : MonoBehaviour
{
    public abstract void Format(object data);

    public abstract void ClearFormat();
}