using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity.Metadata;

namespace DAL.Models
{
    public class Libro
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public bool Read { get; set; }

        //public Category Category { get; set; }
    }
}
