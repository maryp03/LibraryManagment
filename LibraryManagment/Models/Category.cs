using System.ComponentModel.DataAnnotations;
namespace LibraryManagment.Models
{
    public class Category
    {
            
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }

        public ICollection<Book>? Books { get; set; }
    }
}

