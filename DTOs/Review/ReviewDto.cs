namespace Library.DTOs.Review
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string? Comment { get; set; }
        public int Rating { get; set; }

        public int BookId { get; set; }
    }
}
