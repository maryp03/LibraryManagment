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

        [Range(0, int.MaxValue, ErrorMessage = "The year published cannot be greater than the current year.")]
        [YearPublishedValidation]
        public int YearPublished { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Available copies cannot be less than 0.")]
        public int AvailableCopies { get; set; }

        public Category? Category { get; set; }

        public string CategoryName => Category?.Name ?? "Unknown";
    }

    public class YearPublishedValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is int yearPublished)
            {
                int currentYear = DateTime.Now.Year;
                return yearPublished <= currentYear;
            }
            return false;
        }
    }
}
