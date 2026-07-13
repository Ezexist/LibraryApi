using Library.DTOs.Book;

namespace Library.DTOs.Author
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        public List<string> Books { get; set; } = new List<string>();
    }
}
