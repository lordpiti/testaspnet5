using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DAL.Models
{
    public class CategoryMongo
    {
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }
    }
}
