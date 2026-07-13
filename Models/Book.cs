namespace Library.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public int Pages { get; set; }

        public int AuthorId { get; set; }
        public Author? Author { get; set; }

        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}
