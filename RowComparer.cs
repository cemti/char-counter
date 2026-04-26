using System.Collections;

namespace CharCounter;

internal class RowComparer : IComparer
{
    public int Index { get; set; }
    public bool ReverseOrder { get; set; }
    private IComparable GetTag(object? x) => (IComparable)((ListViewItem)x!).SubItems[Index].Tag!;

    public int Compare(object? x, object? y)
    {
        var result = GetTag(x).CompareTo(GetTag(y));
        return ReverseOrder ? 1 - result : result;
    }
}
