using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LibraryManagment.Data;
using System;
using System.Linq;
using LibraryManagment.Models;

namespace LibraryManagment.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new LibraryManagmentContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<LibraryManagmentContext>>()))
        {
            
            if (context.Book.Any())
            {
                return;   
            }
            context.Book.AddRange(
                new Book
                {
                    Title = "Harry Potter and the Chamber of Secrets",
                    AuthorName = "J.K. Rowling",
                    YearPublished = 2005,
                    AvailableCopies = 2
                },
                new Book
                {
                    Title = "Little Women",
                    AuthorName = "Louisa May Alcott",
                    YearPublished = 1868,
                    AvailableCopies = 1
                },
                new Book
                {
                    Title = "Proud and Prejudice",
                    AuthorName = "Jane Asten",
                    YearPublished = 1813,
                    AvailableCopies = 3
                }

            );

        }
    }
}