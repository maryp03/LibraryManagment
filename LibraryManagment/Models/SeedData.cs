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

            if (!context.Category.Any())
            {
                context.Category.AddRange(
                    new Category
                    {
                        Name = "Fiction"
                    },
                    new Category
                    {
                        Name = "Classics"
                    },
                    new Category
                    {
                        Name = "Science Fiction"
                    },
                    new Category
                    {
                        Name = "Fantasy"
                    },
                    new Category
                    {
                        Name="Horror"
                    },
                    new Category
                    {
                        Name="Thiller"
                    }
                );
                context.SaveChanges();
            }

            if (!context.Book.Any())
            {
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
                        AuthorName = "Jane Austen",
                        YearPublished = 1813,
                        AvailableCopies = 3
                    }
                );
                context.SaveChanges();
            }
            if (!context.User.Any())
            {
                context.User.AddRange(
                    new User
                    {
                        FirstName = "Anna",
                        LastName = "Kowalska",
                        Email = "anna@gmail.com"
                    },
                    new User
                    {
                        FirstName = "Kate",
                        LastName = "Blue",
                        Email = "katie123@gmail.com"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}