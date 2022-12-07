using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheBookTown.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "Atomic Habits",
                    Author = "James Clear",
                    Genre = Genr.Self_Help
                },
                new Book
                {
                    Id = 2,
                    Title = " The Alchemist",
                    Author = "Paulo Coelho",
                    Genre = Genr.Fiction
                }
                );

        }
    }
}
