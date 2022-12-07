using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBookTown.Data;

namespace TheBookTown.Models
{
    public class SQLBookRepository : IBookRepository
    {
        private readonly ApplicationDbContext context;

        public SQLBookRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public Book Add(Book book)
        {
            context.Books.Add(book);
            context.SaveChanges();
            return book;
        }

        public Book Delete(int id)
        {
            Book book = context.Books.Find(id);
            if (book != null)
            {
                context.Books.Remove(book);
                context.SaveChanges();

            }
            return book;
        }

        public IEnumerable<Book> GetAllBook()
        {
            return context.Books;
        }

        public Book GetBook(int Id)
        {
            return context.Books.Find(Id);
        }

        public Book Update(Book bookChanges)
        {
            var book = context.Books.Attach(bookChanges);
            book.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return bookChanges;
        }
    }
}
