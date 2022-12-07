using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheBookTown.Models
{
    public class MockBookRepository : IBookRepository
    {
        private List<Book> _bookList;
        public MockBookRepository()
        {
            _bookList = new List<Book>()
            {
                new Book() {Id =1, Title="Atomic Habits", Genre=Genr.Self_Help,Author="James Clear" },
                new Book() { Id = 2, Title = "The Alchemist", Genre = Genr.Fiction, Author = "Paulo Coelho" },
                

             };
        }

        public Book Add(Book book)
        {
            book.Id = _bookList.Max(e => e.Id) + 1;
            _bookList.Add(book);
            return book;
        }

        public Book Delete(int id)
        {
            Book book = _bookList.FirstOrDefault(e => e.Id == id);
            if (book != null)
            {
                _bookList.Remove(book);
            }
            return book;
        }

        public IEnumerable<Book> GetAllBook()
        {
            return _bookList;
        }

        public Book GetBook(int Id)
        {

            return _bookList.FirstOrDefault(e => e.Id == Id);
        }

        public Book Update(Book bookChanges)
        {
            Book book = _bookList.FirstOrDefault(e => e.Id == bookChanges.Id);
            if (book != null)
            {
                book.Title = bookChanges.Title;
                book.Author = bookChanges.Author;
                book.Genre = bookChanges.Genre;
            }
            return book;

        }
    }
}

