using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IBookRepository
    {
        List<GenericBook> GetBooks();
        bool Delete(string id);

        GenericBook Create(GenericBook book);

        GenericBook Update(string id, GenericBook book);

        IEnumerable<GenericCategory> GetCategories();
    }
}
