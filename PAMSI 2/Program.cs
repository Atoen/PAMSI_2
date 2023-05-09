using System.Diagnostics;
using PAMSI_2;
using PAMSI_2.Sorts;

var path = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\data.csv");
var movies = FileParser.ParseData(path);

Console.WriteLine($"Total movie count: {movies.Count}");

var moviesWithRating = new SimpleArrayList<Movie>();
foreach (var movie in movies)
{
    if (movie.HasRating) moviesWithRating.Add(movie);
}

Console.WriteLine($"Movies with rating: {moviesWithRating.Count}");

var sw = Stopwatch.StartNew();

moviesWithRating.IntroSort(MoveComparator.Rating);
Console.WriteLine("Sorted");

sw.Stop();

moviesWithRating.AssertSorted(MoveComparator.Rating);
Console.WriteLine("Verified");
Console.WriteLine(sw.Elapsed);