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

        public async Task BorrowBookAsync(int userId, int bookId)
        {
            await Database.ExecuteSqlRawAsync(
                "EXEC BorrowBook @UserId = {0}, @BookId = {1}",
                userId, bookId);
        }

        public async Task ReturnBookAsync(int borrowingId)
        {
            await Database.ExecuteSqlRawAsync(
                "EXEC ReturnBook @BorrowingId = {0}",
                borrowingId);
        }
        public async Task UpdateBorrowingAsync(int borrowingId, int userId, int oldBookId, int newBookId)
        {
            await Database.ExecuteSqlRawAsync(
                "EXEC UpdateBorrowing @BorrowingId = {0}, @UserId = {1}, @OldBookId = {2}, @NewBookId = {3}",
                borrowingId, userId, oldBookId, newBookId);
        }
    }
}
