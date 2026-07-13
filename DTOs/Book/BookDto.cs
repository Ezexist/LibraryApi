using Library.DTOs.Review;

namespace Library.DTOs.Book
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public int Pages { get; set; }

       // public int AuthorId { get; set; }
       public string AuthorName { get; set; } = string.Empty ;

        
        public List<ReviewDto> Reviews { get; set; }
        public int ReviewCount { get; set; }
        public double AverageRating { get; set; }
    }
}
