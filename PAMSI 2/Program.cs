using System.Globalization;
using System.Text.RegularExpressions;
using PAMSI_2;

var movies = new SimpleArrayList<Movie>();

var path = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\data.csv");

using var reader = new StreamReader(path);

var regex = CsvRegex();

while (reader.ReadLine() is { } line)
{
    var match = regex.Match(line);

    if (!match.Success) continue;

    var title = match.Groups[2].Value;
    if (title.StartsWith('\"')) title = title[1..^1];

    var rawRating = match.Groups[^1].Value;

    if (!double.TryParse(rawRating, CultureInfo.InvariantCulture, out var rating)) rating = double.NaN;

    movies.Add(new Movie(title, rating));
}

Console.WriteLine($"Total movie count: {movies.Count}");

var moviesWithRating = new SimpleArrayList<Movie>();
foreach (var movie in movies)
{
    if (movie.HasRating) moviesWithRating.Add(movie);
}

Console.WriteLine($"Movies with rating: {moviesWithRating.Count}");

internal static partial class Program
{
    [GeneratedRegex("(\\d+),(\"[^\"]*\"|([^,]*)),(\\d+\\.\\d+)?", RegexOptions.Compiled | RegexOptions.CultureInvariant)]
    private static partial Regex CsvRegex();
}