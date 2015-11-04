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
using Microsoft.AspNet.Http;
using System.IO;
using Microsoft.Framework.Runtime;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        private IBookRepository _bookRepository;
        private readonly IApplicationEnvironment _appEnvironment;

        public HomeController(IBookRepository bookRepository, IApplicationEnvironment applicationEnvironment)
        {
            _bookRepository = bookRepository;
            _appEnvironment = applicationEnvironment;
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

        [HttpPost]
        [AcceptVerbs("GET", "POST")]
        [Route("/api/books/postImg")]
        public bool PostImg([FromBody] string imageDataURL)
        {
            string base64 = imageDataURL.Substring(imageDataURL.IndexOf(',') + 1);
            byte[] data = Convert.FromBase64String(base64);

            //System.IO.File.WriteAllBytes("c:\\test\\jaja.png", data);

            return true;
        }
    }
}
