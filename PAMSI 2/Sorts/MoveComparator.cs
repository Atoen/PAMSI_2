namespace PAMSI_2.Sorts;

public delegate int Comparator<in T>(T first, T second);

public static class MoveComparator
{
    public static Comparator<Movie> Rating { get; } = (first, second) => first.Rating.CompareTo(second.Rating);
    public static Comparator<Movie> Title { get; } = (first, second) => string.Compare(first.Title, second.Title, StringComparison.Ordinal);
    public static Comparator<Movie> Id { get; } = (first, second) => first.Id.CompareTo(second.Id);

}