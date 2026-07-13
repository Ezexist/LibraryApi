using System.ComponentModel.DataAnnotations;

namespace Library.QueryObjects
{
    public class BookQueryObject
    {
        public string? Title { get; set; }
        public string? Genre { get; set; }
        public string? SortBy { get; set; }
        public bool IsDescending { get; set; } = false;
        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; } = 1;
        [Range(1,100)]
        public int PageSize { get; set; } = 10;
    }
}
