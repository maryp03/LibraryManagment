using System.ComponentModel.DataAnnotations;
namespace LibraryManagment.Models
{
    public class Borrowing
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime DateBorrowed { get; set; }
        public DateTime? DateReturned { get; set; }


        public User User { get; set; }
        public Book Book { get; set; }
    }
}
