namespace Library.DTOs.Other
{
    public class GenreStatisticsDto
    {
        public string Genre { get; set; } = string.Empty;
        public int BookCount { get; set; }
        public double AveragePages { get; set; }
    }
}
