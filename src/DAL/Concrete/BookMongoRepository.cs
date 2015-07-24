using DAL.Interface;
using DAL.Models;
using DAL.Properties;
using Microsoft.Framework.OptionsModel;
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


        public BookMongoRepository(IOptions<AppSettings> settings)
        {
            var serverConnectionString = settings.Options.UseAzureMongoDb == "true" ?
                settings.Options.MongoConnectionAzure : settings.Options.MongoConnection;

            var databaseName = settings.Options.UseAzureMongoDb == "true" ?
                settings.Options.DatabaseAzure : settings.Options.Database;

            #region added only for Azure

            //for some reason DI doesn't look to work properly on Azure when loading the config values on the Appsettings class
            //thats why the connection strings for the mongodb db are hardcoded here. Locally it's ok
            if (serverConnectionString==null || databaseName==null)
            {
                serverConnectionString = "mongodb://lordpiti:Kidswast1@ds036648.mongolab.com:36648/MongoLab-e";
                databaseName = "MongoLab-e";
            }

            #endregion


            _client = new MongoClient(serverConnectionString);
            _server = _client.GetServer();
            _database = _server.GetDatabase(databaseName);
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

        public IEnumerable<GenericCategory> GetCategories()
        {
            var categories = _database.GetCollection<CategoryMongo>("categories").FindAll();
            var destinationList = AutoMapper.Mapper.Map<List<GenericCategory>>(categories);

            return destinationList;
        }
    }
}
