using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using MongoDB.Driver;
using WebApplication3.Models;
using MongoDB.Driver.Builders;
using DAL.Models;
using DAL.Interface;
using DAL.Concrete;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        private IBookRepository _bookRepository;

        public HomeController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }

        [HttpGet]
        [Route("/api/books")]
        public List<GenericBook> GetBooks()
        {
            return _bookRepository.GetBooks();
        }

        [HttpDelete]
        [Route("/api/books/{id}")]
        public bool DeleteBook(string id)
        {
            _bookRepository.Delete(id);

            return true;
        }

        [HttpPost]
        [Route("/api/books")]
        public GenericBook CreateBook([FromBody] GenericBook theBook)
        {
            return _bookRepository.Create(theBook);//test comment
        }

        [HttpPut]
        [Route("/api/books/{id}")]
        public GenericBook UpdateBook(string id, [FromBody]GenericBook book)
        {          
            return _bookRepository.Update(id, book);
        }

        [HttpGet]
        [Route("/api/books/categories")]
        public IEnumerable<GenericCategory> GetCategories()
        {
            return _bookRepository.GetCategories();
        }
    }
}
