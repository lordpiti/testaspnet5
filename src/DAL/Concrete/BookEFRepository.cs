using DAL.Interface;
using DAL.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class BookEFRepository : IBookRepository
    {
        private BooksContext _context;

        public BookEFRepository()
        {
            _context = new BooksContext();
        }

        private Book LibroToBook(Libro libro)
        {

            //var id = ObjectId.Parse(libro.Id.ToString().PadLeft(24, '0'));//new MongoDB.Bson.ObjectId(libro.Id.ToString() + "111111111111");

            return new Book() {
                Author = libro.Author,
                Title = libro.Title,
                Id = ObjectId.Parse(libro.IdMongo),
                read = libro.Read
            };
        }

        private Libro BookToLibro(Book libro)
        {
            //int id = Convert.ToInt32(libro.Id.ToString());
            int id = Convert.ToInt32(libro.Id.ToString());
            

            return new Libro() {
                Author = libro.Author,
                Id = id,
                Title = libro.Title,
                Read = libro.read
            };
        }

        public List<Book> GetBooks()
        {
            var libros = _context.Libros;

            var books = libros.Select(x => LibroToBook(x));

            return books.ToList();
        }

        public bool Delete(string id)
        {
            var selectedItem =_context.Libros.FirstOrDefault(x=>x.IdMongo==id);
            _context.Libros.Remove(selectedItem);
            _context.SaveChanges();

            return true;
        }

        public Book Create(Book book)
        {
            var libro = new Libro()
            {
                Author = book.Author,
                Read = book.read,
                Title = book.Title
            };

            _context.Libros.Add(libro);
            _context.SaveChanges();

            libro.IdMongo = libro.Id.ToString().PadLeft(24, '0');
            _context.SaveChanges();

            return book;
        }

        public Book Update(string id, Book book)
        {
            var libro = _context.Libros.FirstOrDefault(x => x.IdMongo == id);
            libro.Read = book.read;
            libro.Title = book.Title;
            libro.Author = book.Author;
            _context.SaveChanges();

            return book;
        }
    }
}
