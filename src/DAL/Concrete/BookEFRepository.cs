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

        public List<GenericBook> GetBooks()
        {
            var libros = _context.Libros;

            var destinationList = AutoMapper.Mapper.Map<List<GenericBook>>(libros);

            return destinationList;
        }

        public bool Delete(string id)
        {
            var selectedItem =_context.Libros.FirstOrDefault(x=>x.Id==Convert.ToInt32(id));
            _context.Libros.Remove(selectedItem);
            _context.SaveChanges();

            return true;
        }

        public GenericBook Create(GenericBook book)
        {
            var libro = AutoMapper.Mapper.Map<Libro>(book);

            _context.Libros.Add(libro);
            _context.SaveChanges();

            book.Id=libro.Id.ToString();
            return book;
        }

        public GenericBook Update(string id, GenericBook book)
        {
            var libro = _context.Libros.FirstOrDefault(x => x.Id == Convert.ToInt32(id));
            libro.Read = book.Read;
            libro.Title = book.Title;
            libro.Author = book.Author;
            _context.SaveChanges();

            return book;
        }
    }
}
