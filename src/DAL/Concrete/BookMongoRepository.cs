using DAL.Interface;
using DAL.Models;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class BookMongoRepository : IBookRepository
    {
        private MongoClient _client;
        private MongoServer _server;
        private MongoDatabase _database;


        public BookMongoRepository()
        {
            _client = new MongoClient("mongodb://localhost:27017");
            _server = _client.GetServer();
            _database = _server.GetDatabase("bookAPI");
        }

        public List<Book> GetBooks()
        {
            var books = _database.GetCollection<Book>("books").FindAll();

            return books.ToList();
        }

        public bool Delete(string id)
        {
            var query = Query<Book>.EQ(e => e.Id, new MongoDB.Bson.ObjectId(id));
            var result = _database.GetCollection<Book>("books").Remove(query);

            return true;
        }

        public Book Create(Book book)
        {
            _database.GetCollection<Book>("books").Save(book);

            return book;
        }

        public Book Update(string id, Book book)
        {
            book.Id = new MongoDB.Bson.ObjectId(id);
            var query = Query<Book>.EQ(e => e.Id, book.Id);
            var update = Update<Book>.Replace(book); // update modifiers
            _database.GetCollection<Book>("books").Update(query, update);

            return book;
        }
    }
}
