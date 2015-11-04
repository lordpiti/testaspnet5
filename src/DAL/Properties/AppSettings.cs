using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Properties
{
    public class AppSettings
    {
        public string SiteTitle { get; set; }
        public string Database { get; set; }
        public string MongoConnection { get; set; }
        public string MongoConnectionAzure { get; set; }
        public string DatabaseAzure { get; set; }
        public string UseAzureMongoDb { get; set; }
        public string StorageKey { get; set; }
    }
}
