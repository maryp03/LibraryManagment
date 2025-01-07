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
        public DateTime DateBorrowed { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateReturned { get; set; }


        public User User { get; set; }
        public Book Book { get; set; }
    }
}
