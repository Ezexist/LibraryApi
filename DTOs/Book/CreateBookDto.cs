using System.ComponentModel.DataAnnotations;

namespace Library.DTOs.Book
{
    public class CreateBookDto
    {
        [Required]
        [StringLength(50)]
        public string Title { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public string Genre { get; set; } = string.Empty;
        [Required]
        [Range(1,1000)]
        public int Pages { get; set; }
        [Required]
        [Range(1,int.MaxValue)]
        public int AuthorId { get; set; }
    }
}
