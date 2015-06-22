using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Book
    {
        public ObjectId Id { get; set; }

        [BsonElement("author")]
        public string Author { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        public int __v { get; set; }

        public bool read { get; set; }

    }
}
