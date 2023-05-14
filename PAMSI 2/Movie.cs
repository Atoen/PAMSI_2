using System.Globalization;

namespace PAMSI_2;

public record Movie(int Id, string Title, double Rating)
{
    public bool HasRating => !double.IsNaN(Rating);

    public override string ToString() =>
        $"{Id} {Title} {(HasRating ? Rating.ToString("F1", CultureInfo.InvariantCulture) : "--")}";
}