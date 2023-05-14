using System.Diagnostics;
using PAMSI_2.Sorts;

namespace PAMSI_2;

public static class Benchmark
{
    public static void Run(SimpleArrayList<Movie> list, Comparator<Movie> comparator)
    {
        QuickSort(list, comparator);
        MergeSort(list, comparator);
        IntroSort(list, comparator);
    }

    private static void QuickSort(SimpleArrayList<Movie> list, Comparator<Movie> comparator)
    {
        Console.Write("\nQuicksort ");
        
        var stopwatch = new Stopwatch();

        var toSort = new SimpleArrayList<Movie>(list);
        stopwatch.Start();

        toSort.QuickSort(comparator);

        stopwatch.Stop();

        toSort.AssertSorted(comparator);

        var average = toSort.Average(movie => movie.Rating);
        var count = toSort.Count;

        var median = count % 2 == 0
            ? (toSort[count / 2 - 1].Rating + toSort[count / 2].Rating) / 2
            : toSort[(count + 1) / 2 - 1].Rating;
        
        Console.WriteLine(stopwatch.Elapsed);
        Console.WriteLine($"Average: {average}, median: {median}");
    }
    
    private static void MergeSort(SimpleArrayList<Movie> list, Comparator<Movie> comparator)
    {
        Console.Write("\nMergeSort ");

        var stopwatch = new Stopwatch();

        var toSort = new SimpleArrayList<Movie>(list);
        stopwatch.Start();

        toSort.MergeSort(comparator);

        stopwatch.Stop();

        toSort.AssertSorted(comparator);

        var average = toSort.Average(movie => movie.Rating);
        var count = toSort.Count;

        var median = count % 2 == 0
            ? (toSort[count / 2 - 1].Rating + toSort[count / 2].Rating) / 2
            : toSort[(count + 1) / 2 - 1].Rating;
        
        Console.WriteLine(stopwatch.Elapsed);
        Console.WriteLine($"Average: {average}, median: {median}");

    }
    
    private static void IntroSort(SimpleArrayList<Movie> list, Comparator<Movie> comparator)
    {
        Console.Write("\nIntroSort ");

        var stopwatch = new Stopwatch();

        var toSort = new SimpleArrayList<Movie>(list);
        stopwatch.Start();

        toSort.IntroSort(comparator);

        stopwatch.Stop();

        toSort.AssertSorted(comparator);

        var average = toSort.Average(movie => movie.Rating);
        var count = toSort.Count;

        var median = count % 2 == 0
            ? (toSort[count / 2 - 1].Rating + toSort[count / 2].Rating) / 2
            : toSort[(count + 1) / 2 - 1].Rating;
        
        Console.WriteLine(stopwatch.Elapsed);
        Console.WriteLine($"Average: {average}, median: {median}");
    }
}