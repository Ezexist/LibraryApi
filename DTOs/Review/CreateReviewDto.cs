using System.ComponentModel.DataAnnotations;

namespace Library.DTOs.Review
{
    public class CreateReviewDto
    {
        [Required]
        [StringLength(300)]
        public string? Comment { get; set; }
        [Required]
        [Range(1,10)]
        public int Rating { get; set; }

        public int BookId { get; set; }
    }
}
