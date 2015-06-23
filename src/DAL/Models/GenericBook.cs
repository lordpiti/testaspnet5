using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class GenericBook
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public bool Read { get; set; }
    }
}
