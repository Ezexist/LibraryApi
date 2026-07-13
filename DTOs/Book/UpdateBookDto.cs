namespace Library.DTOs.Book
{
    public class UpdateBookDto
    {
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public int Pages { get; set; }

        public int AuthorId { get; set; }
    }
}
