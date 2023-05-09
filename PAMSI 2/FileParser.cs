using System.Globalization;
using System.Text.RegularExpressions;

namespace PAMSI_2;

public static partial class FileParser
{
    public static SimpleArrayList<Movie> ParseData(string path, int maxLines = -1)
    {
        using var reader = new StreamReader(path);
        var movies = new SimpleArrayList<Movie>();
        var lineIndex = 0;

        var regex = CsvRegex();

        while (reader.ReadLine() is { } line)
        {
            lineIndex++;

            if (maxLines != -1 && lineIndex > maxLines) break;

            var match = regex.Match(line);
            if (!match.Success) continue;

            var id = int.Parse(match.Groups[1].Value);
            var title = match.Groups[2].Value;

            // removing surrounding parenthesis
            if (title.StartsWith('\"')) title = title[1..^1];
            var rawRating = match.Groups[^1].Value;

            if (!double.TryParse(rawRating, CultureInfo.InvariantCulture, out var rating)) rating = double.NaN;

            movies.Add(new Movie(id, title, rating));
        }

        return movies;
    }

    [GeneratedRegex("(\\d+),(\"[^\"]*\"|([^,]*)),(\\d+\\.\\d+)?", RegexOptions.Compiled | RegexOptions.CultureInvariant)]
    private static partial Regex CsvRegex();
}