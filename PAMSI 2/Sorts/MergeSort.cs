namespace PAMSI_2.Sorts;

public static partial class Sort
{
    public static void MergeSort<T>(this SimpleArrayList<T> source, Comparator<T> comparator)
    {
        if (source is not {Count: > 1}) return;

        var tmp = new T[source.Count].AsSpan();
        MergeSort(source.AsSpan(), 0, source.Count - 1, tmp, comparator);
    }

    private static void MergeSort<T>(Span<T> source, int left, int right, Span<T> temp, Comparator<T> comparator)
    {
        if (left >= right) return;

        var middle = (left + right) / 2;

        MergeSort(source, left, middle, temp, comparator);
        MergeSort(source, middle + 1, right, temp, comparator);

        Merge(source, left, middle, right, temp, comparator);
    }

    private static void Merge<T>(Span<T> source, int left, int middle, int right, Span<T> temp, Comparator<T> comparator)
    {
        var i = left;
        var j = middle + 1;
        var k = left;

        while (i <= middle && j <= right)
        {
            if (comparator.Invoke(source[i], source[j]) <= 0)
            {
                temp[k] = source[i];
                i++;
            }
            else
            {
                temp[k] = source[j];
                j++;
            }
            k++;
        }

        while (i <= middle)
        {
            temp[k] = source[i];
            i++;
            k++;
        }

        while (j <= right)
        {
            temp[k] = source[j];
            j++;
            k++;
        }

        for (var x = left; x <= right; x++)
        {
            source[x] = temp[x];
        }
    }
}