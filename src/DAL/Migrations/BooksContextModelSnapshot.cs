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
                    .Annotation("SqlServer:ValueGeneration", "Identity");
                
                builder.Entity("DAL.Models.Category", b =>
                    {
                        b.Property<int>("Id")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 0)
                            .Annotation("SqlServer:ValueGeneration", "Default");
                        b.Property<string>("Name")
                            .Annotation("OriginalValueIndex", 1);
                        b.Key("Id");
                    });
                
                builder.Entity("DAL.Models.Libro", b =>
                    {
                        b.Property<string>("Author")
                            .Annotation("OriginalValueIndex", 0);
                        b.Property<int>("Id")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 1)
                            .Annotation("SqlServer:ValueGeneration", "Default");
                        b.Property<bool>("Read")
                            .Annotation("OriginalValueIndex", 2);
                        b.Property<string>("Title")
                            .Annotation("OriginalValueIndex", 3);
                        b.Key("Id");
                    });
                
                return builder.Model;
            }
        }
    }
}
