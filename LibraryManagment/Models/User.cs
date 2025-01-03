﻿using System.ComponentModel.DataAnnotations;

namespace LibraryManagment.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }


        public ICollection<Borrowing>? Borrowings { get; set; }

    }
}
