using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LibraryManagment.Models;

namespace LibraryManagment.Data
{
    public class LibraryManagmentContext : IdentityDbContext<ApplicationUser>
    {
        public LibraryManagmentContext(DbContextOptions<LibraryManagmentContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Book { get; set; } = default!;
        public DbSet<Borrowing> Borrowing { get; set; } = default!;
        public DbSet<Category> Category { get; set; } = default!;
        public DbSet<User> User { get; set; } = default!;

 /*       public async Task BorrowBookAsync(int userId, int bookId)
        {
            await Database.ExecuteSqlRawAsync(
                "EXEC BorrowBook @UserId = {0}, @BookId = {1}",
                userId, bookId);
        } */
    }
}
