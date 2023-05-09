namespace PAMSI_2.Sorts;

public static partial class Sort
{
    public static void IntroSort<T>(this SimpleArrayList<T> source, Comparator<T> comparator)
    {
        if (source is not {Count: > 1}) return;

        var depthLimit = (int) Math.Floor(2 * Math.Log(source.Count, 2));
        IntroSort(source.AsSpan(), 0, source.Count -1, depthLimit, comparator);
    }

    private static void IntroSort<T>(Span<T> source, int left, int right, int depthLimit, Comparator<T> comparator)
    {
        while (right > left)
        {
            var partitionSize = right - left + 1;

            // Use Insertion Sort for small subarrays
            if (partitionSize <= 16)
            {
                InsertionSort(source, left, right, comparator);
                return;
            }

            // Switch to Heap Sort if the recursion depth exceeds the limit
            if (depthLimit == 0)
            {
                HeapSort(source, left, right, comparator);
                return;
            }

            depthLimit--;

            // Choose a pivot element and partition the array
            var pivotIndex = ChoosePivot(source, left, right, comparator);
            pivotIndex = Partition(source, left, right, pivotIndex, comparator);

            // Recurse on the smaller partition and adjust the bounds
            if (pivotIndex - left < right - pivotIndex)
            {
                IntroSort(source, left, pivotIndex - 1, depthLimit, comparator);
                left = pivotIndex + 1;
            }
            else
            {
                IntroSort(source, pivotIndex + 1, right, depthLimit, comparator);
                right = pivotIndex - 1;
            }
        }
    }

    private static int ChoosePivot<T>(Span<T> source, int left, int right, Comparator<T> comparator)
    {
        return Median(source, left, right, comparator);
    }

    private static int Median<T>(Span<T> source, int left, int right, Comparator<T> comparator)
    {
        var i1 = Random.Shared.Next(left, right + 1);
        var i2 = Random.Shared.Next(left, right + 1);
        var i3 = Random.Shared.Next(left, right + 1);
        var (a, b, c) = (source[i1], source[i2], source[i3]);

        if (comparator.Invoke(a, b) < 0)
        {
            if (comparator.Invoke(b, c) < 0) return i2;
            return comparator.Invoke(a, c) < 0 ? i3 : i1;
        }

        if (comparator.Invoke(a, c) < 0) return i1;
        return comparator.Invoke(b, c) < 0 ? i3 : i2;
    }

    private static void InsertionSort<T>(Span<T> source, int left, int right, Comparator<T> comparator)
    {
        for (var i = left + 1; i <= right; i++)
        {
            var value = source[i];
            var j = i - 1;

            while (j >= left && comparator.Invoke(source[j], value) > 0)
            {
                source[j + 1] = source[j];
                j--;
            }

            source[j + 1] = value;
        }
    }

    private static void HeapSort<T>(Span<T> source, int left, int right, Comparator<T> comparator)
    {
        var heapSize = right - left + 1;
        for (var i = heapSize / 2; i >= 1; i--)
        {
            Heapify(source, left, i, heapSize, comparator);
        }

        for (var i = heapSize; i > 1; i--)
        {
            Swap(source, left, left + i - 1);
            heapSize--;
            
            Heapify(source, left, 1, heapSize, comparator);
        }
    }

    private static void Heapify<T>(Span<T> source, int lo, int i, int heapSize, Comparator<T> comparator)
    {
        while (true)
        {
            var left = 2 * i;
            var right = left + 1;
            var largest = i;

            if (left <= heapSize && comparator.Invoke(source[lo + left - 1], source[lo + largest - 1]) > 0)
            {
                largest = left;
            }

            if (right <= heapSize && comparator.Invoke(source[lo + right - 1], source[lo + largest - 1]) > 0)
            {
                largest = right;
            }

            if (largest == i)
            {
                break;
            }

            Swap(source, lo + i - 1, lo + largest - 1);
            i = largest;
        }
    }
}