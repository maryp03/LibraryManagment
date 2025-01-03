using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LibraryManagment.Models;

namespace LibraryManagment.Data
{
    public class LibraryManagmentContext : DbContext
    {
        public LibraryManagmentContext (DbContextOptions<LibraryManagmentContext> options)
            : base(options)
        {
        }

        public DbSet<LibraryManagment.Models.Book> Book { get; set; } = default!;
        public DbSet<LibraryManagment.Models.Borrowing> Borrowing { get; set; } = default!;
        public DbSet<LibraryManagment.Models.Category> Category { get; set; } = default!;
        public DbSet<LibraryManagment.Models.User> User { get; set; } = default!;
    }
}
