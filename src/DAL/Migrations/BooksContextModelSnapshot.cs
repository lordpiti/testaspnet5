using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Builders;
using Microsoft.Data.Entity.Relational.Migrations.Infrastructure;
using DAL.Models;

namespace DAL.Migrations
{
    [ContextType(typeof(BooksContext))]
    partial class BooksContextModelSnapshot : ModelSnapshot
    {
        public override IModel Model
        {
            get
            {
                var builder = new BasicModelBuilder()
                    .Annotation("SqlServer:ValueGeneration", "Sequence");
                
                builder.Entity("DAL.Models.Libro", b =>
                    {
                        b.Property<string>("Author")
                            .Annotation("OriginalValueIndex", 0);
                        b.Property<int>("Id")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 1)
                            .Annotation("SqlServer:ValueGeneration", "Default");
                        b.Property<string>("IdMongo")
                            .Annotation("OriginalValueIndex", 2);
                        b.Property<bool>("Read")
                            .Annotation("OriginalValueIndex", 3);
                        b.Property<string>("Title")
                            .Annotation("OriginalValueIndex", 4);
                        b.Key("Id");
                    });
                
                return builder.Model;
            }
        }
    }
}
