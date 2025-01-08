using LibraryManagment.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace LibraryManagment.Models
{
    public class Borrowing
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int BookId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DateNotInFuture]
        public DateTime DateBorrowed { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateReturned { get; set; }


        public User? User { get; set; }
        public Book? Book { get; set; }

        [NotMapped]
        public int DelayDays { get; set; }


    }

    public class DateNotInFutureAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime date)
            {
                if (date > DateTime.Now)
                {
                    return new ValidationResult("The borrowed date cannot be in the future.");
                }
            }

            return ValidationResult.Success;
        }
    }


}
