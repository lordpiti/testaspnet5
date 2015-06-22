using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IBookRepository
    {
        List<Book> GetBooks();
        bool Delete(string id);

        Book Create(Book book);

        Book Update(string id, Book book);
    }
}
