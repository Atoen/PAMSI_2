using System.Runtime.CompilerServices;

namespace PAMSI_2.Sorts;

public static partial class Sort
{
    public static void QuickSort<T>(this SimpleArrayList<T> source, Comparator<T> comparator)
    {
        QuickSort(source.AsSpan(), 0, source.Count - 1, comparator);
    }

    private static void QuickSort<T>(Span<T> source, int left, int right, Comparator<T> comparator)
    {
        while (left < right)
        {
            var pivotIndex = PivotPoint(left, right);
            var index = Partition(source, left, right, pivotIndex, comparator);
            QuickSort(source, left, index - 1, comparator);
            left = index + 1;
        }
    }

    private static int Partition<T>(Span<T> source, int left, int right, int pivotIndex, Comparator<T> comparator)
    {
        var pivotValue = source[pivotIndex];

        Swap(source, pivotIndex, right);

        var pos = left;
        for (var i = left; i <= right - 1 ; i++)
        {
            if (comparator.Invoke(source[i], pivotValue) < 0)
            {
                Swap(source, i, pos);
                pos++;
            }
        }

        Swap(source, pos, right);

        return pos;
    }

    #region Field Specific Sorting

    public static void QuickSort(this SimpleArrayList<Movie> source, SortComparison comparison)
    {
        if (source is not {Count: > 1}) return;

        var span = source.AsSpan();

        switch (comparison)
        {
            case SortComparison.Rating:
                QuickSortRating(span, 0, source.Count - 1);
                break;

            case SortComparison.Title:
                QuickSortTitle(span, 0, source.Count - 1);
                break;

            case SortComparison.Id:
                QuickSortId(span, 0, source.Count - 1);
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(comparison), comparison, null);
        }
    }

    private static void QuickSortRating(Span<Movie> source, int left, int right)
    {
        while (left < right)
        {
            var index = PartitionRating(source, left, right);
            QuickSortRating(source, left, index - 1);
            left = index + 1;
        }
    }

    private static void QuickSortTitle(Span<Movie> source, int left, int right)
    {
        while (left < right)
        {
            var index = PartitionTitle(source, left, right);
            QuickSortTitle(source, left, index - 1);
            left = index + 1;
        }
    }

    private static void QuickSortId(Span<Movie> source, int left, int right)
    {
        while (left < right)
        {
            var index = PartitionId(source, left, right);
            QuickSortId(source, left, index - 1);
            left = index + 1;
        }
    }

    private static int PartitionRating(Span<Movie> source, int left, int right)
    {
        var pivotIndex = PivotPoint(left, right);
        var pivotValue = source[pivotIndex].Rating;

        Swap(source, pivotIndex, right);

        var pos = left;
        for (var i = left; i <= right - 1 ; i++)
        {
            if (source[i].Rating < pivotValue)
            {
                Swap(source, i, pos);
                pos++;
            }
        }

        Swap(source, pos, right);

        return pos;
    }

    private static int PartitionTitle(Span<Movie> source, int left, int right)
    {
        var pivotIndex = PivotPoint(left, right);
        var pivotValue = source[pivotIndex].Title;

        Swap(source, pivotIndex, right);

        var pos = left;
        for (var i = left; i <= right - 1 ; i++)
        {
            if (string.Compare(source[i].Title, pivotValue, StringComparison.Ordinal) < 0)
            {
                Swap(source, i, pos);
                pos++;
            }
        }

        Swap(source, pos, right);

        return pos;
    }

    private static int PartitionId(Span<Movie> source, int left, int right)
    {
        var pivotIndex = PivotPoint(left, right);
        var pivotValue = source[pivotIndex].Id;

        Swap(source, pivotIndex, right);

        var pos = left;
        for (var i = left; i <= right - 1 ; i++)
        {
            if (source[i].Id.CompareTo(pivotValue) < 0)
            {
                Swap(source, i, pos);
                pos++;
            }
        }

        Swap(source, pos, right);

        return pos;
    }

    #endregion

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int PivotPoint(int left, int right) => left + (right - left) / 2;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Swap<T>(Span<T> span, int i, int j) => (span[i], span[j]) = (span[j], span[i]);
}