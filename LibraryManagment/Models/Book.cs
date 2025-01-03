using System.ComponentModel.DataAnnotations;
namespace LibraryManagment.Models
{
    public class Book
    {
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Title { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string AuthorName { get; set; }
        public int? CategoryId { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public int YearPublished { get; set; }
        public int AvailableCopies { get; set; }

        public Category? Category { get; set; }

        public string CategoryName => Category?.Name ?? "Unknown";
    }
}
