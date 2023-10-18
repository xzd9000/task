using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu] public class Item : ScriptableObject
{
    [SerializeField] int _id;
    [SerializeField] string _name;
    [SerializeField] Sprite _icon;

    public int id => _id;
    public string uiName => _name;
    public Sprite icon => _icon;

    [ContextMenu("SetID")]
    private void SetID()
    {
        System.DateTime now = System.DateTime.UtcNow;
        _id = now.Second + now.Minute * 60 + now.Hour * 3600 + now.DayOfYear * 3600 * 24 + now.Year % 40 * 3600 * 24 * 365;
    }

    public static bool operator ==(Item item1, Item item2) => item1._id == item2._id;
    public static bool operator !=(Item item1, Item item2) => !(item1 == item2);

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType()) return false;
        return _id == ((Item)obj)._id;
    }
    public override int GetHashCode() => _id;
}
