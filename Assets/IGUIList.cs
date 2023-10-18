public interface IGUIList
{
    public event System.Action ListChanged;
    public System.Collections.IList guiList { get; }
}
