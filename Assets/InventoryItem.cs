using System;

[Serializable] public struct InventoryItem
{
    public int count;
    public Item item;

    public InventoryItem(int count, Item item)
    {
        this.count = count;
        this.item = item;
    }

    public static bool operator ==(InventoryItem item1, InventoryItem item2) => item1.item == item2.item;
    public static bool operator !=(InventoryItem item1, InventoryItem item2) => !(item1 == item2);

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType()) return false;
        return item.Equals(obj);
    }
    public override int GetHashCode() => item.GetHashCode();
}