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

        public List<GenericBook> GetBooks()
        {
            var books = _database.GetCollection<Book>("books").FindAll();
            var destinationList = AutoMapper.Mapper.Map<List<GenericBook>>(books);

            return destinationList;
        }

        public bool Delete(string id)
        {
            var query = Query<Book>.EQ(e => e.Id, new MongoDB.Bson.ObjectId(id));
            var result = _database.GetCollection<Book>("books").Remove(query);

            return true;
        }

        public GenericBook Create(GenericBook book)
        {
            var bookMongo = AutoMapper.Mapper.Map<Book>(book);

            _database.GetCollection<Book>("books").Save(bookMongo);

            book.Id = bookMongo.Id.ToString();

            return book;
        }

        public GenericBook Update(string id, GenericBook book)
        {
            var bookMongo = AutoMapper.Mapper.Map<Book>(book);

            bookMongo.Id = new MongoDB.Bson.ObjectId(id);
            var query = Query<Book>.EQ(e => e.Id, bookMongo.Id);
            var update = Update<Book>.Replace(bookMongo); // update modifiers
            _database.GetCollection<Book>("books").Update(query, update);

            book = AutoMapper.Mapper.Map<GenericBook>(bookMongo);

            return book;
        }
    }
}
