using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheBookTown.Models
{
    public interface IBookRepository
    {
        Book GetBook(int Id);
        IEnumerable<Book> GetAllBook();
        Book Add(Book book);
        Book Update(Book bookChanges);
        Book Delete(int id);
    }
}
